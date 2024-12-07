using APIproductos.Models;
using APIproductos.Services; // Asegúrate de tener esta línea para importar tus servicios
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Agregar DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), sqlOptions => sqlOptions.EnableRetryOnFailure()));

// Agregar tus servicios
builder.Services.AddScoped<VentasService>(); // Para Ventas
builder.Services.AddScoped<DevolucionesService>(); // Para Devoluciones
builder.Services.AddScoped<ServicesProduct>(); // Para Productos

// Otros servicios
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();
