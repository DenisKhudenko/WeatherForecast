
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using WeatherForecastWeb.BL.Services.Interfaces;
using WeatherForecastWeb.BLL.DTO;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WeatherForecastWeb.API.Controllers;

/// <summary>
/// Получение данных для городов
/// </summary>
[ApiController]
[Route("city")]
public class CityController : ControllerBase
{
    private readonly ICityService _service;

    public CityController(ICityService service)
    {
        _service = service;
    }

    /// <summary>
    /// Получение списка городов
    /// </summary>
    [HttpGet]
    [SwaggerResponse(statusCode: 200, description: "Получение списка городов успешно", type: typeof(IReadOnlyCollection<CityDTO>))]
    public async Task<IActionResult> GetList()
        => Ok(await _service.GetList());

    /// <summary>
    /// Получение города по идентификатору
    /// </summary>
    [HttpGet("{id:int}")]
    [SwaggerResponse(statusCode: 200, description: "Получение города по идентификатору успешно", type: typeof(CityDTO))]
    [SwaggerResponse(statusCode: 404, description: "Ошибка, город по идентфикатору не найден")]
    public async Task<IActionResult> GetById([Required] int id)
    {
        var result = await _service.GetById(id);
        return result is null ? NotFound() : Ok(result);
    }

    /// <summary>
    /// Создание города
    /// </summary>
    [HttpPost]
    [SwaggerResponse(statusCode: 200, description: "Создание города успешно", type: typeof(CityDTO))]
    [SwaggerResponse(statusCode: 404, description: "Ошибка, не удалось создать город")]
    public async Task<IActionResult> Create([FromBody] CreateCityDTO dto)
    {
        var created = await _service.Create(dto);
        return created is null ? NotFound() : Ok(created);
    }

    /// <summary>
    /// Обновление города по идентификатору
    /// </summary>
    [HttpPut("{id:int}")]
    [SwaggerResponse(statusCode: 200, description: "Обновление города по идентификатору успешно", type: typeof(CityDTO))]
    [SwaggerResponse(statusCode: 404, description: "Ошибка, не удалось обновить данные по городу")]
    public async Task<IActionResult> Update([Required] int id, [FromBody] CreateCityDTO dto)
    {
        var updated = await _service.Update(id, dto);
        return updated is null ? NotFound() : Ok(updated);
    }

    /// <summary>
    /// Удаление города по идентификатору
    /// </summary>
    [HttpDelete("{id:int}")]
    [SwaggerResponse(statusCode: 204, description: "Удаление города по идентификатору успешно", type: typeof(bool))]
    [SwaggerResponse(statusCode: 404, description: "Ошибка, не удалось создать город")]
    public async Task<IActionResult> Delete([Required] int id)
    {
        var deleted = await _service.Delete(id);
        return deleted ? NoContent() : NotFound();
    }
}
