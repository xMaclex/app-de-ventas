# Módulo de Comprobantes Fiscales (NCF) — Documentación de Implementación

> **Contexto:** República Dominicana · DGII · Norma 01-07  
> **Tablas involucradas:** `secuencias_ncf_tb`, `ventas_tb`, `facturas_tb`  
> **Controladores:** `NcfController`, `VentasController`, `FacturasController`

---

## 📐 Arquitectura del Módulo NCF

El sistema implementa NCF mediante una tabla de **secuencias** (`SecuenciaNcf`) que actúa como contador controlado. Cada venta consume exactamente un NCF de la secuencia activa correspondiente al tipo de documento del cliente.

```
Cliente seleccionado
       │
       ▼
  TipoDocumento == "RNC"?
       │ Sí              │ No
       ▼                 ▼
  Tipo NCF: B01     Tipo NCF: B02
       │
       ▼
  Buscar SecuenciaNcf activa + no vencida + no agotada
       │
       ├── No existe → Error: "Configure una en NCF"
       │
       ▼
  GenerarProximoNcf()  →  "B020000000001"
       │                   NumeroActual++
       ▼
  SaveChangesAsync()  →  NCF persiste en la BD
       │
       ▼
  Crear Factura con Ncf = ncfGenerado  ← ⚠️ BUG ACTUAL: no se asigna
```

---

## 🗄️ Modelo: `SecuenciaNcf` (`secuencias_ncf_tb`)

### Columnas de la tabla

| Columna              | Tipo          | Descripción                                    |
|----------------------|---------------|------------------------------------------------|
| `id_secuencia`       | INT (PK)      | Identificador único                            |
| `tipo_comprobante`   | VARCHAR(3)    | `B01`, `B02`, `B14`, `B15`                    |
| `numero_comprobante` | INT           | FK referenciada desde `ventas_tb` y `facturas_tb` |
| `numero_inicial`     | INT           | Inicio del rango autorizado por DGII           |
| `numero_final`       | INT           | Fin del rango autorizado                       |
| `numero_actual`      | INT           | Próximo NCF a emitir (se incrementa en cada uso) |
| `numero_ultimo`      | INT           | Último NCF efectivamente utilizado (`UlitmoNumero`) |
| `fecha_vencimiento`  | DATE          | Fecha de expiración de la secuencia            |
| `activa`             | BOOL          | Solo una secuencia activa por tipo a la vez    |

> ⚠️ Nota: el campo se llama `numero_ultimo` en BD y `UlitmoNumero` en C# (typo en el modelo, conservar consistencia al migrار).

### Índice definido en `VentasDbContext`

```csharp
modelBuilder.Entity<SecuenciaNcf>()
    .HasIndex(s => s.NumeroComprobante);
```

### Relaciones (navegación)

```csharp
public ICollection<Factura> Facturas { get; set; }
public ICollection<Venta>   Ventas   { get; set; }
```

Tanto `Factura` como `Venta` tienen una FK `numero_comprobante` apuntando a `SecuenciaNcf`.

---

## 🧮 Propiedades Calculadas (`[NotMapped]`)

Toda la lógica de estado y alertas vive en el modelo, sin consultas adicionales a la BD:

| Propiedad          | Cálculo                                                        | Uso en vistas                          |
|--------------------|----------------------------------------------------------------|----------------------------------------|
| `Disponibles`      | `NumeroFinal - NumeroActual`                                   | Columna "Disponibles" en tabla         |
| `TotalSecuencia`   | `NumeroFinal - NumeroInicial`                                  | Base para calcular porcentaje          |
| `Utilizados`       | `NumeroActual - NumeroInicial`                                 | Internamente para `PorcentajeUsado`    |
| `PorcentajeUsado`  | `Utilizados / TotalSecuencia * 100` (redondeado a 1 decimal)  | Barra de progreso en la tabla          |
| `CercaDeAgotarse`  | `Disponibles > 0 && Disponibles < 100`                        | Alerta amarilla automática             |
| `Agotada`          | `NumeroActual > NumeroFinal`                                   | Bloquea emisión de NCF                 |
| `Vencida`          | `DateTime.Today > FechaVencimiento`                           | Bloquea emisión de NCF + alerta roja   |
| `Descripcion`      | Switch sobre `TipoComprobante`                                | Texto legible en tabla y alertas       |
| `EstadoVisual`     | Lógica de prioridad: Inactiva → Vencida → Agotada → Por Agotar → Activa | Badge en tabla    |
| `BadgeClass`       | Clase Bootstrap según `EstadoVisual`                           | Color del badge sin lógica en la vista |

