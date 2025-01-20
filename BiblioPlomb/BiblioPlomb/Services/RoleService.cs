using BiblioPlomb.Models;
using BiblioPlomb.Data;
using Microsoft.EntityFrameworkCore;
using BiblioPlomb.Repositories;

namespace BiblioPlomb.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<Role> CreateRoleAsync(string type)
        {
            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentException("Le type ne peut pas être vide.", nameof(type));

            if (await _roleRepository.ExistsByTypeAsync(type))
                throw new InvalidOperationException($"Un rôle avec le type '{type}' existe déjà.");

            var role = new Role { Type = type };
            await _roleRepository.AddAsync(role);
            await _roleRepository.SaveChangesAsync();

            return role;
        }

        public async Task<Role?> GetRoleByTypeAsync(string type)
        {
            return await _roleRepository.GetByTypeAsync(type);
        }

        public async Task<Role?> GetRoleByIdAsync(int id)
        {
            return await _roleRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Role>> GetAllRolesAsync()
        {
            return await _roleRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Role>> SearchRolesByTypeAsync(string searchPattern)
        {
            return await _roleRepository.SearchByTypeAsync(searchPattern);
        }

        public async Task<bool> RoleExistsAsync(string type)
        {
            return await _roleRepository.ExistsByTypeAsync(type);
        }
    }
}
