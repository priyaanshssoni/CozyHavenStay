using System;
using CozyHavenStay.Api.Exceptions;
using CozyHavenStay.Api.Mapper;
using CozyHavenStay.Api.RepositoryAbsractions;
using CozyHavenStay.Api.RepositoryAbstractions;
using CozyHavenStay.Api.RepositoryImplementation;
using CozyHavenStay.Api.ServiceAbstractions;
using CozyHavenStay.Data.DTOs;
using CozyHavenStay.Data.Enums;
using CozyHavenStay.Data.Models;

namespace CozyHavenStay.Api.ServiceImplementation
{
    public class BookingService : IBookingService
    {
        private readonly IRepository<int, Booking> _reservationrepository;
        private readonly IRepository<int, Room> _roomrepository;
        private readonly IRepository<string, User> _userrepository;
        private readonly IRepository<int, Payment> _paymentrepository;
        private readonly IRepository<int, Hotel> _hotelrepository;

        public BookingService(IRepository<int, Booking> repository,
            IRepository<int, Room> roomrepository,
            IRepository<string, User> userrepository, IRepository<int, Payment> paymentrepository, IRepository<int, Hotel> hotelrepository)
        {
            _reservationrepository = repository;
            _roomrepository = roomrepository;
            _userrepository = userrepository;
            _paymentrepository = paymentrepository;
            _hotelrepository = hotelrepository;

        }


        public async Task<Booking> DeleteReservation(int id)
        {
        
            var Reservation = await GetReservation(id);
            if (Reservation != null)
            {
                await _reservationrepository.Delete(id);
                return Reservation;
            }
            throw new NotFoundException("Booking Not Found Exception");
        }

        public async Task<List<Booking>> GetAllReservations()
        {
          
            var Reservations = await _reservationrepository.GetAll();
            if (Reservations != null) { return Reservations; }
            throw new NotFoundException("Booking Not Found Exception");

        }

        public async Task<Booking> GetReservation(int id)
        {
         
            var Reservation = await _reservationrepository.GetById(id);
            if (Reservation != null) { return Reservation; }
            throw new NotFoundException("Booking Not Found Exception");
        }

        public async Task<List<Booking>> GetReservationsByRoomId(int roomId)
        {
           
            var Reservations = await GetAllReservations();
            if (Reservations == null)
            {
                throw new System.InvalidOperationException("No Reservations found.");
            }

            return Reservations.Where(b => b.RoomId == roomId).ToList();
        }


        //public async Task<List<Reservation>> GetReservationsByUserId(int userid)
        //{
        //    var Reservations=await GetAllReservations();
        //    if(Reservations==null) throw new NoReservationFoundException();
        //    List<Reservation> Reservations1 = new List<Reservation>();
        //    foreach (var item in Reservations)
        //    {
        //        if(item.UserId == userid) Reservations1.Add(item);
        //    }
        //    return Reservations1;

        //}

        public async Task<int> GetReservationsCount()
        {
            var Reservations = await GetAllReservations();
            return Reservations.Count;
        }

        public async Task<Booking> UpdateReservationStatus(int id, BookingStatus status)
        {
          
            var newReservation = await GetReservation(id);
            if (newReservation != null)
            {
                newReservation.Status = status;
                if (status == BookingStatus.Checkedout || status == BookingStatus.Cancelled)
                {
                    if (newReservation.Room != null)
                    {
                        newReservation.Room.Available = true;
                    }
                }
                else if (status == BookingStatus.Confirmed)
                {
                    if (newReservation.Room != null)
                    {
                        newReservation.Room.Available = false;
                    }
                }
                await _reservationrepository.Update(newReservation);
                return newReservation;
            }
            throw new NotFoundException("Booking Not Found Exception");
        }

        public async Task<List<Booking>> GetHotelReservations(int hotelId)
        {
          
            var allRooms = await _roomrepository.GetAll();
            var hotelRooms = allRooms.Where(r => r.HotelId == hotelId).Select(r => r.RoomId).ToList();

            var allReservations = await GetAllReservations();
            var hotelReservations = allReservations.Where(b => hotelRooms.Contains(b.RoomId)).ToList();

            return hotelReservations;
        }
        public async Task<bool> IsRoomAvailable(int roomId, DateTime checkInDate, DateTime checkOutDate)
        {
           
            var Reservations = await GetReservationsByRoomId(roomId);
            var FilteredReservations = Reservations.Where(x => x.Status == BookingStatus.Confirmed);
            foreach (var Reservation in FilteredReservations)
            {
                if ((checkInDate >= Reservation.CheckInDate && checkInDate < Reservation.CheckOutDate) ||
                    (checkOutDate > Reservation.CheckInDate && checkOutDate <= Reservation.CheckOutDate) ||
                    (checkInDate <= Reservation.CheckInDate && checkOutDate >= Reservation.CheckOutDate))
                {
                    return false;
                }
            }

            return true;
        }

