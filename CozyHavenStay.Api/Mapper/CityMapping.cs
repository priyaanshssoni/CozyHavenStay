using System;
using CozyHavenStay.Data.DTOs;
using CozyHavenStay.Data.Models;

namespace CozyHavenStay.Api.Mapper
{
	public class CityMapping
	{
         public static CityDTO MapCityToDTO(City city)
            {
                return new CityDTO
                {
                    //CityID = city.CityID,
                    Name = city.Name,
                    Description = city.Description,
                    PinCode = city.PinCode,
                    ImageLinks = city.ImageLinks.ToList()

                };
            }

            public static City MapDTOToCity(CityDTO cityyDTO)
            {
                return new City
                {
                    //CityID = cityyDTO.CityID,
                    Name = cityyDTO.Name,
                    Description = cityyDTO.Description,
                    PinCode = cityyDTO.PinCode,
                    ImageLinks = cityyDTO.ImageLinks != null ? cityyDTO.ImageLinks : new List<string>(),
                };
            }
        }
    }


