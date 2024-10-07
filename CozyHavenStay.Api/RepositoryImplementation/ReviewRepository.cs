using System;
using CozyHavenStay.Api.DataContext;
using CozyHavenStay.Api.RepositoryAbstractions;
using CozyHavenStay.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CozyHavenStay.Api.RepositoryImplementation
{
    public class ReviewRepository : IRepository<int, Review>
    {
        private readonly AppDbContext _context;

        public ReviewRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Review> Add(Review item)
        {
            _context.Reviews.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<Review> Delete(int key)
        {
            var review = await GetById(key);
            if (review != null)
            {
                _context.Reviews.Remove(review);
                _context.SaveChanges();
                return review;
            }
            else
            {
                throw new Exception($"Review with ID {key} not found.");
            }
        }

        public async Task<List<Review>> GetAll()
        {
            return await Task.FromResult(_context.Reviews.ToList());
        }

        public async Task<Review> GetById(int key)
        {
            var review = await _context.Reviews
                .FirstOrDefaultAsync(r => r.ReviewId == key);
            
            if (review != null)
            {
                return review;
            }
            else
            {
                throw new Exception($"Review with ID {key} not found.");
            }
        }


        public async Task<Review> Update(Review item)
        {
            var review = await GetById(item.ReviewId);
            if (review != null)
            {
                _context.Entry<Review>(item).State = EntityState.Modified;
                return item;
            }
            else
            {
                throw new Exception($"Review with ID {item.ReviewId} not found.");
            }
        }
    }
}

