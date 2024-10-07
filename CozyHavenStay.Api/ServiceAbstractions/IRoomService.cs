using System;
using CozyHavenStay.Data.DTOs;
using CozyHavenStay.Data.Models;

namespace CozyHavenStay.Api.ServiceAbstractions
{
    public interface IRoomService
    {
        public Task<Room> GetRoom(int id);
        public Task<List<Room>> GetAllRooms();
        public Task<Room> AddRoom(RoomDTO room);
        public Task<Room> UpdateRoomPrice(int id, int price);
        public Task<Room> DeleteRoom(int id);
      //  public Task<ICollection<Booking>> GetRoomReservations(int id);
        public Task<Room> UpdateRoomDetails(RoomDTO roomDTO);
        public Task<ICollection<Room>> GetAvailableRooms();
    }
}

