using System;
using CozyHavenStay.Api.DataContext;
using CozyHavenStay.Api.Exceptions;
using CozyHavenStay.Api.RepositoryAbstractions;
using CozyHavenStay.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CozyHavenStay.Api.RepositoryImplementation
{
    public class BookingRepository : IRepository<int, Booking>
    {
        private readonly AppDbContext _context;
  

        public BookingRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Booking> Add(Booking item)
        {
            _context.Bookings.Add(item);
            await _context.SaveChangesAsync();
            
            return item;

        }

        public async Task<Booking> Delete(int key)
        {
            var booking = await GetById(key);
            if (booking != null)
            {
                _context.Bookings.Remove(booking);
                _context.SaveChanges();
                return booking;
            }
            else
            {
                throw new NotFoundException($"Booking with ID {key} not found.");
            }
        }

        public async Task<List<Booking>> GetAll()
        {
            var bookings = _context.Bookings
                .Include(b => b.Room)
                .ToList();
          
            return await Task.FromResult(bookings);
        }

        public async Task<Booking> GetById(int key)
        {
            var booking = await _context.Bookings
                .Include(b => b.Room)
                .FirstOrDefaultAsync(b => b.ReservationId == key);
            if (booking != null)
            {
              
                return booking;
            }
            else
            {
                throw new NotFoundException($"Booking with ID {key} not found.");
            }
        }


        public async Task<Booking> Update(Booking item)
        {
            var booking = await GetById(item.ReservationId);
            if (booking != null)
            {
                _context.Entry<Booking>(item).State = EntityState.Modified;
                _context.SaveChanges();
         
                return item;
            }
            else
            {
                throw new NotFoundException($"Booking with ID {item.ReservationId} not found.");
            }
        }
    }
}

