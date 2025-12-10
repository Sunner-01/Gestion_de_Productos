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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

