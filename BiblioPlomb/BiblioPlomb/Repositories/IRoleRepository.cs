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
        Task<bool> ExistsByTypeAsync(string type);
        Task SaveChangesAsync();
    }
}
