using System;
namespace CozyHavenStay.Data.DTOs
{
	public class HotelDTO
	{
        public int HotelId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CityId { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public int NumberOfRooms { get; set; }
        public double StartPrice { get; set; }
        public double EndPrice { get; set; }
        
        public int OwnerId { get; set; }
        public ICollection<string> ImageLinks { get; set; }
    }
}

