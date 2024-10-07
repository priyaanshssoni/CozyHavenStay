using System;
using System.ComponentModel.DataAnnotations;

namespace CozyHavenStay.Data.Models
{
    public class Amenity
    {
        [Key]
        public int AmenityId { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<HotelAmenity>? HotelAmenity { get; set; }
        public ICollection<RoomAmenity>? RoomAmenity { get; set; }
    }
}

