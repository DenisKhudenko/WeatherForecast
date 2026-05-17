
namespace WeatherForecastWeb.BLL.DTO;

public class WeatherForecastWebDTO
{
    public int Id { get; set; }

    public DateOnly Date { get; set; }

    public int TemperatureC { get; set; }

    public int TemperatureF { get; set; }

    public string? City { get; set; }
}

public class CreateWeatherForecastWebDTO
{
    public DateOnly Date { get; set; }

    public int TemperatureC { get; set; }

    public string? City { get; set; }
}
