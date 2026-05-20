using WeatherForecastWeb.DAL.Entities;

namespace WeatherForecastWeb.BLL.DTO;

/// <summary>
/// Предоставляет маппер отправки данных для сервиса WeatherForecastWebService
/// </summary>
public class WeatherForecastWebDTO
{
    /// <summary>
    /// id прогноза погоды
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Дата прогноза погоды
    /// </summary>
    public DateOnly Date { get; set; }

    /// <summary>
    /// Температура в градусах Цельсия
    /// </summary>
    public int TemperatureC { get; set; }

    /// <summary>
    /// Температура в градусах по Фаренгейту
    /// </summary>
    public int TemperatureF { get; set; }

    /// <summary>
    /// Город прогноза
    /// </summary>
    public required string City { get; set; }

    /// <summary>
    /// Константа слишком высокой температуры
    /// </summary>
    private static readonly int _hotTemp = 26;

    /// <summary>
    /// Константа слишком низкой температуры
    /// </summary>
    private static readonly int _coldTemp = 15;

    /// <summary>
    /// Если температура больше равна hot или меньше равна cold, выдаем нечестный результат рандом между cold и hot
    /// </summary>
    private static int CheckAndChangeTemp(int temp)
    {
        if (temp >= _hotTemp || temp <= _coldTemp)
        {
            Random random = new Random();
            temp = random.Next(_coldTemp + 1, _hotTemp - 1);
        }

        return temp;
    }

    /// <summary>
    /// Получение температуры по Фаренгейтам
    /// </summary>
    private static int GetTemperatureF(int temperatureC)
    {
        return (32 + (int)(temperatureC / 0.5556));
    }

    /// <summary>
    /// Получение DTO из entity
    /// </summary>
    public static WeatherForecastWebDTO FromEntity(WeatherForecastWebEntity entity)
    {
        var TemperatureC = CheckAndChangeTemp(entity.TemperatureC);
        var TemperatureF = GetTemperatureF(TemperatureC);
        return new WeatherForecastWebDTO
        {
            Id = entity.Id,
            Date = entity.Date,
            TemperatureC = TemperatureC,
            TemperatureF = TemperatureF,
            City = entity.City.ToString()
        };
    }
}

/// <summary>
/// Предоставляет маппер создания объекта для сервиса WeatherForecastWebService
/// </summary>
public class CreateWeatherForecastWebDTO
{
    /// <summary>
    /// Дата прогноза погоды
    /// </summary>
    public DateOnly Date { get; set; }

    /// <summary>
    /// Температура в градусах Цельсия
    /// </summary>
    public int TemperatureC { get; set; }

    /// <summary>
    /// id города прогноза
    /// </summary>
    public int CityId { get; set; }
}
