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

            if (await _roleRepository.ExistsRoleByTypeAsync(type))
                throw new InvalidOperationException($"Un rôle avec le type '{type}' existe déjà.");

            var role = new Role { Type = type };
            await _roleRepository.AddRoleAsync(role);
            await _roleRepository.SaveChangesAsync();

            return role;
        }

        public async Task<Role?> GetRoleByTypeAsync(string type)
        {
            return await _roleRepository.GetByTypeAsync(type);
        }

        public async Task<Role?> GetRoleByIdAsync(int id)
        {
            return await _roleRepository.GetRoleByIdAsync(id);
        }

        public async Task<IEnumerable<Role>> GetAllRolesAsync()
        {
            return await _roleRepository.GetAllRoleAsync();
        }

        public async Task<IEnumerable<Role>> SearchRolesByTypeAsync(string searchPattern)
        {
            return await _roleRepository.SearchByTypeAsync(searchPattern);
        }

        public async Task<bool> RoleExistsAsync(string type)
        {
            return await _roleRepository.ExistsRoleByTypeAsync(type);
        }

        public async Task<Role?> UpdateRoleAsync(int id, string newType)
        {
            if (string.IsNullOrWhiteSpace(newType))
                throw new ArgumentException("Le type ne peut pas être vide.", nameof(newType));

            var existingRole = await _roleRepository.GetRoleByIdAsync(id);
            if (existingRole == null)
                return null;

            // Vérifier si le nouveau type existe déjà pour un autre rôle
            var roleWithSameType = await _roleRepository.GetByTypeAsync(newType);
            if (roleWithSameType != null && roleWithSameType.Id != id)
                throw new InvalidOperationException($"Un rôle avec le type '{newType}' existe déjà.");

            existingRole.Type = newType;
            var updatedRole = await _roleRepository.UpdateRoleAsync(existingRole);
            if (updatedRole != null)
                await _roleRepository.SaveChangesAsync();

            return updatedRole;
        }

        public async Task<bool> DeleteRoleAsync(int id)
        {
            var success = await _roleRepository.DeleteRoleAsync(id);
            if (success)
                await _roleRepository.SaveChangesAsync();
            return success;
        }
    }
}
