
namespace WeatherForecastWeb.BLL.DTO;

public class CityDTO
{
    public int Id { get; set; }

    public required string Name { get; set; }
}

public class CreateCityDTO
{
    public required string Name { get; set; }
}
