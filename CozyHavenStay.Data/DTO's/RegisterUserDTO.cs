using System;
using CozyHavenStay.Data.Enums;

namespace CozyHavenStay.Data.DTOs
{
	public class RegisterUserDTO
	{
        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public DateTime DateofBirth { get; set; }
    }
}

