using CanadianCitiesAPI.Controllers;
using CanadianCitiesAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => 
    {
        c.SwaggerDoc("v1",
            new OpenApiInfo
            {
                Title = "My API - V1",
                Version = "v1"
            }
        );

        var filePath = Path.Combine(System.AppContext.BaseDirectory, "CanadianCitiesAPI.xml");
        c.IncludeXmlComments(filePath);

    });

// add/register a DbContext Service so it can be injected into calling objects at runtime
builder.Services.AddDbContext<ApiDbContext>(options =>
{
    options.UseInMemoryDatabase("States");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
