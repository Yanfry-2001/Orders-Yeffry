using Microsoft.EntityFrameworkCore;
using Orders.Backend.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Inyeccion del DataContext
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LocalConnection")));

// Inyección de dependencias para repositorios y unit of work genéricos
builder.Services.AddScoped(typeof(Orders.Backend.Repositories.Interfaces.IGenericRepository<>), typeof(Orders.Backend.Repositories.Implementations.GenericRepository<>));
builder.Services.AddScoped(typeof(Orders.Backend.UnitsOfWork.Interfaces.IGenericUnitOfWork<>), typeof(Orders.Backend.UnitsOfWork.Implementations.GenericUnitOfWork<>));

// Configuración de CORS (ajustar según necesidades)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
