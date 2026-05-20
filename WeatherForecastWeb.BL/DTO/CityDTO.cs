using WeatherForecastWeb.DAL.Entities;

namespace WeatherForecastWeb.BLL.DTO;

/// <summary>
/// Предоставляет маппер отправки данных для сервиса CityService
/// </summary>
public class CityDTO
{
    /// <summary>
    /// id города
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Наименование города
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Получение DTO из entity
    /// </summary>
    public static CityDTO FromEntity(CityEntity entity)
    {
        return new CityDTO
        {
            Id = entity.Id,
            Name = entity.Name,
        };
    }
}

/// <summary>
/// Предоставляет маппер создания объекта для сервиса CityService
/// </summary>
public class CreateCityDTO
{
    /// <summary>
    /// Наименование города
    /// </summary>
    public required string Name { get; set; }
}
