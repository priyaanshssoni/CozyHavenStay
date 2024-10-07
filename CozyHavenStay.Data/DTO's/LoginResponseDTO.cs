using System;
namespace CozyHavenStay.Data.DTOs;


    public class LoginResponseDTO
{

    public int UserId { get; set; }

    public string Username { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string PhoneNumber { get; set; }

    public string Email { get; set; }

    public DateTime DateofBirth { get; set; }

    public string token { get; set; }
}

