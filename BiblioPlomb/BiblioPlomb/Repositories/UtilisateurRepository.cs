using BiblioPlomb.Data;
using BiblioPlomb.Models;
using Microsoft.EntityFrameworkCore;

namespace BiblioPlomb.Repositories
{
    public class UtilisateurRepository : IUtilisateurRepository
    {
        private readonly BiblioPlombContext _context;

        public UtilisateurRepository(BiblioPlombContext context)
        {
            _context = context;
        }

        public async Task<Utilisateur?> GetByIdAsync(int id)
        {
            return await _context.Utilisateurs
                .Include(utilisateur => utilisateur.UtilisateurRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(utilisateur => utilisateur.Id == id);
        }

        public async Task<IEnumerable<Utilisateur>> GetAllAsync()
        {
            return await _context.Utilisateurs
                .Include(utilisateur => utilisateur.UtilisateurRoles)
                .ThenInclude(ur => ur.Role)
                .ToListAsync();
        }

        public async Task<IEnumerable<Utilisateur>> SearchAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return await GetAllAsync();

            searchTerm = searchTerm.ToLower();

            return await _context.UtilisateurRoles
                .Include(ur => ur.Utilisateur)
                .Include(ur => ur.Role)
                .Where(ur => ur.Utilisateur.Nom.ToLower().Contains(searchTerm) ||
                             ur.Utilisateur.Prenom.ToLower().Contains(searchTerm) ||
                             ur.Utilisateur.Email.ToLower().Contains(searchTerm) ||
                             ur.Role.Type.ToLower().Contains(searchTerm))
                .Select(ur => ur.Utilisateur)
                .Distinct()
                .ToListAsync();
        }

        public async Task<Utilisateur> AddAsync(Utilisateur utilisateur)
        {
            await _context.Utilisateurs.AddAsync(utilisateur);
            return utilisateur;
        }

        public async Task<Utilisateur?> UpdateUtilisateurAsync(Utilisateur utilisateur)
        {
            var existingUser = await GetByIdAsync(utilisateur.Id);
            if (existingUser == null) return null;

            _context.Entry(existingUser).CurrentValues.SetValues(utilisateur);
            existingUser = utilisateur;
            
            return existingUser;
        }

        public async Task<bool> DeleteUtilisateurAsync(int id)
        {
            var utilisateur = await _context.Utilisateurs.FindAsync(id);
            var utilisateurRoles = await _context.UtilisateurRoles.Where(ur => ur.UtilisateurId == id).ToListAsync();
            if (utilisateur == null) return false;
            if (utilisateurRoles.Count() >= 1)
            {
                _context.UtilisateurRoles.RemoveRange(utilisateurRoles);
            }
            _context.Utilisateurs.Remove(utilisateur);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<UtilisateurRole>> GetUtilisateurRolesByUtilisateurIdAsync(int utilisateurId)
        {
            return await _context.UtilisateurRoles
                .Where(ur => ur.UtilisateurId == utilisateurId)
                .Include(ur => ur.Role)
                .ToListAsync();
        }

        public async Task DeleteUtilisateurRoleAsync(int utilisateurId, int roleId)
        {
            var utilisateurRole = await _context.UtilisateurRoles.FirstOrDefaultAsync(ur => ur.UtilisateurId == utilisateurId && ur.RoleId == roleId);
            if (utilisateurRole != null)
            {
                _context.UtilisateurRoles.Remove(utilisateurRole);
            }
        }

        public async Task UpdateUtilisateurRoleAsync(UtilisateurRole utilisateurRole)
        {
             _context.UtilisateurRoles.Update(utilisateurRole);
        }

        public async Task AddUtilisateurRoleAsync(UtilisateurRole utilisateurRole)
        {
            await _context.UtilisateurRoles.AddAsync(utilisateurRole);
        }

        public async Task<Utilisateur> CreateUtilisateurAsync(string nom, string prenom, string email, string motDePasse, int roleId)
        {
            var utilisateur = new Utilisateur
            {
                Nom = nom,
                Prenom = prenom,
                Email = email,
                MotDePasse = motDePasse
            };

            await _context.Utilisateurs.AddAsync(utilisateur);
            await _context.SaveChangesAsync();

            return utilisateur;
        }
    }
}
