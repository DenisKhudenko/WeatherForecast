
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using WeatherForecastWeb.BL.Services.Interfaces;
using WeatherForecastWeb.BLL.DTO;

namespace WeatherForecastWeb.API.Controllers;

/// <summary>
/// Получение данных для прогноза погоды
/// </summary>
[ApiController]
[Route("weatherForecast")]
public class WeatherForecastWebController : ControllerBase
{
    private readonly IWeatherForecastWebService _service;

    public WeatherForecastWebController(IWeatherForecastWebService service)
    {
        _service = service;
    }

    /// <summary>
    /// Получение списка прогнозов погоды
    /// </summary>
    [HttpGet]
    [SwaggerResponse(statusCode: 200, description: "Получение списка прогнозов погоды", type: typeof(IReadOnlyCollection<WeatherForecastWebDTO>))]
    public async Task<IActionResult> GetList([FromQuery] bool? isTodayWeather)
        => Ok(await _service.GetList(isTodayWeather));

    /// <summary>
    /// Получение прогноза погоды по идентификатору
    /// </summary>
    [HttpGet("{id:int}")]
    [SwaggerResponse(statusCode: 200, description: "Получение прогноза погоды по идентификатору", type: typeof(WeatherForecastWebDTO))]
    [SwaggerResponse(statusCode: 404, description: "Ошибка, не удалось получить данные по идентификатору")]
    public async Task<IActionResult> GetById([Required] int id)
    {
        var result = await _service.GetById(id);
        return result is null ? NotFound() : Ok(result);
    }

    /// <summary>
    /// Получение списка прогнозов погоды по идентификатору города
    /// </summary>
    [HttpGet("city/{cityId}")]
    [SwaggerResponse(statusCode: 200, description: "Получение списка прогнозов погоды по идентификатору города", typeof(IReadOnlyCollection<WeatherForecastWebDTO>))]
    [SwaggerResponse(statusCode: 404, description: "Ошибка, не удалось получить список прогнозов погоды по идентификатору города")]
    public async Task<IActionResult> GetByCityId([Required] int cityId)
    {
        var result = await _service.GetByCityId(cityId);
        return result is null ? NotFound() : Ok(result);
    }

    /// <summary>
    /// Создание прогноза погоды
    /// </summary>
    [HttpPost]
    [SwaggerResponse(statusCode: 200, description: "Создание прогноза погоды", type: typeof(WeatherForecastWebDTO))]
    [SwaggerResponse(statusCode: 404, description: "Ошибка, не удалось создать прогноз погоды")]
    public async Task<IActionResult> Create([FromBody] CreateWeatherForecastWebDTO dto)
    {
        var created = await _service.Create(dto);
        return created is null ? NotFound() : Ok(created);
    }

    /// <summary>
    /// Обновление прогноза погоды по идентификатору
    /// </summary>
    [HttpPut("{id:int}")]
    [SwaggerResponse(statusCode: 200, description: "Обновление прогноза погоды по идентификатору", type: typeof(WeatherForecastWebDTO))]
    [SwaggerResponse(statusCode: 404, description: "Ошибка, не удалось обновить данные прогноза погоды")]
    public async Task<IActionResult> Update([Required] int id, [FromBody] CreateWeatherForecastWebDTO dto)
    {
        var updated = await _service.Update(id, dto);
        return updated is null ? NotFound() : Ok(updated);
    }

    /// <summary>
    /// Удаление прогноза погоды по идентификатору
    /// </summary>
    [HttpDelete("{id:int}")]
    [SwaggerResponse(statusCode: 204, description: "Удаление прогноза погоды по идентификатору успешно", type: typeof(bool))]
    [SwaggerResponse(statusCode: 404, description: "Ошибка, не удалось удалить прогноз погоды")]
    public async Task<IActionResult> Delete([Required] int id)
    {
        var deleted = await _service.Delete(id);
        return deleted ? NoContent() : NotFound();
    }
}
