
using Microsoft.AspNetCore.Mvc;
using WeatherForecastWeb.BL.Services.Interfaces;
using WeatherForecastWeb.BLL.DTO;

namespace WeatherForecastWeb.App.Controllers;

[ApiController]
[Route("weatherForecast")]
public class WeatherForecastWebController : ControllerBase
{
    private readonly IWeatherForecastWebService _service;

    public WeatherForecastWebController(IWeatherForecastWebService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] bool? isTodayWeather)
        => Ok(await _service.GetList(isTodayWeather));

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _service.GetById(id);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpGet("city/{cityId}")]
    public async Task<IActionResult> GetByCityId(int cityId)
    {
        var result = await _service.GetByCityId(cityId);
        return Ok(result);
    }


    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateWeatherForecastWebDTO dto)
    {
        var created = await _service.Create(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] CreateWeatherForecastWebDTO dto)
    {
        var updated = await _service.Update(id, dto);
        return updated is null ? NotFound() : Ok(updated);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.Delete(id);
        return deleted ? NoContent() : NotFound();
    }
}
