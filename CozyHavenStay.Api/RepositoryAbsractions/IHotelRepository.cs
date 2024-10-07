using System;
using CozyHavenStay.Api.RepositoryAbstractions;
using CozyHavenStay.Data.Models;

namespace CozyHavenStay.Api.RepositoryAbsractions
{
	public interface IHotelRepository  : IRepository<int, Hotel>
	{
        Task<List<Hotel>> GetHotelsByCityId(int cityId);
        void AddHotelToCity(int hotelId, int cityId);
        void RemoveHotelFromCity(int hotelId, int cityId);
    }
}

