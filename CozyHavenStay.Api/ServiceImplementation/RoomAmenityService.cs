using System;
using CozyHavenStay.Api.DataContext;
using CozyHavenStay.Api.ServiceAbstractions;
using CozyHavenStay.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CozyHavenStay.Api.ServiceImplementation
{
    public class RoomAmenityService : IRoomAmenityService
    {
        private readonly AppDbContext _context;

        public RoomAmenityService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<RoomAmenity>> GetRoomAmenities(int roomid)
        {
            return await _context.RoomAmenities
                                .Include(ha => ha.Amenity)
                                .Where(ha => ha.RoomID == roomid)
                                .ToListAsync();
        }
    }
}

