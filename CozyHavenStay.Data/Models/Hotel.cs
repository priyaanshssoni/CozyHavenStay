using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CozyHavenStay.Data.Models
{
	public class Hotel
	{
        [Key]
        public int HotelId { get; set; }

     
        [ForeignKey("City")]
        public int CityId { get; set; }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public string City { get; set; }
        public string Address { get; set; }
     
     
        [Required]
        public ICollection<string>? ImageLinks { get; set; }

        public int NumberOfRooms { get; set; }
        //public int NumberOfRooms => Rooms?.Count ?? 0;
        public double StartPrice { get; set; }
        public double EndPrice { get; set; }

        [ForeignKey("User")]
        public int OwnerId { get; set; }

        public User? Owner { get; set; }
        public City? Cityy { get; set; }
        public ICollection<Room>? Rooms { get; set; }
        public ICollection<Review>? Reviews { get; set; }
        public ICollection<HotelAmenity>? HotelAmenities { get; set; }

        public Hotel()
        {
            ImageLinks = new List<string>();
        }
    }
}

