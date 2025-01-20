using BiblioPlomb.Data;
using BiblioPlomb.Models;
using Microsoft.EntityFrameworkCore;

namespace BiblioPlomb.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly BiblioPlombContext _context;

        public RoleRepository(BiblioPlombContext context)
        {
            _context = context;
        }

        public async Task<Role?> GetByIdAsync(int id)
        {
            return await _context.Roles.FindAsync(id);
        }

        public async Task<Role?> GetByTypeAsync(string type)
        {
            return await _context.Roles
                .FirstOrDefaultAsync(r => r.Type.ToLower() == type.ToLower());
        }

        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<IEnumerable<Role>> SearchByTypeAsync(string searchPattern)
        {
            if (string.IsNullOrWhiteSpace(searchPattern))
                return await GetAllAsync();

            return await _context.Roles
                .Where(r => r.Type.ToLower().Contains(searchPattern.ToLower()))
                .ToListAsync();
        }

        public async Task<Role> AddAsync(Role role)
        {
            await _context.Roles.AddAsync(role);
            return role;
        }

        public async Task<Role?> UpdateAsync(Role role)
        {
            var existingRole = await GetByIdAsync(role.Id);
            if (existingRole == null) return null;

            existingRole.Type = role.Type;
            _context.Roles.Update(existingRole);
            return existingRole;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var role = await GetByIdAsync(id);
            if (role == null) return false;

            _context.Roles.Remove(role);
            return true;
        }

        public async Task<bool> ExistsByTypeAsync(string type)
        {
            return await _context.Roles
                .AnyAsync(r => r.Type.ToLower() == type.ToLower());
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
