using BiblioPlomb.Data;
using BiblioPlomb.Models;
using Microsoft.EntityFrameworkCore;

namespace BiblioPlomb.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly BiblioPlombDB _context;

        public RoleRepository(BiblioPlombDB context)
        {
            _context = context;
        }

        public async Task<Role?> GetRoleByIdAsync(int id)
        {
            return await _context.Roles.FindAsync(id);
        }

        public async Task<Role?> GetByTypeAsync(string type)
        {
            return await _context.Roles
                .FirstOrDefaultAsync(r => r.Type.ToLower() == type.ToLower());
        }

        public async Task<IEnumerable<Role>> GetAllRoleAsync()
        {
            return await _context.Roles.ToListAsync();
        }


        public async Task<IEnumerable<Role>> SearchByTypeAsync(string searchPattern)
        {
            if (string.IsNullOrWhiteSpace(searchPattern))
                return await GetAllRoleAsync(); // Retourne tous les rôles si le modèle de recherche est vide

            searchPattern = searchPattern.ToLower();

            // Recherche les rôles dont le type contient le modèle de recherche (insensible à la casse)
            return await _context.Roles
                .Where(r => r.Type.ToLower().Contains(searchPattern))
                .ToListAsync();
        }

        public async Task<Role> AddRoleAsync(Role role)
        {
            await _context.Roles.AddAsync(role);
            return role;
        }

        public async Task<Role?> UpdateRoleAsync(Role role)
        {
            var existingRole = await GetRoleByIdAsync(role.Id);
            if (existingRole == null) return null;

            existingRole.Type = role.Type;
            _context.Roles.Update(existingRole);
            return existingRole;
        }

        public async Task<bool> DeleteRoleAsync(int id)
        {
            var role = await _context.Roles.FindAsync(id);
            var utilisateurRoles = await _context.UtilisateurRoles.Where(ur => ur.RoleId == id).ToListAsync();
            if (role == null) return false;
            if (utilisateurRoles.Count() >= 1)
            {
                _context.UtilisateurRoles.RemoveRange(utilisateurRoles);
            }
            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsRoleByTypeAsync(string type)
        {
            return await _context.Roles
                .AnyAsync(r => r.Type.ToLower() == type.ToLower());
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Role>> GetRolesByUtilisateurIdAsync(int utilisateurId)
        {
            return await _context.UtilisateurRoles
                .Include(ur => ur.Role)
                .Where(ur => ur.UtilisateurId == utilisateurId)
                .Select(ur => ur.Role)
                .ToListAsync();
        }
    }
}
