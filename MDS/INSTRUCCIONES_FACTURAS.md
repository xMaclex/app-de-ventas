# 🧾 Guía de Implementación - Módulo de Facturas

## 📋 Archivos Generados

He creado el sistema completo de gestión de facturas con cumplimiento fiscal para República Dominicana:

### Modelo Actualizado:
- `Factura_Updated.cs` → Reemplazar en `ventaapp/Models/`
  - Incluye clase auxiliar: `SecuenciaNcf`

### Controlador:
- `FacturasController.cs` → Reemplazar el archivo vacío en `ventaapp/Controllers/`

### Vistas (crear carpeta `Views/Facturas/`):
- `Index.cshtml` → Historial de facturas con filtros avanzados
- `Details.cshtml` → Comprobante fiscal completo e imprimible
- `Reporte607.cshtml` → Reporte fiscal de ventas (DGII)

### Script SQL:
- `actualizar_facturas_tb.sql` → Para actualizar la base de datos

---

## 🚀 Pasos de Instalación

### 1. Actualizar el Modelo
```bash
cp Factura_Updated.cs ventaapp/Models/Factura.cs
```

**IMPORTANTE:** Este archivo incluye 2 clases:
- `Factura` - Modelo principal actualizado con validaciones
- `SecuenciaNcf` - Clase para gestión de rangos NCF

### 2. Actualizar la Base de Datos

Ejecuta el script SQL para agregar los campos necesarios:

```bash
mysql -u root -p ventas_db < actualizar_facturas_tb.sql
```

O ejecuta directamente en phpMyAdmin/MySQL Workbench:

```sql
ALTER TABLE facturas_tb 
ADD COLUMN motivo_anulacion VARCHAR(500) DEFAULT '' AFTER estado,
ADD COLUMN fecha_anulacion DATETIME NULL AFTER motivo_anulacion,
ADD COLUMN monto_total DECIMAL(10,2) DEFAULT 0 AFTER fecha_anulacion,
ADD COLUMN monto_itbis DECIMAL(10,2) DEFAULT 0 AFTER monto_total;
```

**Opcional - Tabla de Secuencias NCF:**
Si deseas gestionar los rangos de NCF autorizados por DGII:

```sql
CREATE TABLE secuencias_ncf_tb (
    id_secuencia INT AUTO_INCREMENT PRIMARY KEY,
    tipo_comprobante VARCHAR(3) NOT NULL,
    numero_inicial BIGINT NOT NULL,
    numero_final BIGINT NOT NULL,
    numero_actual BIGINT NOT NULL,
    fecha_vencimiento DATE NOT NULL,
    activa BOOLEAN DEFAULT TRUE
);
```

### 3. Actualizar el DbContext

Agregar la tabla de secuencias al DbContext (si la creaste):

```csharp
public DbSet<SecuenciaNcf> SecuenciasNcf { get; set; }
```

### 4. Copiar el Controlador
```bash
cp FacturasController.cs ventaapp/Controllers/FacturasController.cs
```

### 5. Crear Carpeta de Vistas
```bash
mkdir -p ventaapp/Views/Facturas

cp Facturas_Index.cshtml ventaapp/Views/Facturas/Index.cshtml
cp Facturas_Details.cshtml ventaapp/Views/Facturas/Details.cshtml
cp Facturas_Reporte607.cshtml ventaapp/Views/Facturas/Reporte607.cshtml
```

### 6. Verificar el Layout
El menú de navegación ya debe tener el enlace a Facturas en `_Layout.cshtml`

### 7. Ejecutar Migraciones (si usas EF Core Migrations)
```bash
cd ventaapp
dotnet ef migrations add UpdateFacturasFields
dotnet ef database update
```

### 8. Ejecutar la Aplicación
```bash
dotnet run
```

### 9. Probar el Módulo
- **Historial:** `https://localhost:5001/Facturas`
- **Reporte 607:** `https://localhost:5001/Facturas/Reporte607`

---

## ✨ Características Implementadas

### 📄 Vista Index (Historial de Facturas):

#### KPIs del Dashboard:
✅ **4 tarjetas estadísticas:**
  - Facturas del día
  - Facturas del mes
  - Monto total mensual
  - ITBIS recaudado del mes

