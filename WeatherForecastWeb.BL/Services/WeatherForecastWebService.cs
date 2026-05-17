using System.Data;
using WeatherForecastWeb.BL.Services.Interfaces;
using WeatherForecastWeb.BLL.DTO;
using WeatherForecastWeb.DAL.Entities;
using WeatherForecastWeb.DAL.Repositories.Interfaces;

namespace WeatherForecastWeb.BL.Services;

public class WeatherForecastWebService : IWeatherForecastWebService
{
    private readonly IWeatherForecastWebRepository _repository;

    private static readonly int _hotTemp = 26;
    private static readonly int _coldTemp = 15;

    public WeatherForecastWebService(IWeatherForecastWebRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<WeatherForecastWebDTO>> GetList()
    {
        var entities = await _repository.GetList();
        return entities.Select(MapListToDto);
    }

    public async Task<WeatherForecastWebDTO?> GetUnfairResultById(int id)
    {
        var entity = await _repository.GetById(id);

        bool isUnfairResult = true;
        return entity is null ? null : MapToDto(entity, isUnfairResult);
    }

    public async Task<WeatherForecastWebDTO?> GetById(int id)
    {
        var entity = await _repository.GetById(id);
        return entity is null ? null : MapToDto(entity);
    }

    public async Task<WeatherForecastWebDTO> Create(CreateWeatherForecastWebDTO dto)
    {
        var entity = new WeatherForecastWebEntity
        {
            Date = dto.Date,
            TemperatureC = dto.TemperatureC,
            City = dto.City
        };
        var created = await _repository.Create(entity);
        return MapToDto(created);
    }

    public async Task<WeatherForecastWebDTO?> Update(int id, CreateWeatherForecastWebDTO dto)
    {
        var entity = new WeatherForecastWebEntity
        {
            Id = id,
            Date = dto.Date,
            TemperatureC = dto.TemperatureC,
            City = dto.City
        };
        var updated = await _repository.Update(entity);
        return updated is null ? null : MapToDto(updated);
    }

    public async Task<bool> Delete(int id)
        => await _repository.Delete(id);

    // Если температура больше равна hot или меньше равна cold, выдаем нечестный результат(hot - 1)
    private static int CheckAndChangeTemp(int temp)
    {
        return (temp >= _hotTemp || temp <= _coldTemp) ? (_hotTemp - 1) : temp;
    }

    private static int getTemperatureF(int temperatureC)
    {
        return (32 + (int)(temperatureC / 0.5556));
    }

    private static WeatherForecastWebDTO MapToDto(WeatherForecastWebEntity e, bool isUnfairResult = false) => new()
    {
        Id = e.Id,
        Date = e.Date,
        TemperatureC = (isUnfairResult) ? (int) CheckAndChangeTemp(e.TemperatureC) : e.TemperatureC,
        TemperatureF = (isUnfairResult) ? (int) getTemperatureF(e.TemperatureC) : e.TemperatureF,
        City = e.City
    };

    private static WeatherForecastWebDTO MapListToDto(WeatherForecastWebEntity e) => new()
    {
        Id = e.Id,
        Date = e.Date,
        TemperatureC = e.TemperatureC,
        TemperatureF = e.TemperatureF,
        City = e.City
    };
}

