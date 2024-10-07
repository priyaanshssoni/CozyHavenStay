using System;
using CozyHavenStay.Data.DTOs;
using CozyHavenStay.Data.Models;

namespace CozyHavenStay.Api.Mapper
{
    public class RoomMapping
    {
        public static RoomDTO MapRoomToDTO(Room room)
        {
            return new RoomDTO
            {
                RoomId = room.RoomId,
                HotelId = room.HotelId,
                RoomSize = room.RoomSize,
                RoomType = room.RoomType,
                Price = room.Price,
                Capacity = room.Capacity,
                Available = true,
                ImageLinks = room.ImageLinks.ToList(),

            };
        }

        public static Room MapRoomDTOToEntity(RoomDTO roomDTO)
        {
            return new Room
            {
                RoomId = roomDTO.RoomId,
                HotelId = roomDTO.HotelId,
                RoomSize = roomDTO.RoomSize,
                RoomType = roomDTO.RoomType,
                Price = roomDTO.Price,
                Capacity = roomDTO.Capacity,
                Available = true,
               ImageLinks = roomDTO.ImageLinks != null ? roomDTO.ImageLinks : new List<string>(),
            };
        }
    }
}

