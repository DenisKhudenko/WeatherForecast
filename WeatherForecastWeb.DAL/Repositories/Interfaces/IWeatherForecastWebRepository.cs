
using WeatherForecastWeb.DAL.Entities;

namespace WeatherForecastWeb.DAL.Repositories.Interfaces
{
    public interface IWeatherForecastWebRepository
    {
        Task<IEnumerable<WeatherForecastWebEntity>> GetList();

        Task<WeatherForecastWebEntity?> GetById(int id);

        Task<WeatherForecastWebEntity> Create(WeatherForecastWebEntity entity);

        Task<WeatherForecastWebEntity?> Update(WeatherForecastWebEntity entity);

        Task<bool> Delete(int id);
    }
}
