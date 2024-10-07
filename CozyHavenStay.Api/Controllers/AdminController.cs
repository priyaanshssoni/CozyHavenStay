using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CozyHavenStay.Api.Exceptions;
using CozyHavenStay.Api.ServiceAbstractions;
using CozyHavenStay.Data.Enums;
using CozyHavenStay.Data.Models;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CozyHavenStay.Api.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
   

    public class AdminController : Controller
    {
        private readonly IUserService _userService;

        public AdminController(IUserService userService)
        {
            _userService = userService;

        }

    
        [HttpGet("getbyuserid")]
        public async Task<IActionResult> GetUserById(int userId)
        {

                var user = await _userService.GetUserbyId(userId);
                return Ok(user);
           
        }

        [HttpDelete("deleteUser")]
        public async Task<ActionResult<User>> DeleteUser(string username)
        {

            var deletedUser = await _userService.DeleteUser(username);
            return Ok(deletedUser);

        }

   
        [HttpGet("getallusers")]
        public async Task<ActionResult<List<User>>> GetUsers()
        {

            var users = await _userService.GetAllUsers();
            return Ok(users);


        }
       
        [HttpGet("gethotelowners")]
        public async Task<ActionResult<List<User>>> GetHotelOwners()
        {

            var hotelOwners = await _userService.GetHotelOwners();
            return Ok(hotelOwners);



        }

        [Authorize(Roles = "Admin")]
        [HttpPut("assignrole/{userid}")]
        public async Task<ActionResult> AssignRole(int userid, UserType role)
        {
            
            await _userService.AssignRole(userid, role);
           
            return Ok($"Role assigned successfully to user with id {userid}");
        }
    }
}

