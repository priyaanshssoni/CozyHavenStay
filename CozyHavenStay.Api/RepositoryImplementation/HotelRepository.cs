using System;
using CozyHavenStay.Api.DataContext;
using CozyHavenStay.Api.RepositoryAbsractions;
using CozyHavenStay.Api.RepositoryAbstractions;
using CozyHavenStay.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CozyHavenStay.Api.RepositoryImplementation;

    public class HotelRepository : IRepository<int, Hotel>,IHotelRepository
{

    private readonly AppDbContext _context;


    public HotelRepository(AppDbContext context)
    {
        _context = context;

    }

    public async Task<Hotel> Add(Hotel item)
    {

        _context.Hotels.Add(item);
        await _context.SaveChangesAsync();
        return item;

    }

    public async Task<Hotel> Delete(int key)
    {
        var hotel = await GetById(key);
        if (hotel != null)
        {
            _context.Hotels.Remove(hotel);
            _context.SaveChanges();
            return hotel;
        }
        else
        {
            throw new Exception($"Hotel with ID {key} not found.");
        }
    }


    public async Task<List<Hotel>> GetAll()
    {
        return await Task.FromResult(_context.Hotels
            .Include(h => h.Rooms)
            .Include(h => h.Reviews)
            .Include(h => h.HotelAmenities)
            .Include(h => h.Owner)
            .ToList());
    }

    public async Task<Hotel> GetById(int key)
    {
        var hotel = await Task.FromResult(_context.Hotels
            .Include(h => h.Rooms)
            .Include(h => h.Reviews)
            .Include(h => h.HotelAmenities)
               .Include(h => h.Owner)

            .FirstOrDefault(h => h.HotelId == key));

        if (hotel != null)
        {
            return hotel;
        }
        throw new Exception($"Hotel with ID {key} not found.");
    }

    public async Task<Hotel> Update(Hotel item)
    {
        var hotel = await GetById(item.HotelId);

        if (hotel != null)
        {
            _context.Entry<Hotel>(item).State = EntityState.Modified;
            _context.SaveChanges();
            return item;
        }
        throw new Exception($"Hotel with ID {item.HotelId} not found for update.");
    }

    public async Task<List<Hotel>> GetHotelsByCityId(int cityId)
    {
           var hotelfound =  await _context.Hotels
                .Where(h => h.CityId == cityId)
                .Include(h => h.Cityy)
                .ToListAsync();

            return hotelfound;
    }

    public void AddHotelToCity(int hotelId, int cityId)
    {
        var hotel = _context.Hotels.Find(hotelId);
        var city = _context.Cities.Find(cityId);

        if (hotel != null && city != null)
        {
            hotel.Cityy = city;
            _context.SaveChanges();
        }
    }

    public void RemoveHotelFromCity(int hotelId, int cityId)
    {
        var hotel = _context.Hotels.Find(hotelId);
        var city = _context.Cities.Find(cityId);

        if (hotel != null && city != null)
        {
            hotel.Cityy = null;
            _context.SaveChanges();
        }
    }

}
    



