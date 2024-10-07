using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using CozyHavenStay.Data.Enums;

namespace CozyHavenStay.Data.Models
{
	public class User
	{
        [Key]
        public int UserId { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }


        public DateTime DateofBirth { get; set; }


        [Required]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        [Required]
        public string Password { get; set; }


        [Required]
        public UserType UserType { get; set; } 


        [Required]
        public DateTime RegistrationDate { get; set; } = DateTime.Now;



        public ICollection<Booking>? Bookings { get; set; }

        public ICollection<Review>? Reviews { get; set; }

        public ICollection<Hotel>? Hotels { get; set; }
    }
}

