using System;
using CozyHavenStay.Api.Mapper;
using CozyHavenStay.Api.RepositoryAbstractions;
using CozyHavenStay.Api.RepositoryImplementation;
using CozyHavenStay.Api.ServiceAbstractions;
using CozyHavenStay.Data.DTOs;
using CozyHavenStay.Data.Models;

namespace CozyHavenStay.Api.ServiceImplementation
{
    public class ReviewService : IReviewService
    {
        private readonly IRepository<int, Review> _repository;
        private readonly IRepository<int, Hotel> _hrepository;

        public ReviewService(IRepository<int, Review> repository, IRepository<int, Hotel> hrepository)
        {
            _repository = repository;
            _hrepository = hrepository;
        }
        public async Task<Review> AddReview(ReviewDTO review)
        {
            Review newreview = ReviewMapping.MapReviewDTOToEntity(review);
            return await _repository.Add(newreview);
        }

        public async Task<Review> DeleteReview(int id)
        {
            var review = await GetReview(id);
            if (review != null)
            {
                await _repository.Delete(id);
                return review;
            }
            throw new Exception();
        }

        public Task<List<Review>> GetAllReviews()
        {
            var reviews = _repository.GetAll();
            if (reviews != null) return reviews;
                    
            throw new Exception();
        }

        public Task<Review> GetReview(int id)
        {
            var review = _repository.GetById(id);
            if (review != null) return review;
            throw new Exception();
        }

        public async Task<Review> UpdateReviewRating(int id, float rating)
        {
            var hotel = await GetReview(id);
            if (hotel != null)
            {
                hotel.Rating = rating;
                await _repository.Update(hotel);
                return hotel;
            }
            throw new Exception();
        }

        public async Task<Review> AddReviewToHotel(int hotelId, ReviewDTO review)
        {
            // Map the ReviewDTO to a Review entity
            Review newReview = ReviewMapping.MapReviewDTOToEntity(review);

            // Add the review to the review repository
            newReview = await _repository.Add(newReview);

            // Get the hotel entity
            Hotel hotel = await _hrepository.GetById(hotelId);

            // Add the review to the hotel's review list
            hotel.Reviews.Add(newReview);

            // Update the hotel entity
            await _hrepository.Update(hotel);

            return newReview;
        }


        public async Task<List<Review>> GetReviewsByUserId(int userId)
        {
            // Fetch all reviews
            var allReviews = await _repository.GetAll();

            if (allReviews == null || !allReviews.Any())
            {
                throw new Exception("No reviews found.");
            }

            // Filter reviews by UserId
            var userReviews = allReviews.Where(r => r.UserId == userId).ToList();

            if (userReviews == null || !userReviews.Any())
            {
                throw new Exception($"No reviews found for user with ID {userId}");
            }

            return userReviews;
        }


        public async Task<List<Review>> GetReviewsByHotelOwnerId(int ownerId)
        {
            // Fetch all hotels
            var hotels = await _hrepository.GetAll();

            if (hotels == null || !hotels.Any())
            {
                throw new Exception("No hotels found.");
            }

            // Filter hotels by OwnerId
            var hotelsByOwnerId = hotels.Where(h => h.OwnerId == ownerId).ToList();

            if (hotelsByOwnerId == null || !hotelsByOwnerId.Any())
            {
                throw new Exception($"No hotels found for owner with ID {ownerId}");
            }

            // Fetch all reviews
            var allReviews = await _repository.GetAll();

            if (allReviews == null || !allReviews.Any())
            {
                throw new Exception("No reviews found.");
            }

            // Filter reviews by hotel IDs that belong to the specified owner ID
            var reviews = allReviews.Where(r => hotelsByOwnerId.Any(h => h.HotelId == r.HotelId)).ToList();

            return reviews;
        }
    }
}

