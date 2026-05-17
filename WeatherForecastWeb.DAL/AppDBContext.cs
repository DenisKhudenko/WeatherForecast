
using Microsoft.EntityFrameworkCore;
using WeatherForecastWeb.DAL.Entities;

namespace WeatherForecastWeb.DAL
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

        public DbSet<WeatherForecastWebEntity> WeatherForecasts => Set<WeatherForecastWebEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WeatherForecastWebEntity>(entity =>
            {  
                entity.HasKey(key => key.Id);
            });
        }
    }
}