#### Filtros Avanzados:
✅ **6 filtros disponibles:**
  - Fecha desde
  - Fecha hasta
  - Cliente específico
  - Tipo de comprobante (B01, B02, B14, B15)
  - Estado (Activa/Anulada)
  - Botón de búsqueda y limpiar

#### Tabla de Facturas:
✅ **Columnas:**
  - ID de factura
  - Número de factura
  - Fecha de emisión
  - Cliente
  - NCF completo
  - Tipo de comprobante con badge
  - Monto total
  - Estado con colores
  - Botones de acción

#### Acciones Disponibles:
✅ **Ver detalles** - Abre comprobante fiscal
✅ **Descargar PDF** - Genera PDF (próximamente)
✅ **Anular factura** - Modal con motivo requerido

### 📄 Vista Details (Comprobante Fiscal):

#### Diseño Profesional:
✅ **Header empresarial:**
  - Nombre de la empresa
  - RNC
  - Dirección
  - Número de factura
  - Fecha de emisión

#### NCF Destacado:
✅ **Información fiscal:**
  - Número de Comprobante Fiscal (NCF)
  - Tipo de comprobante con descripción
  - Alerta visual con colores

#### Datos del Cliente:
✅ **Información completa:**
  - Nombre completo
  - Tipo y número de documento
  - Correo electrónico
  - Número de venta asociado

#### Detalle de Productos:
✅ **Tabla de items:**
  - Código del producto
  - Descripción completa
  - Cantidad
  - Precio unitario
  - Subtotal

#### Cálculos Fiscales:
✅ **Resumen detallado:**
  - Subtotal (sin impuesto)
  - ITBIS con porcentaje
  - Total destacado con color

#### Estado de la Factura:
✅ **Indicador visual:**
  - ACTIVA - Badge verde
  - ANULADA - Badge rojo con motivo y fecha

#### Funcionalidades:
✅ **Botones de acción:**
  - Imprimir (CSS optimizado para impresión)
  - Descargar PDF
  - Enviar por correo
  - Anular factura (solo si está activa)

### 📊 Vista Reporte607 (Reporte Fiscal):

#### Selector de Período:
✅ Dropdown de mes (12 meses)
✅ Dropdown de año (últimos 5 años)
✅ Botón de consultar

#### Resumen Ejecutivo:
✅ **4 métricas principales:**
  - Total de facturas del período
  - Ventas sin ITBIS
  - ITBIS recaudado
  - Total general

#### Resumen por Tipo de Comprobante:
✅ **Tabla detallada:**
  - Tipo de NCF con descripción
  - Cantidad de facturas
  - Monto total por tipo
  - ITBIS por tipo
  - Barra de progreso con porcentaje
  - Fila de totales

#### Detalle en Formato DGII:
✅ **Tabla con formato oficial:**
  - RNC/Cédula del cliente
  - Tipo de identificación
  - Número de NCF
  - NCF modificado (para notas de crédito)
  - Tipo de ingreso
  - Fecha de emisión
  - Factura sin ITBIS
  - Factura con ITBIS
  - ITBIS desglosado
  - Total
  - Fila de totales al final

#### Funcionalidades:
✅ **Acciones disponibles:**
  - Imprimir (formato optimizado)
  - Exportar a Excel (próximamente)
  - CSS para impresión profesional

#### Notas Legales:
✅ **Información importante:**
  - Fecha límite de presentación
  - Verificación de NCF
  - Exclusión de facturas anuladas
  - Período de respaldo
  - Referencia a normativa DGII

---

## 🔧 Funcionalidades del Controlador

### FacturasController.cs incluye:

✅ **Index:** Historial con 6 filtros + KPIs
✅ **Details:** Muestra factura completa con relaciones
✅ **Anular:** Cambia estado, registra motivo y fecha
✅ **Reporte607:** Genera reporte fiscal mensual con agrupaciones
✅ **GestionNCF:** Placeholder para gestión de secuencias (futuro)
✅ **DescargarPDF:** Placeholder para generación PDF (futuro)
✅ **EnviarCorreo:** Placeholder para envío por email (futuro)

### Métodos Auxiliares:
✅ `ValidarNCF()` - Validación básica de formato NCF

---

## 💾 Modelo Actualizado

