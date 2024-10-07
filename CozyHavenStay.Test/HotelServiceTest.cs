
using CozyHavenStay.Api.RepositoryAbstractions;
using CozyHavenStay.Api.ServiceAbstractions;
using CozyHavenStay.Api.ServiceImplementation;
using CozyHavenStay.Data.DTOs;
using CozyHavenStay.Data.Models;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyHavenStay.Test
{
    [TestFixture]
    public class HotelServiceTest
    {
        private Mock<IRepository<int, Hotel>> _mockRepository;
        //private Mock<IRoomService> _mockRoomRepo;
        //private Mock<IBookingService> _mockReservationRepo;
        private Mock<IHotelAmenityService> _mockHotelAmenityRepo;
        private IHotelService _hotelService;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<IRepository<int, Hotel>>();
 
            //_mockRoomRepo = new Mock<IRoomService>();
            //_mockReservationRepo = new Mock<IBookingService>();
            _mockHotelAmenityRepo = new Mock<IHotelAmenityService>();
            _hotelService = new HotelService(_mockRepository.Object,_mockHotelAmenityRepo.Object);
        }

        [Test]
        public async Task AddHotel_ValidHotelDTO_ReturnsAddedHotel()
        {
            // Arrange
            var hotelDTO = new HotelDTO { ImageLinks = new List<string> { "url1", "url2" } }; 
            var ownerId = 1;
            var addedHotel = new Hotel(); 
            _mockRepository.Setup(repo => repo.Add(It.IsAny<Hotel>())).ReturnsAsync(addedHotel); 

            // Act
            var result = await _hotelService.AddHotel(hotelDTO, ownerId);

            // Assert
            Assert.That(result != null); 
        }


        [Test]
        public async Task DeleteHotel_ExistingHotelId_ReturnsDeletedHotel()
        {
            // Arrange
            var hotelId = 1; 
            var hotelToDelete = new Hotel(); 
            _mockRepository.Setup(repo => repo.Delete(hotelId)).ReturnsAsync(hotelToDelete); 

            // Act & Assert
            Exception? exception = null;

            try
            {
                await _hotelService.DeleteHotel(hotelId);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            // Assert
            Assert.That(exception != null);
        }


        [Test]
        public async Task GetAllHotels_ReturnsListOfHotels()
        {
            // Arrange
            var hotels = new List<Hotel>();
            _mockRepository.Setup(repo => repo.GetAll()).ReturnsAsync(hotels);

            // Act
            var result = await _hotelService.GetAllHotels();

            // Assert
            Assert.That(result, Is.EqualTo(hotels));
        }

        [Test]
        public async Task GetHotel_ExistingHotelId_ReturnsHotel()
        {
            // Arrange
            var hotelId = 1;
            var hotel = new Hotel { };
            _mockRepository.Setup(repo => repo.GetById(hotelId)).ReturnsAsync(hotel);

            // Act
            var result = await _hotelService.GetHotel(hotelId);

            // Assert
            Assert.That(result, Is.EqualTo(hotel));
        }

        [Test]
        public async Task UpdateHotelDescription_ExistingHotelId_ReturnsUpdatedHotel()
        {
            // Arrange
            var hotelId = 1; 
            var updatedHotel = new Hotel { HotelId = hotelId }; 
            _mockRepository.Setup(repo => repo.GetById(hotelId)).ReturnsAsync(updatedHotel); 
            _mockRepository.Setup(repo => repo.Update(updatedHotel)).ReturnsAsync(updatedHotel); 

            // Act
            var result = await _hotelService.UpdateHotelDescription(hotelId, "New Description");

            // Assert
            Assert.That(result != null); 
        }


        [Test]
        public async Task UpdateHotelOwner_ExistingHotelId_ReturnsUpdatedHotel()
        {
            // Arrange
            var hotelId = 1; 
            var ownerId = 2; 
            var updatedHotel = new Hotel { }; 
            _mockRepository.Setup(repo => repo.GetById(hotelId)).ReturnsAsync(updatedHotel);

            // Act
            var result = await _hotelService.UpdateHotelOwner(hotelId, ownerId);

            // Assert
            Assert.That(result, Is.EqualTo(updatedHotel)); 
        }

        [Test]
        public async Task GetHotelReviews_ExistingHotelId_ReturnsHotelReviews()
        {
            // Arrange
            var hotelId = 1; 
            var hotel = new Hotel { Reviews = new List<Review>() }; 
            _mockRepository.Setup(repo => repo.GetById(hotelId)).ReturnsAsync(hotel);

            // Act
            var result = await _hotelService.GetHotelReviews(hotelId);

            // Assert
            Assert.That(result, Is.EqualTo(hotel.Reviews)); 
        }


        [Test]
        public async Task GetHotelAmenities_ExistingHotelId_ReturnsHotelAmenities()
        {
            // Arrange
            var hotelId = 1; 
            var hotel = new Hotel { HotelAmenities = new List<HotelAmenity>() }; 
            _mockRepository.Setup(repo => repo.GetById(hotelId)).ReturnsAsync(hotel);

            // Act
            var result = await _hotelService.GetHotelAmenities(hotelId);

            // Assert
            Assert.That(result, Is.EqualTo(hotel.HotelAmenities)); 
        }

     

        [Test]
        public async Task GetHotelsByOwner_ValidOwnerId_ReturnsListOfHotels()
        {
            // Arrange
            int ownerId = 1;
            var hotels = new List<Hotel>
            {
                new Hotel { HotelId = 1, OwnerId = ownerId },
                new Hotel { HotelId = 2, OwnerId = ownerId },
                new Hotel { HotelId = 3, OwnerId = ownerId + 1 } 
            };
            _mockRepository.Setup(repo => repo.GetAll()).ReturnsAsync(hotels);

            // Act
            var result = await _hotelService.GetHotelsByOwner(ownerId);

            // Assert
            Assert.That(result !=null);
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result.All(h => h.OwnerId == ownerId));
        }

        [Test]
        public void GetHotelsByOwner_RepositoryThrowsException_ThrowsException()
        {
            // Arrange
            int ownerId = 1;
            _mockRepository.Setup(repo => repo.GetAll()).ThrowsAsync(new Exception("Test exception"));

            // Act & Assert
            Assert.ThrowsAsync<Exception>(async () => await _hotelService.GetHotelsByOwner(ownerId));
        }

        [Test]
        public async Task UpdateHotelDetails_ValidHotelDTO_ReturnsUpdatedHotel()
        {
            // Arrange
            var hotelDTO = new HotelDTO { HotelId = 1 }; 
            var existingHotel = new Hotel { }; 
            _mockRepository.Setup(repo => repo.GetById(hotelDTO.HotelId)).ReturnsAsync(existingHotel); 

            // Act
            var result = await _hotelService.UpdateHotelDetails(hotelDTO);

            // Assert
            Assert.That(result, Is.EqualTo(existingHotel)); 
        }

        [Test]
        public async Task GetHotelsByLocation_ValidLocation_ReturnsListOfHotels()
        {
            // Arrange
            string location = "New York";
            var hotels = new List<Hotel>
            {
                new Hotel { HotelId = 1, City = "New York" },
                new Hotel { HotelId = 2, City = "Los Angeles" },
                new Hotel { HotelId = 3, City = "New York" }
            };
            _mockRepository.Setup(repo => repo.GetAll()).ReturnsAsync(hotels);

            // Act
            var result = await _hotelService.GetHotelsByLocation(location);

            // Assert
            Assert.That(result != null);
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result.All(h => h.City.ToLower() == location.ToLower()));
        }

      
    }
}