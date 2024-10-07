using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CozyHavenStay.Api.Mapper;
using CozyHavenStay.Api.ServiceAbstractions;
using CozyHavenStay.Api.ServiceImplementation;
using CozyHavenStay.Data.DTOs;
using CozyHavenStay.Data.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CozyHavenStay.Api.Controllers
{
    [Route("api/[controller]")]
    public class AmenityController : ControllerBase
    {
        private readonly IAmenityService _amenityService;
        private readonly IHotelService _hotelService;
        private readonly IRoomService _roomservice;


        public AmenityController(IAmenityService amenityService, IHotelService hotelService,IRoomService roomservice)
        {
            _amenityService = amenityService;
            _hotelService = hotelService;
            _roomservice = roomservice;
        }

        [HttpPost("addamenity")]
        public async Task<ActionResult<Amenity>> AddAmenity(AmenityDTO amenity)
        {
            var newAmenity = await _amenityService.AddAmenity(amenity);
            return CreatedAtAction(nameof(GetAmenity), new { id = newAmenity.AmenityId }, newAmenity);
        }

        [HttpDelete("deleteamenity")]
        public async Task<ActionResult<Amenity>> DeleteAmenity(int id)
        {
            var deletedAmenity = await _amenityService.DeleteAmenity(id);
            if (deletedAmenity == null)
            {
                return NotFound();
            }
            return deletedAmenity;
        }

        [HttpGet("getamenitybyid")]
        public async Task<ActionResult<Amenity>> GetAmenity(int id)
        {
            var amenity = await _amenityService.GetAmenity(id);
            if (amenity == null)
            {
                return NotFound();
            }
            return amenity;
        }

        [HttpGet("getallamenity")]
        public async Task<ActionResult<List<Amenity>>> GetAllAmenities()
        {
            var amenities = await _amenityService.GetAllAmenities();
            return amenities;
        }


        [HttpPut("updateamenity")]
        public async Task<IActionResult> UpdateAmenity(int id,string name)
        {
         
                var updatedAmenity = await _amenityService.UpdateAmenity(id, name);
                return Ok(updatedAmenity);
          
        }

        [HttpPost("addamenitytohotel")]
        public async Task<IActionResult> AddAmenityToHotel(int hotelId, int amenityId)
        {
            try
            {
                var hotel = await _hotelService.GetHotel(hotelId);

                if (hotel == null)
                {
                    return NotFound("Hotel not found.");
                }

                var amenity = await _amenityService.GetAmenity(amenityId);

                if (amenity == null)
                {
                    return NotFound("Amenity not found.");
                }

                if (hotel.HotelAmenities != null && hotel.HotelAmenities.Any(ha => ha.AmenityId == amenityId))
                {
                    return Conflict("Amenity is already added to the hotel.");
                }

                if (hotel.HotelAmenities != null)
                {
                    hotel.HotelAmenities.Add(new HotelAmenity { HotelId = hotelId, AmenityId = amenityId });
                }
                else
                {
                    hotel.HotelAmenities = new List<HotelAmenity>();
                    hotel.HotelAmenities.Add(new HotelAmenity { HotelId = hotelId, AmenityId = amenityId });
                }

                await _hotelService.UpdateHotelDetails(hotel);

                return Ok("Amenity added to hotel successfully.");
            }
            catch
            {
                return StatusCode(500, "An error occurred while adding the amenity to the hotel.");
            }
        }

        [HttpPost("deleteamenityfromhotel")]
        public async Task<IActionResult> DeleteAmenityFromHotel(int hotelId, int amenityId)
        {
            try
            {
                var hotel = await _hotelService.GetHotel(hotelId);

                if (hotel == null)
                {
                    return NotFound("Hotel not found.");
                }

                var amenity = await _amenityService.GetAmenity(amenityId);

                if (amenity == null)
                {
                    return NotFound("Amenity not found.");
                }

                if (hotel.HotelAmenities != null)
                {
                    var hotelAmenity = hotel.HotelAmenities.FirstOrDefault(ha => ha.AmenityId == amenityId);

                    if (hotelAmenity == null)
                    {
                        return NotFound("Amenity is not associated with the hotel.");
                    }

                    hotel.HotelAmenities.Remove(hotelAmenity);

                    await _hotelService.UpdateHotelDetails(hotel);

                    return Ok("Amenity deleted from hotel successfully.");
                }
                else
                {
                    return StatusCode(500, "An error occurred while deleting the amenity from the hotel.");
                }
            }
            catch
            {
                return StatusCode(500, "An error occurred while deleting the amenity from the hotel.");
            }
        }

        [HttpPost("addamenitytoroom")]
        public async Task<IActionResult> AddAmenityToRoom(int roomId, int amenityId)
        {
            try
            {
                var room = await _roomservice.GetRoom(roomId);

                if (room == null)
                {
                    return NotFound("Room not found.");
                }

                var amenity = await _amenityService.GetAmenity(amenityId);

                if (amenity == null)
                {
                    return NotFound("Amenity not found.");
                }

                if (room.RoomAmenities != null && room.RoomAmenities.Any(ra => ra.AmenityId == amenityId))
                {
                    return Conflict("Amenity is already added to the room.");
                }

                if (room.RoomAmenities != null)
                {
                    room.RoomAmenities.Add(new RoomAmenity { RoomID = roomId, AmenityId = amenityId });
                }
                else
                {
                    room.RoomAmenities = new List<RoomAmenity>();
                    room.RoomAmenities.Add(new RoomAmenity { RoomID = roomId, AmenityId = amenityId });
                }
                var roomdto = RoomMapping.MapRoomToDTO(room);
                await _roomservice.UpdateRoomDetails(roomdto);

                return Ok("Amenity added to room successfully.");
            }
            catch
            {
                return StatusCode(500, "An error occurred while adding the amenity to the room.");
            }
        }

        [HttpPost("deleteamenityfromroom")]
        public async Task<IActionResult> DeleteAmenityFromRoom(int roomId, int amenityId)
        {
            try
            {
                var room = await _roomservice.GetRoom(roomId);

                if (room == null)
                {
                    return NotFound("Room not found.");
                }

                var amenity = await _amenityService.GetAmenity(amenityId);

                if (amenity == null)
                {
                    return NotFound("Amenity not found.");
                }

                if (room.RoomAmenities != null)
                {
                    var roomAmenity = room.RoomAmenities.FirstOrDefault(ra => ra.AmenityId == amenityId);

                    if (roomAmenity == null)
                    {
                        return NotFound("Amenity is not associated with the room.");
                    }
                    room.RoomAmenities.Remove(roomAmenity);
                    var roomdto = RoomMapping.MapRoomToDTO(room);
                    await _roomservice.UpdateRoomDetails(roomdto);

                    return Ok("Amenity deleted from room successfully.");
                }
                else
                {
                    return StatusCode(500, "An error occurred while deleting the amenity from the room.");
                }
            }
            catch
            {
                return StatusCode(500, "An error occurred while deleting the amenity from the room.");
            }
        }

    }
}

