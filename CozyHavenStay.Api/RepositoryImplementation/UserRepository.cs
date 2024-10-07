using System;
using System.Linq.Expressions;
using CozyHavenStay.Api.DataContext;
using CozyHavenStay.Api.RepositoryAbstractions;
using CozyHavenStay.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CozyHavenStay.Api.RepositoryImplementation
{
	public class UserRepository : IRepository<string,User>
	{
		private readonly AppDbContext _context;

		public UserRepository(AppDbContext context)
		{
			_context = context;

		}

		public async Task<User> Add(User item)
		{
			_context.Users.Add(item);
			await _context.SaveChangesAsync();
			return item;
		}

        public async Task<User> Delete(string key)
        {
			var user = await GetById(key);
			if(user != null)
			{
				_context.Users.Remove(user);
				_context.SaveChanges();
				return user;
			}

			return null;
        }

        public async Task<User> GetById(string uname)
        {
            var user = await _context.Users
                //.Include(u => u.Bookings)
                .Include(u => u.Reviews)
                .Include(u => u.Hotels)
                .FirstOrDefaultAsync(u => u.Username == uname);
            return user;
        }

        public async Task<List<User>> GetAll()
        {

            var users = await _context.Users
                //.Include(u => u.Bookings)
                .Include(u => u.Reviews)
                .Include(u => u.Hotels)
                .ToListAsync();
            return users;
        }

        public async Task<User> Update(User item)
        {

            var user = await GetById(item.Username);
            if (user != null)
            {
                _context.Entry<User>(item).State = EntityState.Modified;
                _context.SaveChanges();

                return item;
            }
            return null;
        }

        public Task<IEnumerable<User>> GetWithQuery(Expression<Func<User, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Task<User> UpdateWithQuery(int id, User entity)
        {
            throw new NotImplementedException();
        }
    }
}

