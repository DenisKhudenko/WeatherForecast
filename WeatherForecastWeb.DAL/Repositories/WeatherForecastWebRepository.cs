using Microsoft.EntityFrameworkCore;
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
    {
        return await _context.WeatherForecasts
            .AsNoTracking()
            .Include(entity => entity.City)
            .ToListAsync();
    }

    public async Task<WeatherForecastWebEntity?> GetById(int id)
    {
        return await _context.WeatherForecasts
            .AsNoTracking()
            .Include(entity => entity.City)
            .FirstOrDefaultAsync(entity => entity.Id == id);
    }

    public async Task<IEnumerable<WeatherForecastWebEntity>> GetByCityId(int cityId)
    {
        return await _context.WeatherForecasts
            .AsNoTracking()
            .Include(entity => entity.City)
            .Where(entity => entity.CityId == cityId)
            .ToListAsync();
    }

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

    public async Task<WeatherForecastWebEntity?> hasDateByCity(DateOnly date, CityEntity city)
    {
        return await _context.WeatherForecasts
            .AsNoTracking()
            .FirstOrDefaultAsync(value => value.Date == date && value.City == city);
    }
}