
using WeatherForecastWeb.BLL.DTO;
using WeatherForecastWeb.DAL.Entities;

namespace WeatherForecastWeb.BL.Extensions
{
    public static class CityExtension
    {
        public static CityDTO MapToDTO(this CityEntity entity)
        {
            return CityDTO.FromEntity(entity);
        }
    }
}