### Clase Factura:
✅ Todos los campos del modelo original
✅ **Campos nuevos:**
  - `MotivoAnulacion` (hasta 500 caracteres)
  - `FechaAnulacion` (nullable)
  - `MontoTotal` (decimal)
  - `MontoItbis` (decimal)

✅ **Propiedades calculadas (NotMapped):**
  - `EstadoDescripcion` - Descripción amigable del estado
  - `TipoNCFDescripcion` - Nombre completo del tipo de NCF

### Clase SecuenciaNcf:
✅ Para gestión futura de rangos NCF
✅ **Propiedades:**
  - TipoComprobante, NumeroInicial, NumeroFinal
  - NumeroActual, FechaVencimiento, Activa
  - Disponibles (calculado)
  - PorcentajeUsado (calculado)
  - CercaDeAgotarse (calculado)
  - Vencida (calculado)

---

## 🧾 Sistema de NCF (Números de Comprobante Fiscal)

### Tipos de Comprobantes Implementados:

1. **B01 - Crédito Fiscal**
   - Para empresas con RNC
   - Permite recuperación del ITBIS
   - Uso: Ventas a negocios

2. **B02 - Consumidor Final**
   - Para personas físicas
   - No permite recuperación de ITBIS
   - Uso: Ventas al por menor

3. **B14 - Régimen Especial**
   - Contribuyentes en régimen especial
   - Uso: Casos específicos

4. **B15 - Gubernamental**
   - Ventas al gobierno
   - Instituciones públicas
   - Uso: Licitaciones y contratos

### Formato de NCF:
- **Estructura:** B01YYYYMMDDNNNNNNNN
- **B01:** Tipo de comprobante (3 caracteres)
- **YYYYMMDD:** Fecha de emisión
- **NNNNNNNN:** Número secuencial (8 dígitos)
- **Total:** 19 caracteres

### Validaciones:
✅ Longitud exacta de 19 caracteres
✅ Comienza con el tipo correcto (B01, B02, etc.)
✅ Formato numérico después del tipo

---

## 📈 Reportes Fiscales Disponibles

### 1. Reporte 607 (Ventas):
- **Propósito:** Declarar ventas mensuales a la DGII
- **Contenido:**
  - Desglose por tipo de comprobante
  - Datos del cliente (RNC/Cédula)
  - Montos con y sin ITBIS
  - ITBIS recaudado
- **Presentación:** Mensual (día 20)
- **Formato:** Según Norma 01-07 DGII

### 2. Reporte 606 (Compras):
- **Estado:** No implementado (es para compras)
- **Nota:** Se implementaría si gestionas compras

### 3. Declaración de ITBIS:
- **Estado:** Datos disponibles en Reporte 607
- **Uso:** Los totales del 607 se usan para la declaración

---

## 🎨 Características de UX/UI

### Historial de Facturas:
✅ Filtros expandibles y claros
✅ KPIs en tarjetas de colores
✅ Badges para tipos y estados
✅ Modal para anular con motivo
✅ Iconos descriptivos (Bootstrap Icons)

### Comprobante Fiscal:
✅ Diseño profesional tipo factura
✅ NCF destacado en alerta azul
✅ Información organizada en secciones
✅ Tabla de productos limpia
✅ Totales destacados con color
✅ CSS optimizado para impresión
✅ Zona de peligro para anular

### Reporte 607:
✅ Selector de período intuitivo
✅ KPIs del mes en tarjetas
✅ Gráficas de progreso por tipo
✅ Tabla con formato DGII oficial
✅ Notas legales importantes
✅ Botones de exportación
✅ Impresión profesional

---

## 🔐 Seguridad y Validaciones

✅ **AntiForgeryToken** en todos los formularios POST
✅ **Validación de ModelState** en el servidor
✅ **Try-catch** en operaciones críticas
✅ **Motivo obligatorio** para anulaciones
✅ **Registro de auditoría** (fecha/hora de anulación)
✅ **Estados inmutables** (no se puede reactivar)
✅ **Validación de NCF** con formato correcto

---

## 🆘 Solución de Problemas

### Error: Columnas no existen en facturas_tb
**Solución:** Ejecuta el script `actualizar_facturas_tb.sql`

### No se muestran los montos en las facturas
**Solución:** 
- Verifica que los campos `monto_total` y `monto_itbis` estén llenos
- Actualiza las facturas existentes con los valores correctos

