namespace WeatherForecastWeb.DAL.Entities
{
    public class WeatherForecastWebEntity
    {
        public int Id { get; set; }

        public DateOnly Date { get; set; }

        public int TemperatureC {  get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public int CityId { get; set; }

        public CityEntity? City { get; set; }
    }
}