        public async Task<Booking> AddReservation(BookingDTO Reservation1 )
        {
           
            Booking booking = BookingMapping.MapReservationDTOToEntity(Reservation1);
            bool isRoomAvailable = await IsRoomAvailable(booking.RoomId, booking.CheckInDate, booking.CheckOutDate);
            if (!isRoomAvailable)
            {
                throw new Exception("Room is not available for the specified dates");
            }

            //var user = await _userrepository.GetById(username);
            //if (user == null)
            //{
            //    throw new Exception("User does not exist");
            //}

            if (booking.CheckOutDate <= booking.CheckInDate)
            {
                throw new Exception("Check-out date must be after check-in date");
            }

            if (booking.CheckInDate.Date < DateTime.UtcNow.Date)
            {
                throw new Exception("Check-in date cannot be earlier than today");
            }

            var room = await _roomrepository.GetById(booking.RoomId);
            if (room == null)
            {
                throw new Exception("Room does not exist");
            }
            TimeSpan span = booking.CheckOutDate - booking.CheckInDate;
            float totalPrice = (float)span.TotalDays * room.Price;

            booking.TotalPrice = totalPrice;
            booking.Status = BookingStatus.Confirmed;
            booking.BookedDate = DateTime.UtcNow;
            booking.UserId = Reservation1.UserId;
            var addedReservation = await _reservationrepository.Add(booking);
            return addedReservation;
        }



        public async Task<List<Booking>> GetReservationsByUserId(int userId)
        {
            // Fetch all bookings
            var allReservations = await GetAllReservations();

            if (allReservations == null || !allReservations.Any())
            {
                throw new NotFoundException("No bookings found.");
            }

            // Filter reservations by UserId
            var userReservations = allReservations.Where(b => b.UserId == userId).ToList();

            if (userReservations == null || !userReservations.Any())
            {
                throw new NotFoundException($"No bookings found for user with ID {userId}");
            }

            return userReservations;
        }



        public async Task<List<Booking>> GetBookingsByOwnerId(int ownerId)
        {
            // Fetch all bookings
            var allReservations = await GetAllReservations();

            if (allReservations == null || !allReservations.Any())
            {
                throw new NotFoundException("No bookings found.");
            }

            // Fetch all rooms
            var allRooms = await _roomrepository.GetAll();

            if (allRooms == null || !allRooms.Any())
            {
                throw new NotFoundException("No rooms found.");
            }

            // Fetch all hotels
            var allHotels = await _hotelrepository.GetAll();

            if (allHotels == null || !allHotels.Any())
            {
                throw new NotFoundException("No hotels found.");
            }

            // Filter reservations by OwnerId
            var ownerReservations = allReservations.Where(b =>
            {
                var room = allRooms.FirstOrDefault(r => r.RoomId == b.RoomId);
                if (room != null)
                {
                    var hotel = allHotels.FirstOrDefault(h => h.HotelId == room.HotelId);
                    if (hotel != null)
                    {
                        return hotel.OwnerId == ownerId;
                    }
                }
                return false;
            }).ToList();

            if (ownerReservations == null || !ownerReservations.Any())
            {
                throw new NotFoundException($"No bookings found for owner with ID {ownerId}");
            }

            return ownerReservations;
        }



        public async Task<Booking> RefundReservation(int id)
        {
            var reservation = await GetReservation(id);
            if (reservation != null)
            {
                if (reservation.Status == BookingStatus.RefundRequested)
                {
                    reservation.Status = BookingStatus.Refunded;
                    await _reservationrepository.Update(reservation);
                    return reservation;
                }
                else
                {
                    throw new Exception("Reservation is not in requested status");
                }
            }
            throw new NotFoundException("Booking Not Found Exception");
        }

        public async Task<Booking> AskRefund(int id)
        {
            var reservation = await GetReservation(id);
            if (reservation != null)
            {
                if (reservation.Status == BookingStatus.Confirmed)
                {
                    reservation.Status = BookingStatus.RefundRequested;
                    await _reservationrepository.Update(reservation);
                    return reservation;
                }
                else
                {
                    throw new Exception("Reservation is not in confirmed status");
                }
            }
            throw new NotFoundException("Booking Not Found Exception");
        }
    }


          


 }



