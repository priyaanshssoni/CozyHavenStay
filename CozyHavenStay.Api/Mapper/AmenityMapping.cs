using System;
using CozyHavenStay.Data.DTOs;
using CozyHavenStay.Data.Models;

namespace CozyHavenStay.Api.Mapper
{
    public class AmenityMapping
    {
        public Amenity _amenity;

        public AmenityMapping(AmenityDTO amenitydto)
        {
            _amenity = new Amenity
            {
                AmenityId = amenitydto.Id,
                Name = amenitydto.Name
            };
        }

        public Amenity GetAmenity()
        {

            return _amenity;
        }
    }
}

