using System;
using CozyHavenStay.Api.Mapper;
using CozyHavenStay.Api.RepositoryAbstractions;
using CozyHavenStay.Api.RepositoryImplementation;
using CozyHavenStay.Api.ServiceAbstractions;
using CozyHavenStay.Data.DTOs;
using CozyHavenStay.Data.Models;

namespace CozyHavenStay.Api.ServiceImplementation
{
    public class HotelService : IHotelService
    {
        private readonly IRepository<int, Hotel> _repository;
        private readonly IRoomService _roomService;
        private readonly IBookingService _bookingService;
        private readonly ICityService _cityservice;
        private readonly IHotelAmenityService _hotelAmenityService;

        public HotelService(IRepository<int, Hotel> repository, IHotelAmenityService hotelAmenityService,IBookingService bookingRepo,IRoomService roomRepo,ICityService cityRepo)
        {
            _repository = repository;
            _roomService = roomRepo;
            _bookingService = bookingRepo;
            _hotelAmenityService = hotelAmenityService;
            _cityservice = cityRepo;

        }

        public async Task<Hotel> AddHotel(HotelDTO hotel, int ownerId)
        {
            hotel.OwnerId = ownerId;
            Hotel newhotel = HotelMapping.MapDTOToHotel(hotel);
            newhotel = await _repository.Add(newhotel);
            return newhotel;
        }

        public async Task<Hotel> DeleteHotel(int id)
        {
            var hotel = await GetHotel(id);
            if (hotel != null)
            {
                return await _repository.Delete(id);
            }
            throw new Exception();

        }

        public async Task<List<Hotel>> GetAllHotels()
        {
            var hotels = await _repository.GetAll();
            if (hotels != null) { return hotels; }
            throw new Exception();
        }

        public async Task<Hotel> GetHotel(int id)
        {
            var hotel = await _repository.GetById(id);
            if (hotel != null) { return hotel; }
            throw new Exception();
        }

        public async Task<double> GetAverageRating(int hotelId)
        {
            var hotel = await GetHotel(hotelId);
            if (hotel != null)
            {
                var reviews = await GetHotelReviews(hotelId);
                if (reviews != null && reviews.Any())
                {
                    var ratings = reviews.Select(r => r.Rating).ToList();
                    var averageRating = ratings.Average();
                    return averageRating;
                }
            }
            return 0; // or throw an exception if no reviews found
        }


        //public async Task<List<Hotel>> GetHotelsByDestinationId(int destinationId)
        //{
        //    var hotels=await GetAllHotels();
        //    if (hotels == null) { throw new NoHotelFoundException(); }
        //    List<Hotel> hotels1 = new List<Hotel>();
        //    foreach(var hotel in hotels)
        //    {
        //        if (hotel.DestinationId == destinationId) { hotels1.Add(hotel); }
        //    }
        //    return hotels1;
        //}

        public async Task<Hotel> UpdateHotelDescription(int id, string description)
        {
            var hotel = await GetHotel(id);
            if (hotel != null)
            {
                hotel.Description = description;
                return await _repository.Update(hotel);
            }
            throw new Exception();
        }
        public async Task<Hotel> UpdateHotelOwner(int id, int ownerId)
        {
            //_logger.LogInformation("Updating owner for hotel with ID: {HotelId}", id);
            var hotel = await GetHotel(id);
            if (hotel != null)
            {
                hotel.OwnerId = ownerId;
                await _repository.Update(hotel);
                return hotel;
            }
            throw new Exception();
        }

