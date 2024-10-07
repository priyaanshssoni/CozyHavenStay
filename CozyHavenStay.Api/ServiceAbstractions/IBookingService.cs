using System;
using CozyHavenStay.Data.DTOs;
using CozyHavenStay.Data.Enums;
using CozyHavenStay.Data.Models;

namespace CozyHavenStay.Api.ServiceAbstractions
{
	public interface IBookingService
	{
        public Task<Booking> GetReservation(int id);
        public Task<List<Booking>> GetAllReservations();
        public Task<Booking> AddReservation(BookingDTO Reservation );//string username removed
        public Task<Booking> UpdateReservationStatus(int id, BookingStatus status);
        public Task<Booking> DeleteReservation(int id);
        //public Task<List<Reservation>> GetReservationsByUserId(int userid);
        public Task<List<Booking>> GetReservationsByRoomId(int roomid);
        public Task<int> GetReservationsCount();
        //public Task<int> AvailableRoomsCount();
        public Task<List<Booking>> GetHotelReservations(int hotelId);
        public Task<bool> IsRoomAvailable(int roomId, DateTime checkInDate, DateTime checkOutDate);
        public Task<List<Booking>> GetReservationsByUserId(int userId);
        public Task<List<Booking>> GetBookingsByOwnerId(int ownerId);
        public Task<Booking> RefundReservation(int id);
        public Task<Booking> AskRefund(int id);
    }

}

