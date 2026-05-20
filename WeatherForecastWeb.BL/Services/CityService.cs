using System.Data;
using WeatherForecastWeb.BL.Extensions;
using WeatherForecastWeb.BL.Services.Interfaces;
using WeatherForecastWeb.BLL.DTO;
using WeatherForecastWeb.DAL.Entities;
using WeatherForecastWeb.DAL.Repositories.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WeatherForecastWeb.BL.Services;

public class CityService : ICityService
{
    private readonly ICityRepository _repository;

    public CityService(ICityRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyCollection<CityDTO>> GetList()
    {
        var entities = await _repository.GetList();
        return entities.Select(value => value.MapToDTO()).ToList();
    }

    public async Task<CityDTO?> GetById(int id)
    {
        var entity = await _repository.GetById(id);
        return entity is null ? null : entity.MapToDTO();
    }

    public async Task<CityDTO> Create(CreateCityDTO dto)
    {
        var entity = new CityEntity
        {
            Name = dto.Name
        };

        var created = await _repository.Create(entity);
        return created.MapToDTO();
    }

    public async Task<CityDTO?> Update(int id, CreateCityDTO dto)
    {
        var entity = new CityEntity
        {
            Id = id,
            Name = dto.Name
        };
        var updated = await _repository.Update(entity);
        return updated.MapToDTO();
    }

    public async Task<bool> Delete(int id)
        => await _repository.Delete(id);
}