### El Reporte 607 está vacío
**Solución:**
- Verifica que existan facturas en el mes seleccionado
- Asegúrate de que las facturas estén en estado "Activa"
- Comprueba que los montos no sean 0

### Error al anular factura
**Solución:**
- Verifica que la factura esté en estado "Activa"
- Asegúrate de proporcionar un motivo
- Comprueba que los campos de anulación existan

---

## 📝 Funcionalidades Pendientes (Para Futuras Implementaciones)

### Generación de PDF:
Actualmente es un placeholder. Para implementar:
- Instalar: `iTextSharp` o `QuestPDF`
- Generar PDF con diseño de factura
- Incluir código QR con NCF
- Logo de la empresa

### Envío por Correo:
Actualmente es un placeholder. Para implementar:
- Configurar SMTP en `appsettings.json`
- Usar `MailKit` o `System.Net.Mail`
- Template HTML del correo
- Adjuntar PDF de factura

### Gestión de Secuencias NCF:
Actualmente es un placeholder. Para implementar:
- CRUD completo de secuencias
- Validación de rangos autorizados
- Alertas cuando se agoten (< 100)
- Alertas de vencimiento
- Auto-incremento de secuencias

### Nota de Crédito:
Para implementar:
- Nueva tabla o campo en facturas
- Relación con factura original
- NCF modificado en Reporte 607
- Restar del total de ventas

### Comprobante Fiscal Electrónico (e-CF):
Para implementar:
- Integración con API de DGII
- Firma digital
- Timbre fiscal electrónico
- Envío automático a DGII

---

## ✅ Checklist de Verificación

- [ ] Modelo Factura actualizado con nuevos campos
- [ ] Base de datos actualizada (script SQL)
- [ ] Controlador FacturasController copiado
- [ ] Carpeta Views/Facturas creada
- [ ] 3 vistas copiadas correctamente
- [ ] Aplicación ejecutándose sin errores
- [ ] Navegación a /Facturas funciona
- [ ] Historial muestra facturas correctamente
- [ ] Filtros operativos
- [ ] KPIs calculándose bien
- [ ] Detalles muestra comprobante fiscal
- [ ] Se pueden anular facturas
- [ ] Reporte 607 genera datos correctos
- [ ] Totales en Reporte 607 cuadran
- [ ] Impresión funciona (Details y Reporte607)
- [ ] Badges y estilos se ven bien

---

## 🎯 Cumplimiento Fiscal - República Dominicana

### Normativa Aplicable:
✅ **Ley 11-92** - Código Tributario
✅ **Norma 01-07** - Norma General sobre Comprobantes Fiscales
✅ **Decreto 254-06** - Reglamento de Comprobantes Fiscales

### Requisitos Cubiertos:
✅ NCF válido de 19 caracteres
✅ Tipos de comprobante correctos
✅ Información del emisor (RNC, nombre, dirección)
✅ Información del receptor (documento, nombre)
✅ Fecha de emisión
✅ Detalle de productos/servicios
✅ Desglose de ITBIS
✅ Totales claros
✅ Reporte 607 en formato DGII
✅ Conservación de registros

### Pendiente (Para Producción):
⚠️ Rangos de NCF autorizados por DGII
⚠️ Integración con sistema de la DGII
⚠️ Firma digital (e-CF)
⚠️ Secuencias por año fiscal
⚠️ Validación de vencimiento de NCF

---

## 🎉 ¡Módulo de Facturas Completado!

Este módulo incluye:

- ✅ **Historial completo** de facturas con filtros
- ✅ **Comprobante fiscal** profesional e imprimible
- ✅ **Reporte 607** con formato DGII oficial
- ✅ **Anulación de facturas** con registro de motivo
- ✅ **4 tipos de NCF** (B01, B02, B14, B15)
- ✅ **Validaciones fiscales** según normativa RD
- ✅ **Desglose de ITBIS** completo
- ✅ **Estadísticas** y KPIs en tiempo real
- ✅ **Filtros avanzados** por múltiples criterios
- ✅ **CSS para impresión** optimizado
- ✅ **Diseño responsive** con Bootstrap 5
- ✅ **Preparado para extensión** (PDF, email, secuencias)

¡Ahora tienes un sistema de facturación fiscal completo y profesional!