### Método `GenerarProximoNcf()`

```csharp
public string? GenerarProximoNcf()
{
    if (!Activa || Vencida || Agotada) return null;
    string ncf = $"{TipoComprobante}{NumeroActual:D10}";
    NumeroActual++;
    return ncf;
}
```

**Importante:** este método modifica `NumeroActual` en memoria. Requiere llamar `SaveChangesAsync()` después para persistir el incremento. El NCF generado tiene el formato `B020000000001` (tipo + número de 10 dígitos).

---

## 🎮 Controlador: `NcfController`

Ruta base: `/Ncf`  
Autorización: `[Authorize]` en toda la clase.

### Acciones CRUD

| Ruta | Método | Descripción |
|---|---|---|
| `GET /Ncf` | `Index` | Lista todas las secuencias ordenadas por tipo |
| `GET /Ncf/Crear` | `Crear` | Formulario con valores por defecto (1 año de vigencia, rango 1–10000) |
| `POST /Ncf/Crear` | `Crear` | Valida y persiste. Bloquea si ya existe una activa del mismo tipo |
| `GET /Ncf/Editar/{id}` | `Editar` | Carga secuencia para editar |
| `POST /Ncf/Editar/{id}` | `Editar` | Valida rangos y persiste cambios |
| `POST /Ncf/ToggleActiva/{id}` | `ToggleActiva` | Activa/desactiva. Si activa, desactiva automáticamente otras del mismo tipo |
| `POST /Ncf/Eliminar/{id}` | `Eliminar` | Solo permite eliminar si `NumeroActual == NumeroInicial` (nunca utilizada) |

### Validaciones en `POST /Ncf/Crear`

```csharp
// 1. NumeroFinal debe ser mayor que NumeroInicial
if (model.NumeroFinal <= model.NumeroInicial)
    ModelState.AddModelError(...)

// 2. FechaVencimiento debe ser futura
if (model.FechaVencimiento.Date <= DateTime.Today)
    ModelState.AddModelError(...)

// 3. No puede haber dos secuencias activas del mismo tipo
var existeActiva = await _context.SecuenciaNcf
    .AnyAsync(s => s.TipoComprobante == model.TipoComprobante && s.Activa);
if (existeActiva)
    ModelState.AddModelError(...)
```

### Validaciones en `POST /Ncf/Editar`

```csharp
// NumeroFinal > NumeroInicial
// NumeroActual >= NumeroInicial
// NumeroActual <= NumeroFinal
```

### Lógica de `ToggleActiva`

Al **activar** una secuencia, el sistema automáticamente desactiva todas las otras del mismo tipo en el mismo `SaveChangesAsync()`:

```csharp
if (!secuencia.Activa) // si se va a activar
{
    var otrasActivas = await _context.SecuenciaNcf
        .Where(s => s.TipoComprobante == secuencia.TipoComprobante
                 && s.IdSecuencia != id && s.Activa)
        .ToListAsync();

    foreach (var otra in otrasActivas)
        otra.Activa = false;
}
secuencia.Activa = !secuencia.Activa;
```

### APIs JSON internas

**`GET /Ncf/ProximoNcf?tipo=B02`**  
Devuelve el próximo NCF disponible sin consumirlo. Respuestas posibles:

