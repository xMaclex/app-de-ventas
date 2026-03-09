using Microsoft.EntityFrameworkCore;
using ventaapp.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using OfficeOpenXml;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// EPPlus requires license context. In this project we use it non-commercially.
OfficeOpenXml.ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

/*/ Agregar DbContext
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Try to use MySQL by default; if we can't connect at startup, fall back to SQLite for development/demo purposes
bool mysqlAvailable = true;
try
{
    var tempOptions = new DbContextOptionsBuilder<VentasDbContext>()
        .UseMySql(connectionString, new MySqlServerVersion(new Version(10, 4, 28)))
        .Options;
    using (var tempCtx = new VentasDbContext(tempOptions))
    {
        // quick connectivity check
        mysqlAvailable = tempCtx.Database.CanConnect();
    }
}
catch
{
    mysqlAvailable = false;
}

if (mysqlAvailable)
{
    builder.Services.AddDbContext<VentasDbContext>(options =>
        options.UseMySql(
            connectionString,
            new MySqlServerVersion(new Version(10, 4, 28)),
            mySqlOptions => mySqlOptions.EnableRetryOnFailure()
        ));
}
else
{
    // fall back to SQLite in case MySQL is unreachable (helps prevent hangs during registration)
    var sqliteConn = "Data Source=ventas.db";
    builder.Services.AddDbContext<VentasDbContext>(options =>
        options.UseSqlite(sqliteConn));
    Console.WriteLine("[Warning] MySQL not reachable, falling back to SQLite at " + sqliteConn);
}

// ========================================
// CONFIGURAR COOKIE AUTHENTICATION
// ========================================
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Auth/Login";
        options.LogoutPath = "/Auth/Logout";
        options.AccessDeniedPath = "/Auth/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromHours(8);
        options.SlidingExpiration = true;
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
        options.Cookie.SameSite = SameSiteMode.Lax;
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// Auto-create database if it doesn't exist (MySQL or SQLite)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<VentasDbContext>();
    try
    {
        db.Database.EnsureCreated();
    }
    catch (Exception ex)
    {
        Console.WriteLine("Could not ensure database created: " + ex.Message);
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

// ========================================
// IMPORTANTE: UseAuthentication DEBE ir ANTES de UseAuthorization
// ========================================
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets(); */

//=======================
// CONEXION PHP MYADMIN
//=======================

// Agregar DbContext
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<VentasDbContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(10, 4, 28))));

// ========================================
// CONFIGURAR COOKIE AUTHENTICATION
// ========================================
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Auth/Login";
        options.LogoutPath = "/Auth/Logout";
        options.AccessDeniedPath = "/Auth/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromHours(8);
        options.SlidingExpiration = true;
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
        options.Cookie.SameSite = SameSiteMode.Lax;
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

// ========================================
// IMPORTANTE: UseAuthentication DEBE ir ANTES de UseAuthorization
// ========================================
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();


// ========================================
// CAMBIAR LA RUTA POR DEFECTO A Account/Login
// ========================================
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login}/{id?}")
    .WithStaticAssets();

app.Run();