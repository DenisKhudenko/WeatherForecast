
using Microsoft.EntityFrameworkCore;
using WeatherForecastWeb.DAL.Entities;
using WeatherForecastWeb.DAL.Repositories.Interfaces;

namespace WeatherForecastWeb.DAL.Repositories
{
    public class CityRepository : ICityRepository
    {
        private readonly AppDBContext _context;

        public CityRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CityEntity>> GetList()
            => await _context.City.ToListAsync();

        public async Task<CityEntity?> GetById(int id)
            => await _context.City.FindAsync(id);

        public async Task<CityEntity> Create(CityEntity entity)
        {
            _context.City.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<CityEntity?> Update(CityEntity entity)
        {
            var existing = await _context.City.FindAsync(entity.Id);
            if (existing is null) return null;

            existing.Name = entity.Name;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await _context.City.FindAsync(id);
            if (entity is null) return false;

            _context.City.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
