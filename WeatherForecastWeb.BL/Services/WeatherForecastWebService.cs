using System.Data;
using WeatherForecastWeb.BL.Extensions;
using WeatherForecastWeb.BL.Services.Interfaces;
using WeatherForecastWeb.BLL.DTO;
using WeatherForecastWeb.DAL.Entities;
using WeatherForecastWeb.DAL.Repositories.Interfaces;

namespace WeatherForecastWeb.BL.Services;

public class WeatherForecastWebService : IWeatherForecastWebService
{
    private readonly IWeatherForecastWebRepository _repository;
    private readonly ICityRepository _cityRepository;

    public WeatherForecastWebService(IWeatherForecastWebRepository repository
        , ICityRepository cityRepository)
    {
        _repository = repository;
        _cityRepository = cityRepository;
    }

    public async Task<IReadOnlyCollection<WeatherForecastWebDTO>> GetList(bool? isTodayWeather)
    {
        var entities = await _repository.GetList();

        // Если передан параметр запроса isTodayWeather = true, то отображаем прогноз только за сегодня
        if (isTodayWeather == true)
        {
            entities = entities
                .Where(value => value.Date == DateOnly.FromDateTime(DateTime.Now));
        }

        entities = entities
            .OrderBy(value => value.CityId)
            .OrderByDescending(value => value.Date);

        return entities.Select(value => value.MapToDTO()).ToList();
    }

    public async Task<WeatherForecastWebDTO?> GetById(int id)
    {
        var entity = await _repository.GetById(id);
        return entity is null ? null : entity.MapToDTO();
    }

    public async Task<IReadOnlyCollection<WeatherForecastWebDTO>> GetByCityId(int cityId)
    {
        var city = await _cityRepository.GetById(cityId);
        if (city == null) throw new Exception("City not found, enter valid value");

        var entities = await _repository.GetByCityId(cityId);
        return entities.Select(entity => entity.MapToDTO()).ToList();
    }

    public async Task<WeatherForecastWebDTO> Create(CreateWeatherForecastWebDTO dto)
    {
        var city = await _cityRepository.GetById(dto.CityId);
        if (city == null) throw new Exception("City not found, create city before creating weather forecast");

        // Если уже существует прогноз по городу за переданную дату, то просто обновляем данные
        var value = await _repository.hasDateByCity(dto.Date, city);

        if (value != null) return await Update(value.Id, dto, city);

        var entity = new WeatherForecastWebEntity
        {
            Date = dto.Date,
            TemperatureC = dto.TemperatureC,
            City = city
        };

        var created = await _repository.Create(entity);
        return created.MapToDTO();
    }

    public async Task<WeatherForecastWebDTO> Update(int id
        , CreateWeatherForecastWebDTO dto
        , CityEntity? city = null)
    {
        if (city == null)
        {
            city = await _cityRepository.GetById(dto.CityId);
            if (city == null) throw new Exception("City not found, enter valid value");
        }

        var entity = new WeatherForecastWebEntity
        {
            Id = id,
            Date = dto.Date,
            TemperatureC = dto.TemperatureC,
            City = city
        };
        var updated = await _repository.Update(entity);
        return updated.MapToDTO();
    }

    public async Task<bool> Delete(int id)
        => await _repository.Delete(id);
}

