# Módulo de Ventas / Punto de Venta — Documentación Actual

> **Estado del sistema:** Producción  
> **Conexión:** MySQL `server=127.0.0.1;port=3306;database=ventas_db;user=root`  
> **Framework:** ASP.NET Core MVC · Entity Framework Core · Bootstrap 5 · Chart.js 4.4

---

## 📁 Estructura de Archivos

```
ventaapp/
├── Controllers/
│   └── VentasController.cs          ← Controlador principal (8 acciones)
├── Models/
│   └── Venta.cs                     ← Modelo + VentaDetalle + PuntoVentaViewModel
└── Views/
    └── Ventas/
        ├── PuntoVenta.cshtml        ← Interfaz de caja (AJAX + carrito JS)
        ├── Index.cshtml             ← Historial con filtros y KPIs
        ├── Details.cshtml           ← Detalle completo de una venta
        ├── Reportes.cshtml          ← Análisis con Chart.js
        └── CierreCaja.cshtml        ← Cierre diario imprimible
```

---

## 🗄️ Modelo de Base de Datos

### Tabla `ventas_tb`

| Columna             | Tipo            | Notas                                     |
|---------------------|-----------------|-------------------------------------------|
| `id_venta`          | INT (PK)        | Auto-incremental                          |
| `fecha_venta`       | DATETIME        | Default: `DateTime.Now`                   |
| `id_cliente`        | INT (FK)        | Relación con tabla de clientes            |
| `subtotal`          | DECIMAL(10,2)   |                                           |
| `itbis`             | DECIMAL(10,2)   | Impuesto calculado por producto           |
| `descuento`         | DECIMAL(10,2)   | Default 0                                 |
| `tipo_descuento`    | VARCHAR(20)     | `"Monto"` o `"Porcentaje"`               |
| `total`             | DECIMAL(10,2)   | `subtotal + itbis - descuento`            |
| `metodo_pago`       | VARCHAR(30)     | Efectivo / Tarjeta / Transferencia / Cheque |
| `tipo_comprobante`  | VARCHAR(30)     | Campo presente pero campo NCF se gestiona vía `SecuenciaNcf` |
| `numero_comprobante`| INT             | Campo de referencia (no es el NCF)        |
| `estado`            | VARCHAR(20)     | `Completada` / `Anulada` / `Pendiente`   |
| `tipo_venta`        | VARCHAR(20)     | `Contado` / `Crédito`                    |
| `notas`             | VARCHAR(500)    | Observaciones libres + registro de anulaciones |
| `id_usuarios`       | INT (FK)        | Usuario autenticado via `ClaimTypes.NameIdentifier` |

### Migraciones necesarias (si la tabla ya existe sin los campos nuevos)

```sql
ALTER TABLE ventas_tb ADD COLUMN descuento DECIMAL(10,2) DEFAULT 0;
ALTER TABLE ventas_tb ADD COLUMN tipo_descuento VARCHAR(20) DEFAULT 'Monto';
ALTER TABLE ventas_tb ADD COLUMN tipo_venta VARCHAR(20) DEFAULT 'Contado';
ALTER TABLE ventas_tb ADD COLUMN notas VARCHAR(500) DEFAULT '';
```

O con EF Core:
```bash
cd ventaapp
dotnet ef migrations add AddVentasExtendedFields
dotnet ef database update
```

---

## 🧩 Clases del Modelo (`Venta.cs`)

### `Venta` — Modelo principal mapeado a `ventas_tb`

Relaciones declaradas:
- `Cliente` → `Clientes?`
- `Facturas` → `ICollection<Factura>`
- `Usuario` → `Usuarios?`
- `SecuenciaNcf` → `SecuenciaNcf?`

### `VentaDetalle` — Clase auxiliar para el carrito (no se persiste directamente)

Propiedades calculadas (no mapeadas a BD):
```csharp
public decimal Subtotal      => PrecioUnitario * Cantidad;
public decimal MontoImpuesto => Subtotal * (Impuesto / 100);
public decimal Total         => Subtotal + MontoImpuesto;
```

### `PuntoVentaViewModel` — ViewModel para la vista PuntoVenta

```csharp
public Venta Venta { get; set; }
public List<VentaDetalle> Carrito { get; set; }
public List<Clientes> Clientes { get; set; }
public List<Producto> Productos { get; set; }
// Propiedades calculadas: SubtotalCarrito, ItbisCarrito, TotalCarrito, DescuentoCalculado, TotalFinal
```

---

