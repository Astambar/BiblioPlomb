using BiblioPlomb.Models;

namespace BiblioPlomb.Repositories
{
    public interface IRoleRepository
    {
        Task<Role?> GetRoleByIdAsync(int id);
        Task<Role?> GetByTypeAsync(string type);
        Task<IEnumerable<Role>> GetAllAsync();
        Task<IEnumerable<Role>> SearchByTypeAsync(string searchPattern);
        Task<Role> AddAsync(Role role);
        Task<Role?> UpdateRoleAsync(Role role);
        Task<bool> DeleteRoleAsync(int id);
        Task<bool> ExistsByTypeAsync(string type);
        Task SaveChangesAsync();
        Task<IEnumerable<Role>> GetRolesByUtilisateurIdAsync(int utilisateurId);
    }
}
