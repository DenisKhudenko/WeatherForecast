using WeatherForecastWeb.DAL.Entities;

namespace WeatherForecastWeb.DAL.Repositories.Interfaces
{
    public interface IWeatherForecastWebRepository
    {
        Task<IEnumerable<WeatherForecastWebEntity>> GetList();

        Task<WeatherForecastWebEntity?> GetById(int id);

        Task<IEnumerable<WeatherForecastWebEntity>> GetByCityId(int cityId);

        Task<WeatherForecastWebEntity> Create(WeatherForecastWebEntity entity);

        Task<WeatherForecastWebEntity?> Update(WeatherForecastWebEntity entity);

        Task<bool> Delete(int id);

        Task<WeatherForecastWebEntity?> hasDateByCity(DateOnly date, CityEntity city);
    }
}
