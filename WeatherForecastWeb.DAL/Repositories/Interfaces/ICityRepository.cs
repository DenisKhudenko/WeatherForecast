using WeatherForecastWeb.DAL.Entities;

namespace WeatherForecastWeb.DAL.Repositories.Interfaces
{
    public interface ICityRepository
    {
        Task<IEnumerable<CityEntity>> GetList();

        Task<CityEntity?> GetById(int id);

        Task<CityEntity> Create(CityEntity entity);

        Task<CityEntity?> Update(CityEntity entity);

        Task<bool> Delete(int id);
    }

}
