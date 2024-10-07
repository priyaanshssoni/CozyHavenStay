using System;
using Microsoft.IdentityModel.Tokens;
using CozyHavenStay.Data.DTOs;
using CozyHavenStay.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using CozyHavenStay.Api.Mapper;
using Microsoft.EntityFrameworkCore;
using CozyHavenStay.Api.DataContext;
using CozyHavenStay.Api.Controllers;
using log4net;
using System.Security.Cryptography;
using CozyHavenStay.Data.Enums;
using CozyHavenStay.Api.RepositoryAbstractions;
using CozyHavenStay.Api.ServiceAbstractions;
using CozyHavenStay.Api.Exceptions;

namespace CozyHavenStay.Api.Service
{
	public class UserService : IUserService
	{
        private readonly IRepository<string, User> _repo;
        private readonly AppDbContext _context;
        //private readonly ITokenService _tokenservice;
        private readonly IConfiguration _configuration;
        private static readonly ILog log = LogManager.GetLogger(typeof(UserService));

        public UserService(IRepository<string, User> repo, IConfiguration config,AppDbContext context)
		{
            _repo = repo;
            _configuration = config;
            _context = context;
        }

        public async Task<User> DeleteUser(string username)
        {

            var user = await GetUser(username);

            log.Info($"Deleting user: {username}"); 

            if (user != null)
            {
                await _repo.Delete(username);
                return user;
            }
            throw new Exception("User Not Found");

        }

        public Task<List<User>> GetAllUsers()
        {
            log.Info("Getting all the Users");
            var users = _repo.GetAll();
            if (users != null) return users;
            throw new Exception("User Not Found"); ;
        }

        public async Task<List<User>> GetHotelOwners()
        {
            log.Info("Getting All Hotel Owners");
            var users = await _repo.GetAll();
            if (users == null || !users.Any())
            {
                throw new Exception($"No Hotel Owners Found");
            }

            return users.Where(user => user.UserType == UserType.HotelOwner).ToList();
        }

        public Task<User> GetUser(string username)
        {
            log.Info($"Getting User: {username}");
            var user = _repo.GetById(username);
            if (user == null) throw new Exception($"User Not Found : {username}");
            return user;
        }

        public async Task AssignRole(int userid, UserType role)
        {

            var user = await _repo.GetAll();
            var specificuser = user.FirstOrDefault(u => u.UserId == userid);

            if (specificuser == null) throw new UserNotFoundException($"User Not Found with Id : {userid}");

            specificuser.UserType = role;
            await _repo.Update(specificuser);
            log.Info($"Role assigned successfully to user with id {userid}");
        }

        public async Task<User> GetUserbyId(int userid)
        {
            var users = await _repo.GetAll();
            var user = users.FirstOrDefault(u => u.UserId == userid);
            if (user == null) throw new UserNotFoundException($"User Not Found with Id : {userid}");
            return user;
        }

        public async Task<LoginResponseDTO> Login(LoginUserDTO user)
        {
            log.Info($"Log in for User: {user.Username}");
            var myuser = _context.Users
                 .Where(u => u.Username == user.Username && u.Password == user.Password)
                 .FirstOrDefault();

            if (myuser == null)
            {
                log.Info($"User Not Found: {user.Username}");
                throw new Exception("Unauthorized User");
            }

            TokenService jwtTokenString = new TokenService(_configuration);
            //string tokenString = jwtTokenString.GenerateJWT(user.Username, myuser.UserType.ToString().ToLower());
            string tokenString = jwtTokenString.GenerateJWT(user.Username, myuser.UserType);
            log.Info($"User logged in successfully: {user.Username}");

            LoginResponseDTO abc = new LoginResponseDTO();
            abc.FirstName = myuser.FirstName;
            abc.LastName = myuser.LastName;
            abc.Email = myuser.Email;
            abc.PhoneNumber = myuser.PhoneNumber;
            abc.Username = myuser.Username;
            abc.UserId = myuser.UserId;
            abc.token = tokenString;


            return abc;

        }



        public async Task<User> Register(RegisterUserDTO user)
        {
            log.Info($"Registering For User: {user.Username}");
            User myuser = new RegisterationMapper(user).GetUser();

            myuser = await _repo.Add(myuser);

            return myuser;
        }

        public async Task<User> UpdatePassword(string username, string password)
        {
            log.Info($"Updating password for User: {username}");
            var user = await _repo.GetById(username);
            if (user != null)
            {
                user.Password = password;
                await _repo.Update(user);
              log.Info($"Password updated successfully for user: {username}");
                return user;
            }
            throw new Exception("User Not Found");
        }

        public async Task<User> UpdateUserDetails(string username, string firstName, string lastName, string phonenumber, string email, DateTime dateofbirth)
        {
            log.Info($"Updating User Profile: {username}");
            var existingUser = await GetUser(username);
            if (existingUser != null)
            {
                existingUser.Username = username;
                existingUser.FirstName = firstName;
                existingUser.LastName = lastName;
                existingUser.PhoneNumber = phonenumber;
                existingUser.Email = email;
                existingUser.DateofBirth = dateofbirth;

                existingUser = await _repo.Update(existingUser);
                return existingUser;
            }
            return null;
        }

        public async Task<ICollection<Review>> GetUserReviews(string username)
        {
           log.Info($"Getting Reviews for user: {username}");
            var user = await GetUser(username);
            if (user != null)
            {
                var reviews = user.Reviews;
                if (reviews.IsNullOrEmpty()) { throw new NotFoundException(username); }
                return reviews;
            }
            throw new UserNotFoundException(username);
        }

        public async Task RequestRefund(int bookingId, DateTime refundRequestDate)
        {
            // Get the booking entity
            var booking = await _context.Bookings.FindAsync(bookingId);

            // Check if the refund request is made before the booking date
            if (refundRequestDate < booking.CheckInDate)
            {
                // Update the booking status to "RefundRequested"
                booking.Status = BookingStatus.RefundRequested;

                // Save changes to the database
                await _context.SaveChangesAsync();
            }
            else
            {
                // Throw an exception or return an error message if the refund request is made after the booking date
                throw new System.InvalidOperationException("Refund request must be made before the booking date.");
            }
        }

        public Task<ICollection<Booking>> GetUserBookings(string username)
        {
            throw new System.NotImplementedException();
        }


        public async Task ProcessRefund(int bookingId)
        {
            // Get the booking entity
            var booking = await _context.Bookings.FindAsync(bookingId);

            // Check if the booking status is "RefundRequested"
            if (booking.Status == BookingStatus.RefundRequested)
            {
                // Get the payment entity using the booking id
                var payment = await _context.Payments
                    .Where(p => p.BookingId == bookingId)
                    .FirstOrDefaultAsync();

                if (payment != null)
                {
                    // Update the payment status to "Refunded"
                    payment.Status = PaymentStatus.Refunded;

                    // Update the booking status to "Cancelled"
                    booking.Status = BookingStatus.Cancelled;

                    // Save changes to the database
                    await _context.SaveChangesAsync();
                }
                else
                {
                    // Throw an exception or return an error message if the payment entity is not found
                    throw new System.InvalidOperationException("Payment entity not found for the given booking id.");
                }
            }
            else
            {
                // Throw an exception or return an error message if the booking status is not "RefundRequested"
                throw new System.InvalidOperationException("Booking status must be 'RefundRequested' to process refund.");
            }
        }

        public Task<ICollection<Booking>> GetUserReservations(string username)
        {
            throw new System.NotImplementedException();
        }
    }
}

