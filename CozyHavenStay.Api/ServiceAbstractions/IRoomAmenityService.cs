using System;
using CozyHavenStay.Data.Models;

namespace CozyHavenStay.Api.ServiceAbstractions
{
    public interface IRoomAmenityService
    {
        public Task<List<RoomAmenity>> GetRoomAmenities(int roomid);
    }
}

