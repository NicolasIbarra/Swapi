using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.Domain.Entities;
using MoviesAPI.Domain.Repositories;
using MoviesAPI.Infrastructure.Data;

namespace MoviesAPI.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> RegisterUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User?> SearchUserByUsername(string username)
        {
            var user = await _context.Users.Where(x => x.Username == username).FirstOrDefaultAsync();
            return user;
        }

        public async Task<User?> SearchUserByUsernameAndPassword(string username, string passwordHash)
        {
            var user = await _context.Users.Where(x => x.Username == username && x.Password == passwordHash).FirstOrDefaultAsync();
            return user;
        }

        public async Task<User?> SearchUserByEmail(string email)
        {
            var user = await _context.Users.Where(x => x.Email == email).FirstOrDefaultAsync();
            return user;
        }
    }
}
