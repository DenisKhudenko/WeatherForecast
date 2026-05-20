using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Kpi.Web.Api.App.Controllers;

/// <summary>
/// Базовый контроллер для описания кодов состояния
/// </summary>
[SwaggerResponse(StatusCodes.Status400BadRequest)]
[SwaggerResponse(StatusCodes.Status404NotFound)]
[SwaggerResponse(StatusCodes.Status500InternalServerError)]
[ApiController]
public abstract class BaseController : ControllerBase
{
}
