using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using therapy.backend.Data;
using therapy.backend.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
// builder.Services.AddOpenApi();

// Swagger Services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Therapy API",
        Version = "v1",
        Description = "API for managing therapy sessions and related data."
    });
});

builder.Services.AddDbContext<TherapyDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("TherapyConnectionString")));

builder.Services.AddScoped<IUserRepository, SqlUserRepository>();
builder.Services.AddScoped<ISchoolRepository, SqlSchoolRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Therapy API V1");
        c.RoutePrefix = string.Empty; // Swagger UI available at app root
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();