```json
// Éxito
{ "ok": true, "ncf": "B020000000015", "disponibles": 9985, "vencimiento": "25/03/2027" }

// Error
{ "ok": false, "mensaje": "No hay secuencia activa para el tipo B02." }
{ "ok": false, "mensaje": "La secuencia B02 está vencida." }
{ "ok": false, "mensaje": "La secuencia B02 está agotada." }
```

**`POST /Ncf/ConsumirNcf`** (body: `{ "tipo": "B02" }`)  
Consume (incrementa) el contador dentro de una transacción. Diseñado para ser llamado internamente desde `VentasController`. Devuelve el NCF generado o un error.

---

## 🖥️ Vista: `Ncf/Index.cshtml`

### Panel de alertas automáticas

Antes de la tabla, la vista evalúa el estado de cada secuencia activa y muestra un bloque de advertencia si alguna está vencida o cerca de agotarse:

```razor
@{
    var alertas = Model.Where(s => s.Activa && (s.CercaDeAgotarse || s.Vencida)).ToList();
}
@if (alertas.Any())
{
    // Muestra alerta amarilla con lista de secuencias críticas
    // "quedan solo X comprobantes disponibles" o "vencida el dd/MM/yyyy"
}
```

### Tabla de secuencias — columnas visualizadas

| Columna | Fuente | Detalle |
|---|---|---|
| **Tipo** | `s.Descripcion` | Texto completo: "B01 – Crédito Fiscal" |
| **Rango** | `NumeroInicial` – `NumeroFinal` (formato `D10`) | Ej: `0000000001 – 0000010000` |
| **Actual** | `TipoComprobante + NumeroActual.D10` | Próximo NCF que se generará |
| **Último** | `TipoComprobante + UlitmoNumero.D10` | Último NCF efectivamente emitido |
| **Disponibles** | `s.Disponibles` formateado con `N0` | Número de NCF restantes |
| **Uso** | Barra de progreso con `PorcentajeUsado` | Verde < 75% · Amarillo < 90% · Rojo ≥ 90% |
| **Vencimiento** | `FechaVencimiento.ToString("dd/MM/yyyy")` | Agrega badge rojo "Vencida" si ya expiró |
| **Estado** | `s.EstadoVisual` con clase `s.BadgeClass` | Activa / Por Agotar / Agotada / Vencida / Inactiva |
| **Acciones** | Botones condicionales | Ver abajo |

### Botones de acción por fila

- **Editar** (`btn-outline-primary`) — siempre visible.
- **ToggleActiva** — muestra `bi-toggle-on` (amarillo) si activa, `bi-toggle-off` (verde) si inactiva. Texto del título cambia dinámicamente.
- **Eliminar** (`btn-outline-danger`) — solo visible si `NumeroActual == NumeroInicial` (secuencia no utilizada).

### Colores de fila condicionales

```razor
class="@(s.Vencida || s.Agotada ? "table-danger" : s.CercaDeAgotarse ? "table-warning" : "")"
```

---

## 🔗 Integración con `VentasController`

### Flujo dentro de `ProcesarVenta`

El NCF se determina y consume en el paso 5 del procesamiento de venta:

```csharp
// 1. Determinar tipo según TipoDocumento del cliente
string tipoNcf = (cliente.TipoDocumento == "RNC") ? "B01" : "B02";

// 2. Buscar secuencia válida
var secuenciaNcf = await _context.SecuenciaNcf
    .Where(s => s.TipoComprobante == tipoNcf 
             && s.Activa 
             && !s.Vencida 
             && !s.Agotada)
    .FirstOrDefaultAsync();

// 3. Verificar existencia
if (secuenciaNcf == null)
{
    TempData["Error"] = $"No hay secuencia NCF activa para el tipo {tipoNcf}. " +
                        "Configure una en Configuración → Comprobantes Fiscales (NCF).";
    await transaction.RollbackAsync();
    return RedirectToAction(nameof(PuntoVenta));
}

// 4. Generar NCF (incrementa NumeroActual en memoria)
string? ncfGenerado = secuenciaNcf.GenerarProximoNcf();

if (ncfGenerado == null)
{
    TempData["Error"] = "La secuencia NCF está agotada o vencida...";
    await transaction.RollbackAsync();
    return RedirectToAction(nameof(PuntoVenta));
}

// 5. Guardar venta (SaveChangesAsync persiste el NumeroActual incrementado)
_context.Ventas.Add(nuevaVenta);
await _context.SaveChangesAsync(); // ← aquí se persiste el NCF consumido
```

