using BiblioPlomb.Models;

namespace BiblioPlomb.Services
{
    public interface IServiceUtilisateur
    {
        Task<Utilisateur> CreateUtilisateurAsync(string nom, string prenom, string email, string motDePasse, int[] selectedRoles);
        Task<Utilisateur?> GetUtilisateurByIdAsync(int id);
        Task<IEnumerable<Utilisateur>> GetAllUtilisateursAsync();
        Task<IEnumerable<Utilisateur>> SearchUtilisateursAsync(string searchTerm);
        Task<Utilisateur?> UpdateUtilisateurAsync(int id, string nom, string prenom, string email, string motdepasse, int[] selectedRoles);
        Task<bool> DeleteUtilisateurAsync(int id);
        Task<IEnumerable<Role>> GetRolesByUtilisateurIdAsync(int utilisateurId);
        Task<IEnumerable<UtilisateurRole>> GetUtilisateurRolesByUtilisateurIdAsync(int utilisateurId);
    }
}