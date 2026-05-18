using WeatherForecastWeb.BLL.DTO;

namespace WeatherForecastWeb.BL.Services.Interfaces
{
    public interface ICityService
    {
        Task<IReadOnlyCollection<CityDTO>> GetList();

        Task<CityDTO?> GetById(int id);

        Task<CityDTO> Create(CreateCityDTO dto);

        Task<CityDTO?> Update(int id, CreateCityDTO dto);

        Task<bool> Delete(int id);
    }
}
