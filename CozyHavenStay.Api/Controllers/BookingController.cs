using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CozyHavenStay.Api.Exceptions;
using CozyHavenStay.Api.ServiceAbstractions;
using CozyHavenStay.Api.ServiceImplementation;
using CozyHavenStay.Data.DTOs;
using CozyHavenStay.Data.Enums;
using CozyHavenStay.Data.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CozyHavenStay.Api.Controllers
{
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingservice;

        public BookingController(IBookingService Bookingservice)
        {
            _bookingservice = Bookingservice;
        }
        [HttpGet("AllReservations")]
        public async Task<ActionResult<List<Booking>>> GetReservations()
        {
            try
            {
                return await _bookingservice.GetAllReservations();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }
        [HttpGet("GetById")]
        public async Task<ActionResult<Booking>> GetReservation(int id)
        {
            try
            {
                return await _bookingservice.GetReservation(id);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }
        //[HttpPost("AddReservation")]
        //public async Task<ActionResult<Reservation>> AddReservation(ReservationDTO Reservation)
        //{
        //    try
        //    {
        //        return await _Reservationservice.AddReservation(Reservation);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
        //    }
        //}
        [HttpPut("UpdateReservationStatus")]
        public async Task<ActionResult<Booking>> UpdateReservation(int id, BookingStatus status)
        {
            try
            {
                return await _bookingservice.UpdateReservationStatus(id, status);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }
        [HttpDelete("DeleteReservation")]
        public async Task<ActionResult<Booking>> DeleteReservation(int id)
        {
            try
            {
                return await _bookingservice.DeleteReservation(id);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }
        [HttpGet("HotelReservations")]
        public async Task<IActionResult> GetHotelReservations(int hotelId)
        {
            try
            {
                var Reservations = await _bookingservice.GetHotelReservations(hotelId);
                return Ok(Reservations);

            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpGet("CheckAvailability")]
        public async Task<ActionResult<bool>> CheckRoomAvailability(int roomId, DateTime checkInDate, DateTime checkOutDate)
        {
            bool isRoomAvailable = await _bookingservice.IsRoomAvailable(roomId, checkInDate, checkOutDate);
            return Ok(isRoomAvailable);
        }

        [HttpPost("AddReservation")]
        public async Task<ActionResult<Booking>> AddReservation([FromBody]BookingDTO Reservation)
        {
            try
            {
                var addedReservation = await _bookingservice.AddReservation(Reservation);
                return CreatedAtAction(nameof(GetReservation), new { id = addedReservation.ReservationId }, addedReservation);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("GetByUserId")]
        public async Task<IActionResult> GetBookingsByUserId(int userId)
        {
            try
            {
                var bookings = await _bookingservice.GetReservationsByUserId(userId);
                return Ok(bookings);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving the bookings.", error = ex.Message });
            }
        }


        [HttpGet("GetBookingsByOwnerId")]
        public async Task<ActionResult<List<Booking>>> GetBookingsByOwnerId(int ownerId)
        {
            return await _bookingservice.GetBookingsByOwnerId(ownerId);
        }

        [HttpPut("RefundReservation")]
        public async Task<ActionResult<Booking>> RefundReservation(int id)
        {
            return await _bookingservice.RefundReservation(id);
        }

        [HttpPut("AskRefund")]
        public async Task<ActionResult<Booking>> AsKRefund(int id)
        {
            return await _bookingservice.AskRefund(id);
        }
    }
}

