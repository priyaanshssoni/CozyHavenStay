using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CozyHavenStay.Data.Enums;

namespace CozyHavenStay.Data.Models
{
    public class Room
    {
        [Key]
        public int RoomId { get; set; }

        [ForeignKey("Hotel")]
        public int HotelId { get; set; }

        [Required]
        public RoomType RoomType { get; set; }

        [Required]
        public float RoomSize { get; set; }

        [Required]
        public float Price { get; set; }

        [Required]
        public ICollection<string>? ImageLinks { get; set; }

        [Required]
        public int Capacity { get; set; }

        public bool Available { get; set; }

        public Hotel? Hotel { get; set; }

        public ICollection<RoomAmenity>? RoomAmenities { get; set; }

        public ICollection<Booking>? Bookings { get; set; }
        public Room()
        {
            ImageLinks = new List<string>();
        }
    }
}