        public async Task<List<Hotel>> GetHotelsByOwner(int ownerId)
        {

            try
            {
                var allHotels = await _repository.GetAll();

                var hotelsForOwner = allHotels.Where(h => h.OwnerId == ownerId).ToList();

                return hotelsForOwner;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<ICollection<Review>> GetHotelReviews(int hotelId)
        {

            var hotel = await GetHotel(hotelId);
            if (hotel != null)
            {
                return hotel.Reviews ?? new List<Review>();
            }
            throw new Exception();
        }


        public async Task<List<HotelAmenityDTO>> GetHotelAmenities(int hotelId)
        {
            var hotel = await _repository.GetById(hotelId);
            if (hotel != null)
            {
                if (hotel.HotelAmenities != null)
                {
                    var amenityDTOs = hotel.HotelAmenities
                        .Select(ha => new HotelAmenityDTO
                        {
                            AmenityId = ha.AmenityId,
                            AmenityName = ha.Amenity?.Name

                        })
                        .ToList();

                    return amenityDTOs;
                }
            }
            return new List<HotelAmenityDTO>();
        }


        //public async Task<ICollection<Room>> GetRoomsByHotelId(int hotelId)
        //{

        //    var hotel = await GetHotel(hotelId);
        //    if (hotel != null)
        //    {
        //        return hotel.Rooms;
        //    }
        //    throw new Exception();
        //}
        public async Task<Hotel> UpdateHotelDetails(HotelDTO hotelDTO)
        {

            var existingHotel = await GetHotel(hotelDTO.HotelId);
            if (existingHotel != null)
            {
                existingHotel.Name = hotelDTO.Name;
                existingHotel.Address = hotelDTO.Address;
                existingHotel.CityId = hotelDTO.CityId;
                existingHotel.City = hotelDTO.City;
                existingHotel.Description = hotelDTO.Description;
                existingHotel.NumberOfRooms = hotelDTO.NumberOfRooms;
                existingHotel.StartPrice = hotelDTO.StartPrice;
                existingHotel.EndPrice = hotelDTO.EndPrice;
                existingHotel.ImageLinks = existingHotel.ImageLinks;


                await _repository.Update(existingHotel);

                return existingHotel;
            }
            throw new Exception();
        }

        //public async Task<ICollection<Booking>> GetHotelBookings(int hotelId)
        //{
        //    var rooms = await _roomService.GetAllRooms();
        //    var reservations = await _bookingService.GetAllBookings();
        //    ICollection<Booking> hotelReservations = (from reservation in reservations
        //                                              join room in rooms on reservation.RoomId equals room.RoomId
        //                                              where room.HotelId == hotelId
        //                                              select reservation).ToList();
        //    if (hotelReservations != null || hotelReservations.Count > 0)
        //        return hotelReservations;
        //    throw new Exception();
        //}

        public async Task<Hotel> UpdateHotelDetails(Hotel hotel)
        {

            var existingHotel = await GetHotel(hotel.HotelId);
            if (existingHotel != null)
            {
                existingHotel.Name = hotel.Name;
                existingHotel.Address = hotel.Address;
                existingHotel.Description = hotel.Description;

                await _repository.Update(existingHotel);

                return existingHotel;
            }
            throw new Exception();
        }
        public async Task<List<Hotel>> GetHotelsByLocation(string location)
        {
            var hotels = await _repository.GetAll();
            return hotels.Where(h => h.City.ToLower() == location.ToLower()).ToList();
        }


        public async Task<List<Hotel>> GetHotelsByCriteria(string location, DateTime checkin, DateTime checkout, int capacity)
        {
            var hotels = await GetHotelsByLocation(location);
            var availableHotels = new List<Hotel>();
            var city = await _cityservice.GetCityByName(location);

            foreach (var hotel in hotels)
            {
                var availableRooms = hotel.Rooms.Where(room => room.Capacity >= capacity).ToList();
                foreach (var room in availableRooms)
                {
                    bool isRoomAvailable = await _bookingService.IsRoomAvailable(room.RoomId, checkin, checkout);
                    if (isRoomAvailable)
                    {
                        availableHotels.Add(hotel);
                        break;
                    }
                }
            }

            return availableHotels;
        }

        public Task<List<Hotel>> GetHotelsByDestinationId(int CityID)
        {
            throw new NotImplementedException();
        }

     
    }
}
