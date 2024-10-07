
using CozyHavenStay.Api.RepositoryAbstractions;
using CozyHavenStay.Api.ServiceAbstractions;
using CozyHavenStay.Api.ServiceImplementation;
using CozyHavenStay.Data.DTOs;
using CozyHavenStay.Data.Enums;
using CozyHavenStay.Data.Models;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CozyHavenStay.Test
{
    [TestFixture]
    public class BookingServiceTest
    {
        private Mock<IRepository<int, Booking>> _mockReservationRepository;
        private Mock<IRepository<int, Room>> _mockRoomRepository;
        private Mock<IRepository<string, User>> _mockUserRepository;
        private Mock<IRepository<int, Payment>> _mockPayRepository;
        private IBookingService _reservationService;

        [SetUp]
        public void Setup()
        {
            _mockReservationRepository = new Mock<IRepository<int, Booking>>();

            _mockRoomRepository = new Mock<IRepository<int, Room>>();
            _mockUserRepository = new Mock<IRepository<string, User>>();
            _mockPayRepository = new Mock<IRepository<int, Payment>>();

            _reservationService = new BookingService(_mockReservationRepository.Object, _mockRoomRepository.Object, _mockUserRepository.Object, _mockPayRepository.Object);
        }

        [Test]
        public async Task DeleteReservation_ExistingReservationId_ReturnsDeletedReservation()
        {
            // Arrange
            var reservationId = 1;  
            var reservationToDelete = new Booking();  
            _mockReservationRepository.Setup(repo => repo.GetById(reservationId)).ReturnsAsync(reservationToDelete);  
            _mockReservationRepository.Setup(repo => repo.Delete(reservationId)).ReturnsAsync(reservationToDelete); 

            // Act
            var result = await _reservationService.DeleteReservation(reservationId);

            // Assert
            Assert.That(result != null); 
        }

        [Test]
        public async Task GetAllReservations_ReturnsListOfReservations()
        {
            // Arrange
            var reservations = new List<Booking>(); 
            _mockReservationRepository.Setup(repo => repo.GetAll()).ReturnsAsync(reservations); 

            // Act
            var result = await _reservationService.GetAllReservations();

            // Assert
            Assert.That(result != null);
        }

        [Test]
        public async Task GetReservation_ExistingReservationId_ReturnsReservation()
        {
            // Arrange
            var reservationId = 1;  
            var reservation = new Booking();  
            _mockReservationRepository.Setup(repo => repo.GetById(reservationId)).ReturnsAsync(reservation);

            // Act
            var result = await _reservationService.GetReservation(reservationId);

            // Assert
            Assert.That(result != null); 
        }

        [Test]
        public async Task GetReservationsByRoomId_ExistingRoomId_ReturnsListOfReservations()
        {
            // Arrange
            var roomId = 1; 
            var reservations = new List<Booking>(); 
            _mockReservationRepository.Setup(repo => repo.GetAll()).ReturnsAsync(reservations); 

            // Act
            var result = await _reservationService.GetReservationsByRoomId(roomId);

            // Assert
            Assert.That(result != null); 
        }

        [Test]
        public async Task GetReservationsCount_ReturnsNumberOfReservations()
        {
            // Arrange
            var reservations = new List<Booking>(); 
            _mockReservationRepository.Setup(repo => repo.GetAll()).ReturnsAsync(reservations); 

            // Act
            var result = await _reservationService.GetReservationsCount();

            // Assert
            Assert.That(result != null); 
        }

        [Test]
        public async Task UpdateReservationStatus_ExistingReservationId_ReturnsUpdatedReservation()
        {
            // Arrange
            var reservationId = 1;  
            var status = BookingStatus.Checkedout;  
            var reservation = new Booking
            {
                Room = new Room()  
            };  
            _mockReservationRepository.Setup(repo => repo.GetById(reservationId)).ReturnsAsync(reservation);  
            _mockReservationRepository.Setup(repo => repo.Update(It.IsAny<Booking>())).ReturnsAsync(reservation);  

            // Act
            var result = await _reservationService.UpdateReservationStatus(reservationId, status);

            // Assert
            Assert.That(result != null); 
        }

        [Test]
        public async Task GetHotelReservations_ExistingHotelId_ReturnsListOfReservations()
        {
            // Arrange
            var hotelId = 1; 
            var rooms = new List<Room>();
            var reservations = new List<Booking>();
            _mockRoomRepository.Setup(repo => repo.GetAll()).ReturnsAsync(rooms); 
            _mockReservationRepository.Setup(repo => repo.GetAll()).ReturnsAsync(reservations); 

            // Act
            var result = await _reservationService.GetHotelReservations(hotelId);

            // Assert
            Assert.That(result != null); 
        }

        [Test]
        public async Task IsRoomAvailable_AvailableRoom_ReturnsTrue()
        {
            // Arrange
            var roomId = 1;
            var checkInDate = DateTime.Now.AddDays(5); 
            var checkOutDate = DateTime.Now.AddDays(10);
            var reservations = new List<Booking>(); 
            _mockReservationRepository.Setup(repo => repo.GetAll()).ReturnsAsync(reservations); 

            // Act
            var result = await _reservationService.IsRoomAvailable(roomId, checkInDate, checkOutDate);

            // Assert
            Assert.That(result != null); 
        }

        [Test]
        public async Task AddReservation_ValidReservationDTO_ReturnsAddedReservation()
        {
            // Arrange
            var reservationDto = new BookingDTO
            {
                CheckInDate = DateTime.Now.AddDays(1), 
                CheckOutDate = DateTime.Now.AddDays(3),
            };
            var username = "testuser"; 
            var reservation = new Booking(); 
            _mockReservationRepository.Setup(repo => repo.Add(It.IsAny<Booking>())).ReturnsAsync(reservation);
            _mockUserRepository.Setup(repo => repo.GetById(username)).ReturnsAsync(new User());
            _mockRoomRepository.Setup(repo => repo.GetById(It.IsAny<int>())).ReturnsAsync(new Room()); 
            _mockReservationRepository.Setup(repo => repo.GetAll()).ReturnsAsync(new List<Booking>());

            // Act
            var result = await _reservationService.AddReservation(reservationDto, username);

            // Assert
            Assert.That(result != null); 
        }
    }
}
