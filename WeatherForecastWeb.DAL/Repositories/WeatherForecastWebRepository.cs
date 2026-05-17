using Microsoft.EntityFrameworkCore;
using WeatherForecastWeb.DAL;
using WeatherForecastWeb.DAL.Entities;
using WeatherForecastWeb.DAL.Repositories.Interfaces;

namespace WeatherForecastWeb.DAL.Repositories;

public class WeatherForecastWebRepository : IWeatherForecastWebRepository
{
    private readonly AppDBContext _context;

    public WeatherForecastWebRepository(AppDBContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<WeatherForecastWebEntity>> GetList()
        => await _context.WeatherForecasts.ToListAsync();

    public async Task<WeatherForecastWebEntity?> GetById(int id)
        => await _context.WeatherForecasts.FindAsync(id);

    public async Task<WeatherForecastWebEntity> Create(WeatherForecastWebEntity entity)
    {
        _context.WeatherForecasts.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<WeatherForecastWebEntity?> Update(WeatherForecastWebEntity entity)
    {
        var existing = await _context.WeatherForecasts.FindAsync(entity.Id);
        if (existing is null) return null;

        existing.Date = entity.Date;
        existing.TemperatureC = entity.TemperatureC;
        existing.City = entity.City;

        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> Delete(int id)
    {
        var entity = await _context.WeatherForecasts.FindAsync(id);
        if (entity is null) return false;

        _context.WeatherForecasts.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}

