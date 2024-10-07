using System;
namespace CozyHavenStay.Data.Models
{
    public class RoomAmenity
    {

        public int RoomID { get; set; }
        public Room? Room { get; set; }

        public int AmenityId { get; set; }
        public Amenity? Amenity { get; set; }
    }
}

