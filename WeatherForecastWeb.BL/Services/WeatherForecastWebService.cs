using System.Collections.Generic;
using System.Data;
using WeatherForecastWeb.BL.Services.Interfaces;
using WeatherForecastWeb.BLL.DTO;
using WeatherForecastWeb.DAL.Entities;
using WeatherForecastWeb.DAL.Repositories.Interfaces;

namespace WeatherForecastWeb.BL.Services;

public class WeatherForecastWebService : IWeatherForecastWebService
{
    private readonly IWeatherForecastWebRepository _repository;
    private readonly ICityRepository _cityRepository;

    private static readonly int _hotTemp = 26;
    private static readonly int _coldTemp = 15;

    public WeatherForecastWebService(IWeatherForecastWebRepository repository
        , ICityRepository cityRepository)
    {
        _repository = repository;
        _cityRepository = cityRepository;
    }

    public async Task<IEnumerable<WeatherForecastWebDTO>> GetList(bool? isTodayWeather)
    {
        var entities = await _repository.GetList();

        if (isTodayWeather == true)
        {
            entities = entities.Where(value => value.Date == DateOnly.FromDateTime(DateTime.Now));
        }

        return entities.Select(MapToDto).ToList();
    }

    public async Task<WeatherForecastWebDTO?> GetById(int id)
    {
        var entity = await _repository.GetById(id);
        return entity is null ? null : MapToDto(entity);
    }

    public async Task<IEnumerable<WeatherForecastWebDTO>> GetByCityId(int cityId)
    {
        var city = await _cityRepository.GetById(cityId);
        if (city == null) throw new Exception("City not found, enter valid value");

        var entities = await _repository.GetByCityId(cityId);
        return entities.Select(e => MapToDto(e));
    }

    public async Task<WeatherForecastWebDTO> Create(CreateWeatherForecastWebDTO dto)
    {
        var city = await _cityRepository.GetById(dto.CityId);
        if (city == null) throw new Exception("City not found, create city before creating weather forecast");

        var entity = new WeatherForecastWebEntity
        {
            Date = dto.Date,
            TemperatureC = dto.TemperatureC,
            City = city
        };

        var created = await _repository.Create(entity);
        return MapToDto(created);
    }

    public async Task<WeatherForecastWebDTO?> Update(int id, CreateWeatherForecastWebDTO dto)
    {
        var city = await _cityRepository.GetById(id);
        if (city == null) throw new Exception("City not found, enter valid value");

        var entity = new WeatherForecastWebEntity
        {
            Id = id,
            Date = dto.Date,
            TemperatureC = dto.TemperatureC,
            City = city
        };
        var updated = await _repository.Update(entity);
        return updated is null ? null : MapToDto(updated);
    }

    public async Task<bool> Delete(int id)
        => await _repository.Delete(id);

    // Если температура больше равна hot или меньше равна cold, выдаем нечестный результат рандом между cold и hot
    private static int CheckAndChangeTemp(int temp)
    {
        Random random = new Random();
        return (temp >= _hotTemp || temp <= _coldTemp) ? random.Next(_coldTemp + 1, _hotTemp - 1) : temp;
    }

    private static int getTemperatureF(int temperatureC)
    {
        return (32 + (int)(temperatureC / 0.5556));
    }

    private static WeatherForecastWebDTO MapToDto(WeatherForecastWebEntity entity)
    {
        var TemperatureC = CheckAndChangeTemp(entity.TemperatureC);
        var TemperatureF = getTemperatureF(TemperatureC);
        return new()
        {
            Id = entity.Id,
            Date = entity.Date,
            TemperatureC = TemperatureC,
            TemperatureF = TemperatureF,
            City = entity.City.ToString()
        };
    }

}

