# 🔐 INSTRUCCIONES DE INSTALACIÓN - LOGIN/REGISTER SYSTEM

## ⚠️ IMPORTANTE - ERRORES ENCONTRADOS EN TU PROYECTO

He detectado los siguientes errores en tu código que DEBES corregir:

### 1. Usuarios.cs (LÍNEA 7 - ERROR CRÍTICO)
```csharp
// ❌ INCORRECTO (tu código actual):
[Table("ventas_db")]

// ✅ CORRECTO:
[Table("usuarios_tb")]
```

### 2. VentasDbContext.cs
El DbSet está bien nombrado como `Usuario` (singular), así que el código está correcto.

### 3. Conflictos de Merge en archivos
Debes resolver los conflictos en:
- `appsettings.json`
- `appsettings.Development.json`
- `_Layout.cshtml`

---

## 📋 PASOS DE INSTALACIÓN

### PASO 1: INSTALAR PAQUETE BCRYPT

Abre la **Package Manager Console** y ejecuta:

```powershell
Install-Package BCrypt.Net-Next
```

O desde la terminal:

```bash
dotnet add package BCrypt.Net-Next
```

---

### PASO 2: REEMPLAZAR ARCHIVOS

Copia los siguientes archivos a tu proyecto VentaApp:

#### A. Modelos y ViewModels

1. **`Usuarios.cs`** → Reemplazar en `ventaapp/Models/Usuarios.cs`
   - ⚠️ Corrige la línea 7: `[Table("usuarios_tb")]`

2. **`LoginRegisterViewModels.cs`** → Crear en `ventaapp/ViewModels/LoginRegisterViewModels.cs`
   - Crear la carpeta `ViewModels` si no existe

#### B. Controlador

3. **`AccountController.cs`** → Crear en `ventaapp/Controllers/AccountController.cs`

#### C. Vistas

4. **`Login.cshtml`** → Crear en `ventaapp/Views/Account/Login.cshtml`
   - Crear la carpeta `Views/Account` si no existe

5. **`Register.cshtml`** → Crear en `ventaapp/Views/Account/Register.cshtml`

6. **`_LoginPartial.cshtml`** → Crear en `ventaapp/Views/Shared/_LoginPartial.cshtml`

7. **`_Layout.cshtml`** → REEMPLAZAR en `ventaapp/Views/Shared/_Layout.cshtml`
   - ⚠️ Esto eliminará los conflictos de merge

#### D. Configuración

8. **`Program.cs`** → REEMPLAZAR en `ventaapp/Program.cs`
   - ⚠️ Esto agregará la autenticación

---

### PASO 3: RESOLVER CONFLICTOS EN APPSETTINGS

En `appsettings.json` y `appsettings.Development.json`, elimina los marcadores de conflicto y deja solo:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "server=127.0.0.1;port=3306;database=ventas_db;user=root;password=TU_PASSWORD;SslMode=none;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

Reemplaza `TU_PASSWORD` con tu contraseña de MySQL.

---

### PASO 4: GENERAR HASHES DE CONTRASEÑA

Para crear usuarios de prueba, necesitas generar hashes BCrypt.

#### Opción A: Online (Más rápido)
1. Ve a: https://bcrypt-generator.com/
2. Contraseña: `Admin123!`
3. Rounds: `11`
4. Copia el hash generado

#### Opción B: Programa C#
Crea un proyecto de consola temporal:

```csharp
using System;

class Program
{
    static void Main()
    {
        string password = "Admin123!";
        string hash = BCrypt.Net.BCrypt.HashPassword(password);
        Console.WriteLine($"Hash: {hash}");
    }
}
```

---

### PASO 5: CREAR USUARIOS EN LA BASE DE DATOS

Ejecuta este script SQL en MySQL:

