using System;
using System.Drawing.Printing;
using System.Linq.Expressions;
using AutoMapper;
using CozyHavenStay.Api.Exceptions;
using CozyHavenStay.Api.Mapper;
using CozyHavenStay.Api.RepositoryAbsractions;
using CozyHavenStay.Api.RepositoryAbstractions;
using CozyHavenStay.Api.RepositoryImplementation;
using CozyHavenStay.Api.Service;
using CozyHavenStay.Api.ServiceAbstractions;
using CozyHavenStay.Data.DTOs;
using CozyHavenStay.Data.Models;
using log4net;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace CozyHavenStay.Api.ServiceImplementation;

public class CityService : ICityService
{
    private static readonly ILog log = LogManager.GetLogger(typeof(UserService));
    private readonly IRepository<int, City> _repository;
    private readonly IHotelRepository _hotel;
    public CityService(IRepository<int, City> repository, IHotelRepository hotel)
    {
        _repository = repository;
        _hotel = hotel;
    }

    public async Task<City> AddCity(CityDTO City)
    {
        //if (city.ImageURLs == null || !city.ImageURLs.Any())
        //{
        //    throw new Exception("Image URL are required when adding city");
        //}
        City newcity = CityMapping.MapDTOToCity(City);
        newcity = await _repository.Add(newcity);
        return newcity;
    }

    public async Task<City> DeleteCity(int id)
    {
        var city = await GetCity(id);
        if (city != null)
        {
            return await _repository.Delete(id);
        }
        throw new Exception();
    }

    public async Task<List<City>> GetAllCities()
    {
        var cities = await _repository.GetAll();
        if (cities != null) { return cities; }
        throw new Exception();
    }

    public async Task<City> GetCity(int id)
    {
        var city = await _repository.GetById(id);
        if (city != null) { return city; }
        throw new NotFoundException($"City Not Found with {id}");
    }


    public async Task<City> UpdateCityDescription(int id, string description)
    {
        var city = await GetCity(id);
        if (city != null)
        {
            city.Description = description;
            return await _repository.Update(city);
        }
        throw new Exception();
    }

    public async Task<City> UpdateCityDetails(CityUpdateDTO cityDTO)
    {
        var existingCity = await GetCity(cityDTO.CityID);
        if (existingCity != null)
        {
            existingCity.Name = cityDTO.Name;
            existingCity.Description = cityDTO.Description;
            existingCity.PinCode = cityDTO.PinCode;
            existingCity.ImageLinks = cityDTO.ImageLinks;

            await _repository.Update(existingCity);

            return existingCity;
        }
        throw new Exception();
    }

    public async Task<City> GetCityByName(string location)
    {
        var cities = await _repository.GetAll();
        var searchcity =  cities.Where(h => h.Name.ToLower() == location.ToLower()).SingleOrDefault();
        searchcity.VisitCount++;
        _repository.Update(searchcity);
        return searchcity;
    }

    public async Task<List<Hotel>> GetHotelsByCityId(int cityId)
    {
        return await _hotel.GetHotelsByCityId(cityId);
    }

    public async Task<List<Hotel>> GetHotelsByCity(string cname)
    {

        var city = await GetCityByName(cname);

        if (city != null)
        {
            var cities = await _repository.GetAll();
            var searchcity = cities.Where(h => h.Name.ToLower() == cname.ToLower()).SingleOrDefault();
            searchcity.VisitCount++;
            _repository.Update(searchcity);
            return await GetHotelsByCityId(city.CityID);
        }
        else
        {
            return new List<Hotel>();
        }
    }
    public void AddHotelToCity(int hotelId, int cityId)
    {
        _hotel.AddHotelToCity(hotelId, cityId);
    }

    public void RemoveHotelFromCity(int hotelId, int cityId)
    {
        _hotel.RemoveHotelFromCity(hotelId, cityId);
    }

    public async Task<List<City>> GetTop5CitiesByVisitCount()
    {
        var cities = await _repository.GetAll();
        return cities
            .OrderByDescending(c => c.VisitCount)
            .Take(5)
            .ToList();
    }
}

  

    


