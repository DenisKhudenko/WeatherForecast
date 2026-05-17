using Microsoft.EntityFrameworkCore;
using WeatherForecastWeb.App.Controllers;
using WeatherForecastWeb.BL.Services;
using WeatherForecastWeb.BL.Services.Interfaces;
using WeatherForecastWeb.DAL;
using WeatherForecastWeb.DAL.Repositories;
using WeatherForecastWeb.DAL.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// DI äëÿ ÑÓÁÄ
builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("Default"),
        b => b.MigrationsAssembly("WeatherForecastWeb.DAL")
    ));


// DI äëÿ DAL è BL
builder.Services.AddScoped<IWeatherForecastWebRepository, WeatherForecastWebRepository>();
builder.Services.AddScoped<IWeatherForecastWebService, WeatherForecastWebService>();

builder.Services.AddControllers().AddApplicationPart(typeof(WeatherForecastWebController).Assembly);
//builder.Services.AddControllers();

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