## 🎮 Controlador: `VentasController.cs`

Todas las acciones requieren usuario autenticado (`[Authorize]`).

### `GET /Ventas` — Historial de ventas (`Index`)

Filtros disponibles: `fechaDesde`, `fechaHasta`, `idCliente`, `metodoPago`, `estado`.

Carga en `ViewBag`:
- `VentasHoy`, `TotalHoy` — ventas completadas del día
- `VentasMes`, `TotalMes` — ventas completadas del mes en curso
- `Clientes` — lista para el dropdown de filtros

### `GET /Ventas/PuntoVenta` — Punto de venta

Devuelve un `PuntoVentaViewModel` con:
- Todos los clientes ordenados por nombre
- Productos con estado `"Activo"` ordenados por nombre

### `POST /Ventas/ProcesarVenta` — Procesar venta

Flujo completo dentro de una **transacción de base de datos**:

1. **Deserializar carrito** desde el campo oculto `carritoJson` (JSON).
2. **Validar cliente** con `AsNoTracking()` para evitar conflictos de rastreo de EF.
3. **Validar stock** de todos los productos en una sola consulta (`ToDictionaryAsync`).
4. **Calcular totales** — subtotal, ITBIS, descuento por monto o porcentaje, total final.
5. **Obtener usuario** desde `ClaimTypes.NameIdentifier`.
6. **Buscar secuencia NCF activa** según tipo de documento del cliente:
   - Cliente con `TipoDocumento == "RNC"` → tipo `B01`
   - Otros → tipo `B02`
   - Si no hay secuencia activa: rollback con mensaje de error.
7. **Guardar la venta** primero (para obtener el `IdVenta` real generado por la BD).
8. **Crear facturas y descontar stock** — una factura por cada unidad vendida por producto.
9. **Commit** de la transacción.

**Manejo de errores:** captura `DbUpdateException`, `JsonException` y `Exception` genérica, con rollback en todos los casos.

### `GET /Ventas/Details/{id}` — Detalle de venta

Carga `Cliente`, `Facturas` y `Facturas.Producto` con `Include/ThenInclude`.

### `POST /Ventas/Anular/{id}` — Anular venta

- Cambia el estado de la venta a `"Anulada"`.
- Agrega el motivo y fecha al campo `Notas`.
- Cambia el estado de cada `Factura` relacionada a `"Anulada"`.
- **Devuelve el stock** sumando 1 por cada factura (una factura = una unidad).

### `GET /Ventas/Reportes` — Reportes de análisis

Parámetro `periodo`: `"hoy"` (default) / `"semana"` / `"mes"`.

Carga en `ViewBag`:
- `TotalVentas`, `MontoTotal`, `PromedioVenta`
- `VentasPorMetodo` — agrupado por `MetodoPago` con cantidad y total
- `TopClientes` — top 5 por monto total, usando `NombreCompleto`

### `GET /Ventas/CierreCaja` — Cierre diario

Parámetro `fecha` (default: hoy). Filtra ventas `Completadas` de esa fecha.

Carga en `ViewBag`: `TotalVentas`, `MontoTotal`, `TotalEfectivo`, `TotalTarjeta`, `TotalTransferencia`.

### `GET /Ventas/BuscarProducto?termino=` — API AJAX

Devuelve JSON con hasta 10 productos activos que coincidan con nombre o código.  
Requiere mínimo 2 caracteres. Propiedades devueltas: `idProducto`, `codigoProducto`, `nombreProducto`, `precioVenta`, `impuesto`, `stock`.

---

## 🖥️ Vistas

### `PuntoVenta.cshtml`

La vista más compleja. Usa JavaScript puro (sin jQuery) con array `carrito[]` en memoria.

**Funciones JS implementadas:**

| Función | Descripción |
|---|---|
| `buscarProductos(termino)` | `fetch` al endpoint `BuscarProducto`, mínimo 2 chars |
| `mostrarResultados(productos)` | Genera lista clickeable con nombre, código, stock y precio |
| `agregarAlCarrito(...)` | Agrega o incrementa, respeta límite de stock |
| `actualizarCarrito()` | Redibuja toda la tabla del carrito |
| `cambiarCantidad(index, delta)` | Botones +/- con mínimo 1 |
| `actualizarCantidad(index, valor)` | Input directo, valida contra stock |
| `eliminarDelCarrito(index)` | `splice` y redibujo |
| `calcularTotales()` | Actualiza los 4 displays en tiempo real |

