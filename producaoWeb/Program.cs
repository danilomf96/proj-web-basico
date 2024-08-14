using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using API.Models;

var builder = WebApplication.CreateBuilder(args);

// Adiciona suporte ao DbContext
builder.Services.AddDbContext<AppDataContext>(options =>
    options.UseSqlite("Data Source=meubanco.db"));

// Configura o CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

// Adiciona serviços do controlador
builder.Services.AddControllers();

var app = builder.Build();

// Usa o CORS
app.UseCors("AllowAll");

// Configura o roteamento e os controllers
app.UseRouting();

app.UseAuthorization();

// Mapeia os controllers
app.MapControllers();

app.Run();
