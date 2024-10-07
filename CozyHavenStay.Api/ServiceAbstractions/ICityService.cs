using System;
using CozyHavenStay.Data.DTOs;
using CozyHavenStay.Data.Models;

namespace CozyHavenStay.Api.ServiceAbstractions
{
	public interface ICityService
	{
        public Task<City> GetCity(int id);
        public Task<List<City>> GetAllCities();
        public Task<City> AddCity(CityDTO City);
        public Task<City> UpdateCityDescription(int id, string description);
        public Task<City> DeleteCity(int id);
        public Task<City> GetCityByName(string cname);
        public Task<List<Hotel>> GetHotelsByCity(string cname);
        public Task<City> UpdateCityDetails(CityUpdateDTO cityDTO);
        public void AddHotelToCity(int hotelId, int cityId);
        public void RemoveHotelFromCity(int hotelId, int cityId);
        public Task<List<City>> GetTop5CitiesByVisitCount();
        //public Task<Hotel> UpdateCityDetails(Hotel hotel);
    }
}