**Al enviar el formulario:** serializa el array `carrito[]` a JSON en el campo oculto `carritoJson`.

**Campos del formulario:**
- Cliente (select requerido)
- Método de pago (requerido): Efectivo / Tarjeta / Transferencia / Cheque
- Tipo de venta: Contado / Crédito
- Descuento + tipo (monto `$` o porcentaje `%`)
- Notas (opcional)

> ⚠️ La selección de Tipo de Comprobante (`B01`, `B02`, etc.) está **comentada** en el formulario. El tipo NCF se determina automáticamente en el controlador según el `TipoDocumento` del cliente.

### `Index.cshtml`

- 4 tarjetas KPI (ventas hoy, total hoy, ventas mes, total mes)
- Formulario de filtros (GET) con 5 parámetros
- Tabla con badge de estado (verde/rojo/amarillo)
- Botón anular que abre modal con campo motivo obligatorio
- El modal setea dinámicamente `form.action = /Ventas/Anular/${idVenta}`

> ⚠️ La columna "Comprobante" está **eliminada** de la tabla en la vista actual.

### `Details.cshtml`

- Muestra información de la venta, cliente y productos (a través de `Facturas`)
- Cada fila de producto muestra cantidad `1` y precio unitario (arquitectura: 1 factura = 1 unidad)
- Resumen financiero: subtotal, ITBIS, descuento (si > 0), total
- Modal de anulación integrado
- Botón imprimir (`window.print()`)

> ⚠️ Los campos `Usuarios`, `NumeroComprobante`, `TipoComprobante`, `Notas` y `Descuento/TipoDescuento` están **comentados** en la vista con `@Model.//Campo`. Pendiente de corrección de sintaxis Razor.

### `Reportes.cshtml`

- Selector de período con 3 botones (hoy / semana / mes)
- 4 KPIs + tabla de ventas por método de pago con barras de progreso
- Top 5 clientes con medallas 🥇🥈🥉
- Gráfico de barras (Chart.js) con ventas agrupadas por día
- Tabla detallada de todas las ventas del período

### `CierreCaja.cshtml`

- Selector de fecha (input `date` con máximo = hoy)
- Resumen: total transacciones, monto, ticket promedio, productos vendidos
- Tabla desglose por método de pago (Efectivo / Tarjeta / Transferencia)
- Gráfico de pastel (Chart.js) con distribución de métodos
- Resumen fiscal: subtotal, ITBIS, descuentos, total neto
- Desglose por tipo de venta (contado vs crédito)
- Tabla completa ordenada por hora
- CSS `@media print` para impresión limpia

---

## 🔗 Dependencias Externas (CDN)

```html
<!-- Bootstrap Icons -->
https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/font/bootstrap-icons.min.css

<!-- Chart.js (Reportes y CierreCaja) -->
https://cdn.jsdelivr.net/npm/chart.js@4.4.0/dist/chart.umd.min.js
```

---

## 🐛 Problemas Conocidos y Pendientes

### 1. Sintaxis Razor inválida en `Details.cshtml`

Varios campos usan `@Model.//Campo` que es sintaxis inválida. Deben corregirse:

```razor
<!-- ❌ Actual (no compila) -->
@Model.//Notas
@Model.//TipoDescuento
@Model.//Descuento.ToString("N2")
@Model.//Usuarios

<!-- ✅ Correcto -->
@Model.Notas
@Model.TipoDescuento
@Model.Descuento.ToString("N2")
@Model.Usuario?.NombreUsuario  <!-- o la propiedad que corresponda en Usuarios -->
```

### 2. Columna `NumeroComprobante` en `Index.cshtml`

La columna está en el `<thead>` pero no tiene celda `<td>` correspondiente en el `<tbody>`. Causa desalineación visual. Solución: eliminar el `<th>` o agregar el `<td>`.

### 3. Una factura por unidad vendida

El controlador crea una factura por cada **unidad** de cada producto (`for (int i = 0; i < item.Cantidad; i++)`). Esto puede generar muchas filas en `Details` si la cantidad es alta. Evaluar migrar a un modelo de factura con cantidad y precio total.

### 4. Devolución de stock al anular

Al anular, el controlador suma `+1` por cada factura. Esto es correcto con el diseño actual (1 factura = 1 unidad), pero quedaría roto si se cambia el modelo de facturas.

### 5. NCF en facturas no asignado

En `ProcesarVenta`, se genera el NCF (`ncfGenerado`) pero **no se asigna** a `factura.Ncf`. La propiedad `factura.Ncf` queda nula. Línea faltante:

