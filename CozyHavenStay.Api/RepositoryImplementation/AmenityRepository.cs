using System;
using CozyHavenStay.Api.DataContext;
using CozyHavenStay.Api.RepositoryAbstractions;
using CozyHavenStay.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CozyHavenStay.Api.RepositoryImplementation
{
    public class AmenityRepository : IRepository<int, Amenity>
    {
        private readonly AppDbContext _context;


        public AmenityRepository(AppDbContext context)
        {
            _context = context;

        }

        public async Task<Amenity> Add(Amenity item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();

            return item;
        }

        public async Task<Amenity> Delete(int key)
        {
            var amenity = await GetById(key);
            _context.Amenities.Remove(amenity);
            await _context.SaveChangesAsync();

            return amenity;
        }

        public async Task<Amenity> GetById(int key)
        {
            var amenity = await _context.Amenities.FindAsync(key);
            if (amenity != null)
            {
                return amenity;
            }
            throw new Exception();
        }

        public async Task<List<Amenity>> GetAll()
        {
            return await _context.Amenities.ToListAsync();
        }

        public async Task<Amenity> Update(Amenity item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return item;
        }
    }
}

