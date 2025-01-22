using BiblioPlomb.Models;

namespace BiblioPlomb.Services
{
    public interface IServiceRole
    {
        Task<Role> CreateRoleAsync(string type);
        Task<Role?> GetRoleByTypeAsync(string type);
        Task<Role?> GetRoleByIdAsync(int Id);
        Task<IEnumerable<Role>> GetAllRolesAsync();
        Task<IEnumerable<Role>> SearchRolesByTypeAsync(string searchPattern);
        Task<bool> RoleExistsAsync(string type);
        Task<Role?> UpdateRoleAsync(int id, string newType);
        Task<bool> DeleteRoleAsync(int id);
    }
}
