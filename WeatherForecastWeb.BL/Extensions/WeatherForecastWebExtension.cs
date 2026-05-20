
using WeatherForecastWeb.BLL.DTO;
using WeatherForecastWeb.DAL.Entities;

namespace WeatherForecastWeb.BL.Extensions
{
    public static class WeatherForecastWebExtension
    {
        public static WeatherForecastWebDTO MapToDTO(this WeatherForecastWebEntity entity)
        {
            return WeatherForecastWebDTO.FromEntity(entity);
        }
    }
}
