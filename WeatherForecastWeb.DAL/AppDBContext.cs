
using Microsoft.EntityFrameworkCore;
using WeatherForecastWeb.DAL.Entities;

namespace WeatherForecastWeb.DAL
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

        public DbSet<WeatherForecastWebEntity> WeatherForecasts => Set<WeatherForecastWebEntity>();

        public DbSet<CityEntity> City => Set<CityEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WeatherForecastWebEntity>()
                .HasOne(entity => entity.City)
                .WithMany()
                .HasForeignKey(entity => entity.CityId);
        }
    }
}
