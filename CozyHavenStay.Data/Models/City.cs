using System;
using System.ComponentModel.DataAnnotations;

namespace CozyHavenStay.Data.Models
{

        public class City
        {
            [Key]
            public int CityID { get; set; }

            [Required]
            public string Name { get; set; }
            public string Description { get; set; }
            public string PinCode { get; set; }
            

            public DateTime CreateDat { get; set; } = DateTime.Now;
            public int VisitCount { get; set; }
            [Required]
            public ICollection<string>? ImageLinks { get; set; }

            public ICollection<Hotel>? Hotels { get; set; } 


        public City()
            {
            ImageLinks = new List<string>();
            }

        }

}


