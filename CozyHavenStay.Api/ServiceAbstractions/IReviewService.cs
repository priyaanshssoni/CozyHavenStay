using System;
using CozyHavenStay.Data.DTOs;
using CozyHavenStay.Data.Models;

namespace CozyHavenStay.Api.ServiceAbstractions
{
    public interface IReviewService
    {
        public Task<Review> GetReview(int id);
        public Task<List<Review>> GetAllReviews();
        public Task<Review> AddReview(ReviewDTO review);
        public Task<Review> UpdateReviewRating(int id, float rating);
        public Task<Review> DeleteReview(int id);
        public Task<Review> AddReviewToHotel(int hotelId, ReviewDTO review);
        //public Task<List<Review>> GetReviewsByUserId(int userId);
        //public Task<List<Review>> GetReviewsByHotelId(int hotelId);
        public Task<List<Review>> GetReviewsByUserId(int userId);
        public Task<List<Review>> GetReviewsByHotelOwnerId(int ownerId);
    }
}

