using System;
using CozyHavenStay.Api.DataContext;
using CozyHavenStay.Api.ServiceAbstractions;
using CozyHavenStay.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CozyHavenStay.Api.ServiceImplementation
{
    public class HotelAmenityService : IHotelAmenityService
    {
        private readonly AppDbContext _context;

        public HotelAmenityService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<HotelAmenity>> GetHotelAmenities(int hotelId)
        {
            return await _context.HotelAmenities
                                .Include(ha => ha.Amenity.Name)
                                .Where(ha => ha.HotelId == hotelId)
                                .ToListAsync();
        }
    }
}