---

## 🔗 Integración con `FacturasController`

### Reporte 607 (`GET /Facturas/Reporte607`)

Consulta facturas activas del mes/año seleccionado e incluye el NCF en el reporte:

```csharp
var facturas = await _context.Facturas
    .Include(f => f.Cliente)
    .Include(f => f.Producto)
    .Where(f => f.FechaEmision.Month == mes &&
               f.FechaEmision.Year == año &&
               f.Estado == "Activa")
    .OrderBy(f => f.FechaEmision)
    .ToListAsync();
```

En la vista `Reporte607.cshtml`, el NCF aparece en la columna "Número NCF" de la tabla formato DGII con los campos: RNC/Cédula, Tipo ID, NCF, tipo de ingreso, fecha, montos.

### Exportación Excel (`GET /Facturas/ExportarReporte607`)

Genera `.xlsx` usando EPPlus con columnas: ID, Fecha, Cliente, TipoComprobante, MontoTotal, MontoItbis. El NCF **no está incluido** en el Excel actual — pendiente de agregar.

### Anulación de factura (`POST /Facturas/Anular`)

La anulación de facturas individuales **no devuelve el NCF** a la secuencia (comportamiento correcto: un NCF emitido no se reutiliza). Solo actualiza el estado a `"Anulada"` y registra motivo y fecha.

---

## 📍 Dónde aparece el NCF en el sistema

### En el punto de venta (`PuntoVenta.cshtml`)

El selector de tipo de comprobante está **comentado** en el formulario. El tipo NCF se determina automáticamente en el backend:

```html
<!--  <label class="form-label">Tipo de Comprobante:</label>
      <select asp-for="Venta.TipoComprobante" class="form-select mb-2">
          <option value="B01">B01 - Crédito Fiscal</option>
          ...
      </select> -->
```

El usuario no necesita seleccionar el tipo: el sistema lo infiere del `TipoDocumento` del cliente seleccionado.

### En los detalles de venta (`Ventas/Details.cshtml`)

La sección de comprobantes fiscales muestra las facturas con su NCF:

```razor
@foreach (var factura in Model.Facturas.Take(3))
{
    <strong>@factura.NumeroFactura</strong>
    <small>NCF: @factura.Ncf</small>  // ← actualmente vacío por el bug
}
@if (Model.Facturas.Count > 3)
{
    <small>+ @(Model.Facturas.Count - 3) más...</small>
}
```

### En el detalle de factura (`Facturas/Details.cshtml`)

El NCF se muestra prominentemente en el encabezado de la factura:

```razor
<div class="alert alert-info mb-4">
    <h4>@Model.Ncf</h4>
    <span class="badge bg-primary fs-6">@Model.TipoNCFDescripcion</span>
</div>
```

### En el listado de facturas (`Facturas/Index.cshtml`)

Columna `NCF` en la tabla principal. Filtro por tipo de comprobante (`B01`, `B02`, `B14`, `B15`).

### En el Reporte 607 (`Facturas/Reporte607.cshtml`)

Columna "Número NCF" en el formato de declaración DGII. Agrupación y totales por tipo de comprobante.

### En la gestión de NCF (`Ncf/Index.cshtml`)

Panel principal de administración. Accesible desde `Facturas/Index` mediante el botón "Gestión NCF".

---

## 🔑 Reglas de Negocio Implementadas

