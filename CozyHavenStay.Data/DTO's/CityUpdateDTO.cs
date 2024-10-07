using System;
namespace CozyHavenStay.Data.DTOs
{
	public class CityUpdateDTO
	{
        public int CityID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PinCode { get; set; }
        public ICollection<string>? ImageLinks { get; set; }
    }
}

