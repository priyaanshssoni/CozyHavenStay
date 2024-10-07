using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CozyHavenStay.Api.Mapper;
using CozyHavenStay.Api.RepositoryImplementation;
using CozyHavenStay.Api.ServiceAbstractions;
using CozyHavenStay.Api.ServiceImplementation;
using CozyHavenStay.Data.DTOs;
using CozyHavenStay.Data.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CozyHavenStay.Api.Controllers
{
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewservice;
        private readonly IHotelService _hservice;

        public ReviewController(IReviewService reviewService,IHotelService _hservice)
        {
            _reviewservice = reviewService;
            _hservice = _hservice;
        }
        [HttpGet("AllReviews")]
        public async Task<ActionResult<List<Review>>> GetReviews()
        {
           
                return await _reviewservice.GetAllReviews();
            
        }
        [HttpGet("GetById")]
        public async Task<ActionResult<Review>> GetReviewById(int id)
        {
           
                return await _reviewservice.GetReview(id);
            
        }

        //[HttpGet("GetByHotelId")]
        //public async Task<List<Review>> GetHotelReviews(int id)
        //{
        //    return await _reviewservice.GetReviewsByHotelId(id);
        //}
        //[HttpGet("GetByUserId")]
        //public async Task<List<Review>> GetUserReviews(int id)
        //{
        //    return await _reviewservice.GetReviewsByUserId(id);
        //}


        //public async Task<ActionResult<Review>> AddReview(ReviewDTO review)
        //{

        //    return await _reviewservice.AddReview(review);

        //}
        //[HttpPost("AddReviewToHotel")]

        //public async Task<Review> AddReviewToHotel(ReviewDTO review)
        //{

        //    var hotel = await _hservice.GetHotel(review.HotelId);
        //    if (hotel == null)
        //    {
        //        throw new Exception("Hotel not found");
        //    }

        //    return await _reviewservice.AddReview(review);
        //}

        [HttpPost("addreviewtohotel")]
        public async Task<ActionResult<Review>> AddReviewToHotel(ReviewDTO review)
        {
            //var reviewt = ReviewMapping.MapReviewDTOToEntity(review);
                Review newReview = await _reviewservice.AddReviewToHotel(review.HotelId, review);
                return newReview;
           
        }


        [HttpPut("UpdateRating")]
        public async Task<ActionResult<Review>> UpdateRating(int id, float rating)
        {
            
                return await _reviewservice.UpdateReviewRating(id, rating);
            

        }
        [HttpDelete("DeleteReview")]
        public async Task<ActionResult<Review>> DeleteReview(int id)
        {
            
                return await _reviewservice.DeleteReview(id);
            
            

        }

        [HttpGet("GetByUserId")]
        public async Task<IActionResult> GetReviewsByUserId(int userId)
        {
            try
            {
                var reviews = await _reviewservice.GetReviewsByUserId(userId);
                return Ok(reviews);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving the reviews for the user.", error = ex.Message });
            }
        }


        [HttpGet("GetReviewsByOwnerId")]
        public async Task<ActionResult<List<Review>>> GetReviewsByHotelOwnerId(int ownerId)
        {
            try
            {
                var reviews = await _reviewservice.GetReviewsByHotelOwnerId(ownerId);
                return Ok(reviews);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

