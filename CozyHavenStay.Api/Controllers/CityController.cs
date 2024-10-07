using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using CozyHavenStay.Api.Service;
using CozyHavenStay.Api.ServiceAbstractions;
using CozyHavenStay.Api.ServiceImplementation;
using CozyHavenStay.Data.DTOs;
using CozyHavenStay.Data.Models;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CozyHavenStay.Api.Controllers
{
    [Route("api/[controller]")]
    public class CityController : Controller
    {
        private readonly ICityService _cityservice;

        public CityController(ICityService cityservice)
        {
            _cityservice = cityservice;
        }


        [HttpPost("addCity")]
        public async Task<IActionResult> AddCity([FromBody]CityDTO city)
        {

            var addcity = await _cityservice.AddCity(city);
            return Ok(addcity);



        }


        [HttpGet("getallcities")]
        public async Task<IActionResult> GetAllCities()
        {
         
                var cities = await _cityservice.GetAllCities();
                return Ok(cities);
            
            

        }

        [HttpGet("getcitybyid")]
        public async Task<ActionResult<City>> GetCityById(int id)
        {
            return await _cityservice.GetCity(id);
        }


        [HttpPut("updatecity")]
        public async Task<ActionResult<City>> UpdateCity(int id, CityUpdateDTO cityDTO)
        {
            return await _cityservice.UpdateCityDetails(cityDTO);
        }

        [HttpDelete("deletecity")]
        public async Task<ActionResult<City>> DeleteCity(int id)
        {
            return await _cityservice.DeleteCity(id);
        }


        [HttpGet("getbyname")]
        public async Task<ActionResult<City>> GetCityByName(string location)
        {
            return await _cityservice.GetCityByName(location);
        }


        [HttpGet("{cname}/hotels")]
        public async Task<ActionResult<List<Hotel>>> GetHotelsByCity(string cname)
        {
            return await _cityservice.GetHotelsByCity(cname);
        }

        [HttpPost("addHotelToCity")]
        public IActionResult AddHotelToCity(int hotelId, int cityId)
        {
           
                _cityservice.AddHotelToCity(hotelId, cityId);
                return Ok("Hotel added to city successfully");
         
           
        }

        [HttpDelete("removeHotelFromCity")]
        public IActionResult RemoveHotelFromCity(int hotelId, int cityId)
        {
          
                _cityservice.RemoveHotelFromCity(hotelId, cityId);
                return Ok("Hotel removed from city successfully");
           
        }

        [HttpGet("top5")]
        public async Task<IActionResult> GetTop5CitiesByVisitCount()
        {
                var topCities = await _cityservice.GetTop5CitiesByVisitCount();
                if (topCities == null || topCities.Count == 0)
                {
                    return NotFound("No cities found.");
                }
                return Ok(topCities);
          
        }









    }
}

