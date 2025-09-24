using Microsoft.EntityFrameworkCore;
using MoviesAPI.Domain.Entities;
using MoviesAPI.Domain.Repositories;
using MoviesAPI.Infrastructure.Data;

namespace MoviesAPI.Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _context;

        public RoleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Role>> GetRoles()
        {
            var roles = await _context.Roles.ToListAsync();
            return roles;
        }

        public async Task<Role?> GetRoleById(int id)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == id);
            return role;
        }
    }
}
