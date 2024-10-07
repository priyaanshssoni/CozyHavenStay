using System;
using CozyHavenStay.Data.DTOs;
using CozyHavenStay.Data.Models;

namespace CozyHavenStay.Api.Mapper
{
    public class ReviewMapping
    {
        public static ReviewDTO MapReviewToDTO(Review review)
        {
            return new ReviewDTO
            {
                UserId = review.UserId,
                Rating = review.Rating,
                Comment = review.Comment,
                HotelId = review.HotelId,
            };
        }

        public static Review MapReviewDTOToEntity(ReviewDTO reviewDTO)
        {
            return new Review
            {
                UserId = reviewDTO.UserId,
                Rating = reviewDTO.Rating,
                Comment = reviewDTO.Comment,
                HotelId = reviewDTO.HotelId,
                Date = DateTime.Now,

            };
        }
    }
}

