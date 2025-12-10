using BackEnd_G_P.Data;
using BackEnd_G_P.Models;
using BackEnd_G_P.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configurar base de datos en memoria
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("InventarioDB"));

// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactAppPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

// Registrar servicios
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<RolService>();
builder.Services.AddScoped<ProveedorService>();
builder.Services.AddScoped<CategoriaService>();
builder.Services.AddScoped<ProductoService>();
builder.Services.AddScoped<MovimientoInventarioService>();
builder.Services.AddScoped<OrdenService>();
builder.Services.AddScoped<ReporteService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Sembrar datos iniciales
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    // Roles
    if (!context.Roles.Any())
    {
        context.Roles.AddRange(
            new Rol { Id = 1, Nombre = "Administrador" },
            new Rol { Id = 2, Nombre = "Empleado" }
        );
        context.SaveChanges();
    }

    // Usuarios
    if (!context.Usuarios.Any())
    {
        context.Usuarios.AddRange(
            new Usuario { Id = 1, NombreUsuario = "admin", Email = "admin@gmail.com", PasswordHash = "pass123.", RolId = 1, EstaActivo = true },
            new Usuario { Id = 2, NombreUsuario = "empleado1", Email = "empleado1@gmail.com", PasswordHash = "pass123.", RolId = 2, EstaActivo = true }
        );
        context.SaveChanges();
    }

    // Categorías
    if (!context.Categorias.Any())
    {
        context.Categorias.AddRange(
            new Categoria { Id = 1, Nombre = "Laptops", Descripcion = "Computadoras portátiles de todas las marcas" },
            new Categoria { Id = 2, Nombre = "Celulares", Descripcion = "Smartphones y teléfonos móviles" },
            new Categoria { Id = 3, Nombre = "Accesorios", Descripcion = "Accesorios para dispositivos electrónicos" },
            new Categoria { Id = 4, Nombre = "Componentes", Descripcion = "Componentes de hardware para PC" },
            new Categoria { Id = 5, Nombre = "Audio", Descripcion = "Audífonos, bocinas y equipos de audio" }
        );
        context.SaveChanges();
    }

    // Proveedores
    if (!context.Proveedores.Any())
    {
        context.Proveedores.AddRange(
            new Proveedor { Id = 1, Nombre = "IRIS COMPUTER", Direccion = "Av. Tecnológica 123", Telefono = "7012345", Email = "iris@gmail.com" },
            new Proveedor { Id = 2, Nombre = "TITAN S.A.", Direccion = "Calle Comercio 456", Telefono = "7023456", Email = "titan@gmail.com" },
            new Proveedor { Id = 3, Nombre = "Mundo Cell", Direccion = "Zona Industrial 789", Telefono = "7034567", Email = "mundocell@gmail.com" }
        );
        context.SaveChanges();
    }

    // Productos
    if (!context.Productos.Any())
    {
        context.Productos.AddRange(
            // Laptops
            new Producto { Id = 1, Nombre = "Lenovo Legion 5 Pro", Descripcion = "Laptop gaming con RTX 3060, 16GB RAM, 512GB SSD", Precio = 12500.00m, StockActual = 15, CategoriaId = 1, ProveedorId = 1 },
            new Producto { Id = 2, Nombre = "Dell XPS 13", Descripcion = "Laptop empresarial, Intel i7 11va Gen, 16GB RAM", Precio = 13800.00m, StockActual = 10, CategoriaId = 1, ProveedorId = 2 },
            new Producto { Id = 3, Nombre = "HP Pavilion 15", Descripcion = "Laptop uso general, Intel i5, 8GB RAM, 256GB SSD", Precio = 6500.00m, StockActual = 25, CategoriaId = 1, ProveedorId = 3 },
            
            // Celulares
            new Producto { Id = 4, Nombre = "Realme 13 pro", Descripcion = "128GB, Cámara 48MP", Precio = 9500.00m, StockActual = 20, CategoriaId = 2, ProveedorId = 1 },
            new Producto { Id = 5, Nombre = "Samsung Galaxy S23", Descripcion = "256GB, Snapdragon 8 Gen 2, Cámara 50MP", Precio = 8200.00m, StockActual = 18, CategoriaId = 2, ProveedorId = 2 },
            new Producto { Id = 6, Nombre = "Redmi Note 15 Pro", Descripcion = "256GB, Cámara 200MP, Carga rápida 67W", Precio = 2800.00m, StockActual = 35, CategoriaId = 2, ProveedorId = 3 },
            
            // Accesorios
            new Producto { Id = 7, Nombre = "Mouse Logitech MX Master 3", Descripcion = "Mouse inalámbrico ergonómico, 7 botones programables", Precio = 850.00m, StockActual = 40, CategoriaId = 3, ProveedorId = 1 },
            new Producto { Id = 8, Nombre = "Teclado Mecánico Keychron K2", Descripcion = "Teclado mecánico RGB, switches Blue, wireless", Precio = 950.00m, StockActual = 30, CategoriaId = 3, ProveedorId = 2 },
            new Producto { Id = 9, Nombre = "Webcam Logitech C920", Descripcion = "Full HD 1080p, micrófono integrado", Precio = 680.00m, StockActual = 25, CategoriaId = 3, ProveedorId = 3 },
            
            // Componentes
            new Producto { Id = 10, Nombre = "SSD Samsung 980 PRO 1TB", Descripcion = "NVMe M.2, velocidad 7000MB/s lectura", Precio = 1200.00m, StockActual = 50, CategoriaId = 4, ProveedorId = 1 },
            new Producto { Id = 11, Nombre = "RAM Corsair Vengeance 16GB", Descripcion = "DDR4 3200MHz, Kit 2x8GB RGB", Precio = 650.00m, StockActual = 60, CategoriaId = 4, ProveedorId = 2 },
            new Producto { Id = 12, Nombre = "Fuente EVGA 650W Gold", Descripcion = "80 Plus Gold, modular, 650W", Precio = 890.00m, StockActual = 20, CategoriaId = 4, ProveedorId = 3 },
            
            // Audio
            new Producto { Id = 13, Nombre = "AirPods Pro 2", Descripcion = "Cancelación de ruido activa, estuche MagSafe", Precio = 2200.00m, StockActual = 45, CategoriaId = 5, ProveedorId = 1 },
            new Producto { Id = 14, Nombre = "Sony WH-1000XM5", Descripcion = "Audífonos premium, 30hrs batería", Precio = 3500.00m, StockActual = 15, CategoriaId = 5, ProveedorId = 2 },
            new Producto { Id = 15, Nombre = "JBL Flip 6", Descripcion = "Bocina Bluetooth portátil, resistente al agua IP67", Precio = 1100.00m, StockActual = 30, CategoriaId = 5, ProveedorId = 3 }
        );
        context.SaveChanges();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("ReactAppPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();

