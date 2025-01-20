using BiblioPlomb.Models;

namespace BiblioPlomb.Repositories
{
    public interface IRoleRepository
    {
        Task<Role?> GetByIdAsync(int id);
        Task<Role?> GetByTypeAsync(string type);
        Task<IEnumerable<Role>> GetAllAsync();
        Task<IEnumerable<Role>> SearchByTypeAsync(string searchPattern);
        Task<Role> AddAsync(Role role);
        Task<Role?> UpdateAsync(Role role);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsByTypeAsync(string type);
        Task SaveChangesAsync();
    }
}
