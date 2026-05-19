using WeatherForecastWeb.BLL.DTO;
using WeatherForecastWeb.DAL.Entities;

namespace WeatherForecastWeb.BL.Services.Interfaces
{
    public interface IWeatherForecastWebService
    {
        Task<IReadOnlyCollection<WeatherForecastWebDTO>> GetList(bool? isTodayWeather);

        Task<WeatherForecastWebDTO?> GetById(int id);

        Task<IEnumerable<WeatherForecastWebDTO>> GetByCityId(int cityId);

        Task<WeatherForecastWebDTO> Create(CreateWeatherForecastWebDTO dto);

        Task<WeatherForecastWebDTO?> Update(int id, CreateWeatherForecastWebDTO dto);

        Task<bool> Delete(int id);
    }
}