| Regla | Implementación | Dónde |
|---|---|---|
| Solo una secuencia activa por tipo | Validación en `POST /Ncf/Crear` + desactivación automática en `ToggleActiva` | `NcfController` |
| No eliminar secuencias usadas | Guard: `NumeroActual == NumeroInicial` | `NcfController.Eliminar` |
| Fecha de vencimiento futura al crear | `model.FechaVencimiento.Date <= DateTime.Today` | `NcfController.Crear` |
| NCF bloqueado si vencido o agotado | Verificación antes de emitir | `VentasController.ProcesarVenta` |
| Tipo B01 para empresas (RNC) | `cliente.TipoDocumento == "RNC"` | `VentasController.ProcesarVenta` |
| Tipo B02 para consumidores finales | Caso `else` del condicional anterior | `VentasController.ProcesarVenta` |
| Factura anulada no afecta al NCF | La anulación no revierte `NumeroActual` | `FacturasController.Anular` |
| Alerta al llegar a menos de 100 disponibles | `CercaDeAgotarse` en modelo | `Ncf/Index.cshtml` |
| Fila resaltada en rojo si vencida/agotada | CSS condicional en la tabla | `Ncf/Index.cshtml` |
| Formato NCF: tipo + 10 dígitos | `$"{TipoComprobante}{NumeroActual:D10}"` | `SecuenciaNcf.GenerarProximoNcf()` |

---

## 🐛 Bugs y Pendientes

### Bug 1 — NCF no persiste en la factura (CRÍTICO)

**Descripción:** `ncfGenerado` se genera y consume correctamente (el contador se incrementa), pero nunca se asigna al campo `Factura.Ncf`. Todas las facturas quedan con `Ncf = ""`.

**Impacto:** El Reporte 607 muestra NCF vacíos. La vista `Facturas/Details` muestra el NCF en blanco. Incumplimiento fiscal.

**Corrección:**
```csharp
// En VentasController.ProcesarVenta, dentro del foreach de items del carrito:
var factura = new Factura
{
    IdVenta = nuevaVenta.IdVenta,
    Ncf = ncfGenerado,  // ← línea faltante
    // ... resto de propiedades
};
```

### Bug 2 — Solo B01 y B02 son cubiertos automáticamente

El código solo asigna `B01` (RNC) o `B02` (otros). Los tipos `B14` y `B15` no tienen lógica de asignación automática. Si un cliente gubernamental o de régimen especial hace una compra, recibirá un `B02`.

**Corrección sugerida:** Agregar un campo `TipoNcfPreferido` al modelo `Clientes` o permitir al usuario seleccionar el tipo en el PuntoVenta.

### Bug 3 — `UlitmoNumero` nunca se actualiza

El campo `numero_ultimo` (typo: `UlitmoNumero`) está presente en el modelo y se muestra en la tabla NCF, pero **ningún método lo actualiza**. `GenerarProximoNcf()` solo incrementa `NumeroActual`.

**Corrección:**
```csharp
public string? GenerarProximoNcf()
{
    if (!Activa || Vencida || Agotada) return null;
    string ncf = $"{TipoComprobante}{NumeroActual:D10}";
    UlitmoNumero = NumeroActual;  // ← agregar esta línea
    NumeroActual++;
    return ncf;
}
```

### Bug 4 — Enlace `GestionNCF` apunta a acción comentada

En `Facturas/Index.cshtml` hay un botón:
```razor
<a asp-action="GestionNCF" class="btn btn-warning">Gestión NCF</a>
```
Pero en `FacturasController`, `GestionNCF` está **comentado**. El botón genera un 404.

**Corrección:** Cambiar el enlace para apuntar al controlador correcto:
```razor
<a asp-controller="Ncf" asp-action="Index" class="btn btn-warning">
    <i class="bi bi-gear"></i> Gestión NCF
</a>
```

### Bug 5 — NCF no incluido en exportación Excel del 607

`ExportarReporte607` genera el Excel con 6 columnas pero omite el NCF, que es el campo más importante del reporte fiscal.

