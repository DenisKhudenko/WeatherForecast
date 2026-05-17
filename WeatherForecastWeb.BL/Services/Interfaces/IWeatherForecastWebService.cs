using WeatherForecastWeb.BLL.DTO;

namespace WeatherForecastWeb.BL.Services.Interfaces
{
    public interface IWeatherForecastWebService
    {
        Task<IEnumerable<WeatherForecastWebDTO>> GetList();

        Task<WeatherForecastWebDTO?> GetById(int id);

        Task<WeatherForecastWebDTO?> GetUnfairResultById(int id);

        Task<WeatherForecastWebDTO> Create(CreateWeatherForecastWebDTO dto);

        Task<WeatherForecastWebDTO?> Update(int id, CreateWeatherForecastWebDTO dto);

        Task<bool> Delete(int id);
    }
}
