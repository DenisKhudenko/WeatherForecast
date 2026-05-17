using System.Data;
using WeatherForecastWeb.BL.Services.Interfaces;
using WeatherForecastWeb.BLL.DTO;
using WeatherForecastWeb.DAL.Entities;
using WeatherForecastWeb.DAL.Repositories.Interfaces;

namespace WeatherForecastWeb.BL.Services;

public class CityService : ICityService
{
    private readonly ICityRepository _repository;

    public CityService(ICityRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<CityDTO>> GetList()
    {
        var entities = await _repository.GetList();
        return entities.Select(MapToDto);
    }

    public async Task<CityDTO?> GetById(int id)
    {
        var entity = await _repository.GetById(id);
        return entity is null ? null : MapToDto(entity);
    }

    public async Task<CityDTO> Create(CreateCityDTO dto)
    {
        var entity = new CityEntity
        {
            Name = dto.Name
        };

        var created = await _repository.Create(entity);
        return MapToDto(created);
    }

    public async Task<CityDTO?> Update(int id, CreateCityDTO dto)
    {
        var entity = new CityEntity
        {
            Id = id,
            Name = dto.Name
        };
        var updated = await _repository.Update(entity);
        return updated is null ? null : MapToDto(updated);
    }

    public async Task<bool> Delete(int id)
        => await _repository.Delete(id);

    private static CityDTO MapToDto(CityEntity entity) => new()
    {
        Id = entity.Id,
        Name = entity.Name,
    };

}

