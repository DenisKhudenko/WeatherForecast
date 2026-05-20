using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using System.Reflection;
using WeatherForecastWeb.BL.Services;
using WeatherForecastWeb.BL.Services.Interfaces;
using WeatherForecastWeb.DAL;
using WeatherForecastWeb.DAL.Extensions;
using WeatherForecastWeb.DAL.Repositories;
using WeatherForecastWeb.DAL.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// DI для СУБД
builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("Default"),
        b => b.MigrationsAssembly("WeatherForecastWeb.DAL")
    ));

// DI для DAL и BL
builder.Services.AddScoped<IWeatherForecastWebRepository, WeatherForecastWebRepository>();
builder.Services.AddScoped<IWeatherForecastWebService, WeatherForecastWebService>();
builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<ICityService, CityService>();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Описание swagger для возможности более тонко настраивать описание методов
builder.Services.AddSwaggerGen(swagger =>
{
swagger.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
{
    Title = "WeatherForecastWeb",
    Version = "v1"
});

    // Выполняем маппинг типов, без этого дата отображается в виде отдельных полей "year", "month", "day", "dayOfWeek"
    swagger.MapType<DateOnly>(() => new OpenApiSchema{
        Type = "string",
        Format = "date",
        Example = new OpenApiString($"{DateTime.Now:yyyy-MM-dd}")
    });

    // Включение генерации summary в UI Swagger
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    // Может упасть, если не находит файл, на всякий случай проверим его существование
    if (File.Exists(xmlPath))
    {
        swagger.IncludeXmlComments(xmlPath);
    }
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(swagger =>
    {
        swagger.SwaggerEndpoint("/swagger/v1/swagger.json", "WeatherForecastWeb v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Авто обновление после миграций
app.UseWeatherForecastWeb();

app.Run();
