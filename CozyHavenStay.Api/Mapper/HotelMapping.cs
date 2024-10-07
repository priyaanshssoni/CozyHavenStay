using System;
using CozyHavenStay.Data.DTOs;
using CozyHavenStay.Data.Models;

namespace CozyHavenStay.Api.Mapper
{
    public class HotelMapping
    {
        public static HotelDTO MapHotelToDTO(Hotel hotel)
        {
            return new HotelDTO
            {
                HotelId = hotel.HotelId,
                Name = hotel.Name,
                Address = hotel.Address,
                Description = hotel.Description,
                CityId = hotel.CityId,
                City = hotel.City,
                NumberOfRooms = hotel.Rooms?.Count ?? 0,
                ImageLinks = hotel.ImageLinks.ToList(),
                StartPrice = hotel.StartPrice,
                EndPrice = hotel.EndPrice,
                

            };
        }

        public static Hotel MapDTOToHotel(HotelDTO hotelDTO)
        {
            return new Hotel
            {
                HotelId = hotelDTO.HotelId,
                OwnerId = hotelDTO.OwnerId,
                Name = hotelDTO.Name,
                Address = hotelDTO.Address,
                Description = hotelDTO.Description,
                CityId = hotelDTO.CityId,
                City = hotelDTO.City,
                NumberOfRooms = hotelDTO.NumberOfRooms,
                ImageLinks = hotelDTO.ImageLinks != null ? hotelDTO.ImageLinks : new List<string>(),
                StartPrice = hotelDTO.StartPrice,
                EndPrice = hotelDTO.EndPrice
                
            };
        }
    }
}