**Corrección:**
```csharp
ws.Cells[1,7].Value = "NCF";
// ...
ws.Cells[r,7].Value = f.Ncf;
```

---

## 💡 Ideas de Mejora Futura

### Alerta visual en el Punto de Venta

Antes de procesar una venta, mostrar en el PuntoVenta el estado de la secuencia NCF que se va a usar. Si está cerca de agotarse o vencida, advertir al cajero sin bloquear la venta.

```javascript
// Al seleccionar un cliente, consultar el NCF disponible
async function verificarNcf(tipoDocumento) {
    const tipo = tipoDocumento === 'RNC' ? 'B01' : 'B02';
    const res = await fetch(`/Ncf/ProximoNcf?tipo=${tipo}`);
    const data = await res.json();
    if (!data.ok) {
        // Mostrar alerta roja antes de intentar vender
    } else if (data.disponibles < 100) {
        // Mostrar advertencia amarilla
    }
}
```

La API `GET /Ncf/ProximoNcf?tipo=` ya existe y está lista para este uso.

### Soporte para B14 y B15

Agregar al modelo `Clientes` un campo `TipoNcfPreferido` (default vacío = auto). Si tiene valor, usarlo en lugar de la lógica RNC/no-RNC. Permitir al usuario administrador asignarlo por cliente.

### Renovación automática con aviso por correo

Cuando `Disponibles < 50` o cuando `FechaVencimiento` esté a menos de 30 días, enviar un correo de alerta al administrador del sistema usando `IEmailSender` de ASP.NET Core Identity.

### Exportación 607 completa

El Excel actual tiene 6 columnas. El formato oficial DGII requiere al menos: RNC emisor, RNC receptor, NCF, NCF modificado, tipo de ingreso, fecha de comprobante, monto facturado sin impuestos, ITBIS, monto total. Ampliar `ExportarReporte607` para cumplir el formato oficial.

### Paginación en `Ncf/Index`

Si el sistema tiene muchos años de operación, la tabla de secuencias puede crecer. Agregar paginación o un filtro por tipo de comprobante.

### Historial de NCF consumidos por venta

Agregar un enlace desde la fila de la secuencia NCF hacia las ventas que la utilizaron. Útil para auditorías.

---

## ✅ Checklist de Configuración Inicial

Antes de procesar la primera venta, verificar:

- [ ] Crear secuencia B01 activa con rango autorizado por DGII y fecha de vencimiento válida
- [ ] Crear secuencia B02 activa con rango autorizado por DGII y fecha de vencimiento válida
- [ ] Verificar que los clientes empresariales tengan `TipoDocumento = "RNC"`
- [ ] Verificar que los clientes personas físicas tengan `TipoDocumento` diferente de `"RNC"`
- [ ] Corregir bug: asignar `Ncf = ncfGenerado` en la creación de facturas
- [ ] Corregir bug: actualizar `UlitmoNumero` en `GenerarProximoNcf()`
- [ ] Corregir enlace "Gestión NCF" en `Facturas/Index.cshtml`
- [ ] Verificar que el `numero_actual` inicial coincide con el autorizado por la DGII
- [ ] Comprobar que el formato del NCF (`B020000000001`) es correcto para la DGII

---

## 🗺️ Navegación al Módulo

```
Menú principal
└── Facturas  (/Facturas)
    └── Botón "Gestión NCF"  → /Ncf  (actualmente apunta a FacturasController.GestionNCF que es null)
                                       ↑ Corregir a asp-controller="Ncf" asp-action="Index"

/Ncf
├── Nueva Secuencia  →  /Ncf/Crear
├── Editar           →  /Ncf/Editar/{id}
├── ToggleActiva     →  POST /Ncf/ToggleActiva/{id}
└── Eliminar         →  POST /Ncf/Eliminar/{id}
```

También accesible indirectamente desde el mensaje de error en `PuntoVenta`:
> *"Configure una en Configuración → Comprobantes Fiscales (NCF)."*
