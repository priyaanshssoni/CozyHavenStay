using System;
using CozyHavenStay.Data.Models;

namespace CozyHavenStay.Api.ServiceAbstractions
{
    public interface IHotelAmenityService
    {
        public Task<List<HotelAmenity>> GetHotelAmenities(int hotelId);
    }
}

