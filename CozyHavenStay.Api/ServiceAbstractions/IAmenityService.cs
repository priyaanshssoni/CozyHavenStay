using System;
using CozyHavenStay.Data.DTOs;
using CozyHavenStay.Data.Models;

namespace CozyHavenStay.Api.ServiceAbstractions
{
    public interface IAmenityService
    {
        Task<Amenity> AddAmenity(AmenityDTO amenity);
        Task<Amenity> DeleteAmenity(int amenityId);
        Task<Amenity> GetAmenity(int amenityId);
        Task<List<Amenity>> GetAllAmenities();
        Task<AmenityDTO> UpdateAmenity(int amenityId, string Name);
    }
}

