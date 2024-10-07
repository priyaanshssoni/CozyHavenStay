using System;
using CozyHavenStay.Data.DTOs;
using CozyHavenStay.Data.Models;
using System.Security.Cryptography;
using System.Text;

namespace CozyHavenStay.Api.Mapper
{
	public class RegisterationMapper
	{
        private readonly User user;

        public RegisterationMapper(RegisterUserDTO registerUserDto)
        {
            if (registerUserDto == null)
                throw new ArgumentNullException(nameof(registerUserDto));

            user = new User();
          
            user.Username = registerUserDto.Username;
            user.UserType = Data.Enums.UserType.Customer;
            user.FirstName = registerUserDto.FirstName;
            user.LastName = registerUserDto.LastName;
            user.Email = registerUserDto.Email;
            user.RegistrationDate = DateTime.Now.Date;
            user.PhoneNumber = registerUserDto.PhoneNumber;
            user.DateofBirth = registerUserDto.DateofBirth;
            user.Password = registerUserDto.Password;
        }

        public User GetUser()
        {
            return user;
        }
    }
}

