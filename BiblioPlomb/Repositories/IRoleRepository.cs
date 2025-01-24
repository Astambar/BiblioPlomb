using BiblioPlomb.Models;

namespace BiblioPlomb.Repositories
{
    public interface IRoleRepository
    {
        Task<Role?> GetRoleByIdAsync(int id);
        Task<Role?> GetByTypeAsync(string type);
        Task<IEnumerable<Role>> GetAllRoleAsync();
        Task<IEnumerable<Role>> SearchByTypeAsync(string searchPattern);
        Task<Role> AddRoleAsync(Role role);
        Task<Role?> UpdateRoleAsync(Role role);
        Task<bool> DeleteRoleAsync(int id);
        Task<bool> ExistsRoleByTypeAsync(string type);
        Task SaveChangesAsync();
        Task<IEnumerable<Role>> GetRolesByUtilisateurIdAsync(int utilisateurId);
    }
}
