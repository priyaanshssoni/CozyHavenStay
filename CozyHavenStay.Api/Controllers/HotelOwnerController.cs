using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CozyHavenStay.Api.DataContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CozyHavenStay.Api.Controllers
{
    [Route("api/[controller]")]
    public class HotelOwnerController : Controller
    {

        private readonly AppDbContext  _context;

        public HotelOwnerController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetBookingByDate")]
        public async Task<ActionResult<IEnumerable<object>>> GetBookingSummaries([FromQuery] DateTime startDate,
    [FromQuery] DateTime endDate)
        {
            var parameters = new[]
            {
            new SqlParameter("@startDate", startDate),
            new SqlParameter("@endDate", endDate)
        };

            return await _context.OwnerBookingDetails
                .FromSqlRaw("SELECT * FROM v_OwnerBookingDetails WHERE CheckInDate >= @startDate AND CheckOutDate <= @endDate", parameters)
                .ToListAsync();
        }

        [HttpGet("GetAllBooking")]
        public async Task<ActionResult<IEnumerable<object>>> GetAllBookings()
        {

            return await _context.OwnerBookingDetails
                .FromSqlRaw("SELECT * FROM v_OwnerBookingDetails")
                .ToListAsync();
        }
    }
}

