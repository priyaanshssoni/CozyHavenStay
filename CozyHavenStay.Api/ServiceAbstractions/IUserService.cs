using System;
using CozyHavenStay.Data.DTOs;
using CozyHavenStay.Data.Enums;
using CozyHavenStay.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace CozyHavenStay.Api.ServiceAbstractions
{
	public interface IUserService
	{
        //UserController
        public Task<LoginResponseDTO> Login(LoginUserDTO user);
        public Task<User> Register(RegisterUserDTO user);
        public Task<User> UpdatePassword(string username, string password);
        public Task<User> UpdateUserDetails(string username, string firstName, string lastName, string contactNumber, string email, DateTime dateofbirth);
        public Task<ICollection<Booking>> GetUserReservations(string username);
        public Task RequestRefund(int bookingId, DateTime refundRequestDate);
        public Task<ICollection<Review>> GetUserReviews(string username);
   
        //AdminController
        public Task<List<User>> GetAllUsers();
        public Task<User> GetUser(string username);
        public Task<User> GetUserbyId(int userId);
        public Task<User> DeleteUser(string username);
        public Task<List<User>> GetHotelOwners();
        public Task AssignRole(int userid, UserType role);

        //OwnerController
        //GetAllHotel
        public Task ProcessRefund(int bookingId);

    }
}

