using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CozyHavenStay.Api.ServiceAbstractions;
using CozyHavenStay.Api.ServiceImplementation;
using CozyHavenStay.Data.DTOs;
using CozyHavenStay.Data.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CozyHavenStay.Api.Controllers
{
    [Route("api/[controller]")]
    public class HotelController : Controller
    {
        private readonly IHotelService _hotelService;
        private readonly IHotelAmenityService _hotelAmenityService;

        public HotelController(IHotelService hotelService,IHotelAmenityService hotelAmenityService)
        {
            _hotelService = hotelService;
            _hotelAmenityService = hotelAmenityService;

        }

        [HttpGet("GetAllHotels")]
        public async Task<IActionResult> GetHotels()
        {
           
                var hotels = await _hotelService.GetAllHotels();
                return Ok(hotels);
           
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int id)
        {
           
                var hotel = await _hotelService.GetHotel(id);
                return Ok(hotel);
            
            
        }

        //[HttpGet("GetByCityId")]
        //public async Task<IActionResult> GetByDestinationId(int id)
        //{
        //    try
        //    {
        //        var hotels = await _hotelService.GetHotelsByDestinationId(id);
        //        return Ok(hotels);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ex.Message);
        //    }
        //}

        [HttpPost("AddHotel")]
        public async Task<IActionResult> AddHotel([FromBody]HotelDTO hotel)
        {
            
                int ownerId = hotel.OwnerId;

                var addedHotel = await _hotelService.AddHotel(hotel, ownerId);
                return Ok(addedHotel);
            
          
        }

        [HttpPut("UpdateDescription")]
        public async Task<IActionResult> UpdateDescription(int id, string description)
        {
            
                var updatedHotel = await _hotelService.UpdateHotelDescription(id, description);
                return Ok(updatedHotel);
            
            
        }

        [HttpDelete("DeleteHotel")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
              var deletedHotel = await _hotelService.DeleteHotel(id);
                return Ok(deletedHotel);
            
           

        }
        [HttpGet("GetHotelsByOwner")]
        public async Task<IActionResult> GetHotelsByOwner(int ownerId)
        {
           
                var updatedHotel = await _hotelService.GetHotelsByOwner(ownerId);
                return Ok(updatedHotel);
            
           
        }

        [HttpGet("HotelReviews")]
        public async Task<IActionResult> GetHotelReviews(int id)
        {
            
                var reviews = await _hotelService.GetHotelReviews(id);
                return Ok(reviews);
            

        }

        [HttpGet("GetAverageRating")]
        public async Task<ActionResult<double>> GetAverageRating(int hotelId)
        {
            var averageRating = await _hotelService.GetAverageRating(hotelId);
            return Ok(averageRating);
        }

        [HttpGet("HotelAmenities")]
        public async Task<IActionResult> GetHotelAmenities(int id)
        {


            var amenities = await _hotelAmenityService.GetHotelAmenities(id);
            return Ok(amenities);


        }

        //[HttpGet("GetRoomsByHotelId")]
        //public async Task<ActionResult<ICollection<Room>>> GetRoomsByHotelId(int hotelId)
        //{
        //    try
        //    {
        //        var rooms = await _hotelService.GetRoomsByHotelId(hotelId);
        //        return Ok(rooms);
        //    }
        //    catch (Exception ex)
        //    {
        //        return NotFound(ex.Message);
        //    }
        //}
        [HttpPut("UpdateHotelDetails")]
        public async Task<IActionResult> UpdateHotelDetails(HotelDTO hotelDTO)
        {
           
                var updatedHotel = await _hotelService.UpdateHotelDetails(hotelDTO);
                return Ok(updatedHotel);
            
           
        }


        //[HttpGet("GetHotelReservations")]
        //public async Task<ActionResult<ICollection<Booking>>> GetHotelReservations(int hotelId)
        //{
        //    try
        //    {
        //        var Reservations = await _hotelService.GetHotelBookings(hotelId);
        //        return Ok(Reservations);
        //    }
        //    catch (Exception ex)
        //    {
        //        return NotFound(ex.Message);
        //    }
        //}
        [HttpGet("GetHotelsByLocation")]
        public async Task<ActionResult<IEnumerable<Hotel>>> GetHotelsByLocation(string location)
        {
            
                var hotels = await _hotelService.GetHotelsByLocation(location);
                return Ok(hotels);
            
            
        }

        [HttpGet("GetHotelByLocationAndDate")]
        public async Task<ActionResult<IEnumerable<Hotel>>> GetHotelByLocationAndDate(string location, DateTime checkin, DateTime checkout, int capacity)
        {
           
                var hotels = await _hotelService.GetHotelsByCriteria(location, checkin, checkout, capacity);
                return Ok(hotels);
            
           
        }
    }
}

