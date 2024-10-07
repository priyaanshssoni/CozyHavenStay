using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CozyHavenStay.Api.Exceptions;
using CozyHavenStay.Api.ServiceAbstractions;
using CozyHavenStay.Data.DTOs;
using CozyHavenStay.Data.Models;
using log4net;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CozyHavenStay.Api.Controllers
{
    [Route("api/auth")]
    public class UserController : Controller
    {

        private readonly IUserService _userService;
        private static readonly ILog log = LogManager.GetLogger(typeof(UserController));

        public UserController(IUserService userService)
        {
            _userService = userService;

        }


        [HttpPost("register")]
        public async Task<ActionResult<LoginUserDTO>> Register([FromBody]RegisterUserDTO user)
        {
            log.Info("GET request received");

            var result = await _userService.Register(user);
            log.Info($"Authorized User:{result}");
            return Ok(result);
        

        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDTO>> Login([FromBody]LoginUserDTO user)
        {

            log.Info("GET request received");
            var result = await _userService.Login(user);
            log.Info($"Authorized User:{result.Username}");
            return Ok(result);
           
        }

        [HttpPut("{username}/updatePassword")]
        public async Task<ActionResult<User>> UpdatePassword(string username, string password)
        {
          
                var updatedUser = await _userService.UpdatePassword(username, password);
                return Ok(updatedUser);
            
        }

        [HttpGet("{username}/reviews")]
        public async Task<ActionResult<ICollection<Review>>> GetUserReviews(string username)
        {
           
                var reviews = await _userService.GetUserReviews(username);
                return Ok(reviews);
            
        
        }


        [HttpPut("updateuserprofile/{username}")]
        public async Task<ActionResult> UpdateUserDetails(string username, string firstName, string lastName, string phonenumber, string email, DateTime dateofbirth)
        {

            var user = await _userService.UpdateUserDetails(username,  firstName,  lastName,  phonenumber,  email, dateofbirth);
                if (user != null)
                {
                    return Ok(user);
                }
                return NotFound($"User with username {username} not found.");
           
        }



    }
}

