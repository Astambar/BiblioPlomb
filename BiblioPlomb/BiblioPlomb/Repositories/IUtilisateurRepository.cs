using BiblioPlomb.Models;

namespace BiblioPlomb.Repositories
{
    public interface IUtilisateurRepository
    {
        Task<Utilisateur?> GetByIdAsync(int id);
        Task<IEnumerable<Utilisateur>> GetAllAsync();
        Task<IEnumerable<Utilisateur>> SearchAsync(string searchTerm);
        Task<Utilisateur> AddAsync(Utilisateur utilisateur);
        Task<Utilisateur?> UpdateUtilisateurAsync(Utilisateur utilisateur);
        Task<bool> DeleteUtilisateurAsync(int id);
        Task SaveChangesAsync();
        Task<IEnumerable<UtilisateurRole>> GetUtilisateurRolesByUtilisateurIdAsync(int utilisateurId);
        Task DeleteUtilisateurRoleAsync(int utilisateurId, int roleId);
        Task UpdateUtilisateurRoleAsync(UtilisateurRole utilisateurRole);
        Task AddUtilisateurRoleAsync(UtilisateurRole utilisateurRole);
        Task<Utilisateur> CreateUtilisateurAsync(string nom, string prenom, string email, string motDePasseHash, int roleId);
    }
}