```csharp
Ncf = ncfGenerado,  // ← Agregar en la creación de la factura
```

---

## 💡 Ideas de Implementación Futura

### Alta Prioridad

**Recibo / Ticket imprimible desde PuntoVenta**  
Después de procesar la venta, mostrar un modal con el resumen antes de redirigir a `Details`. Permitir imprimir un ticket de 80mm (formato térmica) directamente desde el modal.

**Pago en efectivo con cálculo de vuelto**  
En el punto de venta, cuando el método es "Efectivo", mostrar un campo "Monto recibido" y calcular el vuelto automáticamente antes de procesar.

**Refactorizar facturas: cantidad en lugar de una fila por unidad**  
Agregar columna `cantidad` a la tabla `facturas_tb` y crear una sola factura por producto (no por unidad). Esto simplifica `Details` y la lógica de devolución de stock en anulaciones.

### Mejoras al Punto de Venta

**Búsqueda de cliente en tiempo real**  
Igual que la búsqueda de productos: campo de texto con AJAX hacia un endpoint `BuscarCliente`, en lugar del dropdown actual. Útil cuando hay muchos clientes.

**Venta sin cliente (consumidor final)**  
Agregar un cliente genérico "Consumidor Final" predeterminado para ventas rápidas sin necesidad de seleccionar cliente.

**Historial de ventas recientes en PuntoVenta**  
Panel lateral o sección inferior con las últimas 5 ventas del día para referencia rápida.

**Agregar producto por cantidad directa**  
Al agregar al carrito, mostrar un pequeño modal o input para especificar la cantidad deseada, en lugar de tener que usar los botones +/-.

### Reportes y Análisis

**Exportar reportes a Excel/PDF**  
Botón en `Reportes.cshtml` y `CierreCaja.cshtml` para descargar los datos como `.xlsx` (usando EPPlus o ClosedXML) o `.pdf` (usando iText o QuestPDF).

**Dashboard de ventas en tiempo real**  
Nueva vista o widget en el layout principal con ventas del día actualizadas periódicamente (polling AJAX cada 30 segundos o SignalR).

**Comparativa de períodos**  
En Reportes, mostrar variación porcentual respecto al período anterior (ej: "ventas del mes vs mes anterior").

**Reporte de productos más vendidos**  
Agregar tabla en Reportes con top 10 productos por cantidad y por monto, útil para gestión de inventario.

### Funcionalidades Fiscales (RD)

**Asignar NCF generado a cada factura**  
Corregir el bug actual donde `ncfGenerado` no se persiste en `factura.Ncf`. Ver sección de problemas conocidos.

**Consulta de RNC en DGII**  
Al seleccionar un cliente con tipo de documento RNC, hacer una consulta a la API de la DGII para validar el RNC y autocompletar nombre fiscal.

**Secuencia NCF compartida entre ventas**  
En el diseño actual se genera un NCF por venta pero se crea una factura por unidad. Si se migra a una factura por producto, el NCF debe ser único por venta (ya está así lógicamente, pero no se está guardando).

### Seguridad y Auditoría

**Registro de auditoría completo**  
Tabla `auditoria_ventas` que registre cada cambio de estado (creación, anulación) con usuario, IP y timestamp.

**Roles y permisos granulares**  
Separar permisos: cajero (solo PuntoVenta), supervisor (puede anular), gerente (reportes y cierre de caja).

**Límite de descuento por rol**  
Configurar un porcentaje máximo de descuento por rol de usuario. Un cajero no puede dar más del 5%, un supervisor hasta el 20%.

---

## ✅ Checklist de Verificación

- [ ] Tabla `ventas_tb` tiene todas las columnas (ejecutar SQL o migración)
- [ ] Corregir sintaxis Razor `@Model.//Campo` en `Details.cshtml`
- [ ] Asignar `ncfGenerado` al campo `factura.Ncf` en `ProcesarVenta`
- [ ] Resolver columna "Comprobante" huérfana en `Index.cshtml`
- [ ] Verificar que exista un cliente con tipo "Consumidor Final" para ventas rápidas
- [ ] Confirmar que exista al menos una `SecuenciaNcf` activa tipo `B01` y `B02`
- [ ] Probar flujo completo: buscar producto → carrito → procesar → detalles
- [ ] Probar anulación y verificar devolución de stock
- [ ] Verificar que Chart.js carga en Reportes y CierreCaja
- [ ] Probar impresión desde CierreCaja (CSS print oculta controles)