```sql
USE ventas_db;

-- Usuario Administrador
INSERT INTO usuarios_tb (
    nombre, apellidos, tipo_documento, numero_documento, 
    numero_telefono, numero_celular, username, clave, 
    email, estado, rol, fecha_registro, intentos_fallidos
) VALUES (
    'Admin', 'Sistema', 'Cédula', '001-0000000-0',
    '809-000-0000', '829-000-0000', 'admin',
    'AQUI_VA_EL_HASH_BCRYPT',  -- ⚠️ Reemplazar con hash real
    'admin@ventaapp.com', 'Activo', 'Administrador', NOW(), 0
);

-- Usuario Vendedor
INSERT INTO usuarios_tb (
    nombre, apellidos, tipo_documento, numero_documento, 
    numero_telefono, numero_celular, username, clave, 
    email, estado, rol, fecha_registro, intentos_fallidos
) VALUES (
    'Juan', 'Pérez', 'Cédula', '001-1234567-8',
    '809-555-1111', '829-555-1111', 'vendedor',
    'AQUI_VA_EL_HASH_BCRYPT',  -- ⚠️ Reemplazar con hash real
    'vendedor@ventaapp.com', 'Activo', 'Vendedor', NOW(), 0
);
```

---

### PASO 6: COMPILAR Y EJECUTAR

1. Compila el proyecto:
```bash
dotnet build
```

2. Si hay errores, asegúrate de:
   - Haber instalado BCrypt.Net-Next
   - Haber corregido `[Table("usuarios_tb")]` en Usuarios.cs
   - No tener conflictos de merge

3. Ejecuta:
```bash
dotnet run
```

4. La aplicación debería abrir en `https://localhost:XXXX/Account/Login`

---

### PASO 7: PROBAR EL SISTEMA

**Credenciales de prueba:**
- Usuario: `admin`
- Contraseña: `Admin123!`

**O:**
- Usuario: `vendedor`
- Contraseña: `Admin123!`

---

## ✅ VERIFICACIÓN FINAL

- [ ] BCrypt.Net-Next instalado
- [ ] Usuarios.cs corregido (`[Table("usuarios_tb")]`)
- [ ] ViewModels creados
- [ ] AccountController creado
- [ ] Vistas Login y Register creadas
- [ ] _LoginPartial creado
- [ ] _Layout.cshtml actualizado
- [ ] Program.cs con autenticación
- [ ] Conflictos de merge resueltos
- [ ] Usuarios de prueba en BD con hash BCrypt
- [ ] Aplicación compila sin errores
- [ ] Login funciona correctamente

---

## 🔒 CARACTERÍSTICAS DEL SISTEMA

✅ Login con validación de credenciales
✅ Register con validaciones de campos
✅ Hash de contraseñas con BCrypt
✅ Bloqueo de cuenta tras 5 intentos fallidos
✅ Control de sesión con cookies HttpOnly
✅ Sistema de roles (Administrador, Gerente, Vendedor)
✅ Protección de rutas con [Authorize]
✅ Menú de usuario en el navbar
✅ Logout funcional

---

## 🚨 ERRORES COMUNES Y SOLUCIONES

### Error: "The entity type 'Usuarios' requires a primary key to be defined"
**Solución:** Asegúrate de que `[Table("usuarios_tb")]` esté correcto en Usuarios.cs

### Error: "No service for type 'Microsoft.AspNetCore.Authentication.IAuthenticationService'"
**Solución:** Asegúrate de que `UseAuthentication()` esté ANTES de `UseAuthorization()` en Program.cs

### Error: "Unable to resolve service for type 'BCrypt'"
**Solución:** Instala el paquete `BCrypt.Net-Next`

### Error: Usuario no puede iniciar sesión
**Solución:** Verifica que el hash en la BD sea válido (generado con BCrypt rounds=11)

---

## 📞 PRÓXIMOS PASOS OPCIONALES

Una vez funcionando el Login/Register, puedes:

1. Crear el CRUD de Usuarios (solo para Administradores)
2. Agregar recuperación de contraseña por email
3. Implementar 2FA (autenticación de dos factores)
4. Agregar roles personalizados
5. Implementar permisos granulares por módulo

---

¡Listo! Tu sistema de autenticación está completo y listo para usar. 🎉
