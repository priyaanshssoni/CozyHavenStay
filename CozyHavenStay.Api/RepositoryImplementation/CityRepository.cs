using System;
using System.Drawing.Printing;
using System.Linq;
using System.Linq.Expressions;
using CozyHavenStay.Api.DataContext;
using CozyHavenStay.Api.RepositoryAbsractions;
using CozyHavenStay.Api.RepositoryAbstractions;
using CozyHavenStay.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CozyHavenStay.Api.RepositoryImplementation
{
    public class CityRepository : IRepository<int, City>,ICityRepository
    {
        private readonly AppDbContext _context;


        public CityRepository(AppDbContext context)
        {
            _context = context;

        }
        public async Task<City> Add(City city)
        {

            _context.Cities.Add(city);
            await _context.SaveChangesAsync();

            return city;
        }


        public async Task<City> Delete(int id)
        {

            var city = await GetById(id);
            if (city != null)
            {
                _context.Cities.Remove(city);
                _context.SaveChanges();

                return city;
            }

            return null;
        }

        public async Task<List<City>> GetAll()
        {

            var cities = await _context.Cities
                .Include(h => h.Hotels)
                .ToListAsync();
            return cities;
        }

        public async Task<City> GetById(int key)
        {

            var city = await _context.Cities
                  .Include(h => h.Hotels)
                .FirstOrDefaultAsync(u => u.CityID == key);
            return city;
        }

        public async Task<IEnumerable<City>> GetWithQuery(Expression<Func<City, bool>> filter = null)
        {
            IQueryable<City> query = _context.Set<City>();

            if (filter is not null)
                query = query.Where(filter);

            return await query.ToListAsync();
        }

        public async Task<City> Update(City id)
        {

            var city = await GetById(id.CityID);
            if (city != null)
            {
                _context.Entry<City>(id).State = EntityState.Modified;
                _context.SaveChanges();

                return city;
            }
            return null;
        }

        public async Task<City> UpdateWithQuery(int id, City entity)
        {
            var existingEntity = await _context.Set<City>().FindAsync(id);

            if (existingEntity is null)
                throw new KeyNotFoundException($"Entity with ID {id} not found.");

            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();

            return existingEntity;
        }

        async Task<IEnumerable<City>> ICityRepository.GetTopVisitedCities(int count)
        {
            if (count <= 0)
                throw new ArgumentOutOfRangeException(nameof(count), "The number of top cities must be greater than zero.");

            return await _context.Cities
                .OrderByDescending(c => c.VisitCount)
                .Take(count)
                .ToListAsync();
        }
    }


}


