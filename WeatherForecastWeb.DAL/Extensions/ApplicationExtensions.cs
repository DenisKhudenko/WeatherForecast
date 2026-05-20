using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace WeatherForecastWeb.DAL.Extensions;

public static class ApplicationExtensions
{
    public static IApplicationBuilder UseWeatherForecastWeb(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        scope.ServiceProvider.GetRequiredService<AppDBContext>().Database.Migrate();

        return app;
    }
}
