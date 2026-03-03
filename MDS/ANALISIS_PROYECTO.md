# Análisis rápido del proyecto `app-de-ventas`

## ¿De qué trata?
Aplicación web ASP.NET Core MVC (`net9.0`) para gestión comercial: autenticación de usuarios, catálogo de productos, clientes, ventas tipo punto de venta, facturación y reportes.

## Funcionalidades implementadas actualmente

1. **Autenticación y sesión con cookies**
   - Login, registro y logout.
   - Bloqueo temporal por intentos fallidos.
   - Gestión de claims del usuario autenticado.

2. **Módulo de clientes**
   - CRUD completo (listar, crear, editar, eliminar, detalle).
   - Filtros por búsqueda y tipo de documento.
   - Exportación a Excel.

3. **Módulo de productos**
   - CRUD completo.
   - Filtros por texto, categoría y estado.
   - Cambio de estado Activo/Inactivo.
   - Estadísticas de inventario y exportación a Excel.

4. **Módulo de ventas / punto de venta**
   - Pantalla de punto de venta con carrito.
   - Validaciones de cliente, stock y total.
   - Descuento por porcentaje o monto.
   - Registro de venta y generación de facturas.
   - Anulación de ventas con reversa de stock.
   - Reportes por periodo, cierre de caja y búsqueda de productos (JSON).

5. **Módulo de facturas**
   - Listado con filtros, detalle y anulación.
   - Reporte 607 y exportación Excel.
   - Endpoints de funcionalidades marcadas como "próximamente" (PDF, envío de correo, gestión NCF).

6. **Dashboard/Home**
   - KPIs de ventas del día y alerta de productos con stock bajo.

## Estado de “migraciones”

- En el repo **no existe carpeta `Migrations/` ni clases de migración de EF Core**.
- El proyecto parece apoyarse en un script SQL (`ventas_db.sql`) y conexión a MySQL.
- La referencia temporal más clara del estado de esquema/datos es el dump SQL con:
  - **Tiempo de generación: 02-03-2026 20:22:53**.

> Con lo disponible en este repositorio, esa es la mejor referencia para “última migración” (como dump de base de datos).
