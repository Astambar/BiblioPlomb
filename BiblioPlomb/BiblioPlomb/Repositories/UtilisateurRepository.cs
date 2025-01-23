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

        public async Task<Utilisateur> AddRoleAsync(Utilisateur utilisateur)
        {
            if (await _context.Utilisateurs.AnyAsync(u => u.Email == utilisateur.Email))
            {
                throw new Exception("Email déjà utilisé.");
            }
            await _context.Utilisateurs.AddAsync(utilisateur);
            return utilisateur;
        }

        public async Task<Utilisateur?> UpdateUtilisateurAsync(Utilisateur utilisateur)
        {
            var existingUser = await _context.Utilisateurs.FindAsync(utilisateur.Id);
            if (existingUser == null) return null;

            existingUser.Nom = utilisateur.Nom;
            existingUser.Prenom = utilisateur.Prenom;
            existingUser.Email = utilisateur.Email;
            existingUser.MotDePasse = utilisateur.MotDePasse; // Assurez-vous de gérer le hachage

            if (await _context.Utilisateurs.AnyAsync(u => u.Email == utilisateur.Email && u.Id != utilisateur.Id))
            {
                throw new Exception("Email déjà utilisé par un autre utilisateur.");
            }
            _context.Utilisateurs.Update(existingUser);

            await _context.SaveChangesAsync();

            return existingUser;
        }

        public async Task<Utilisateur?> UpdateUtilisateurAndRolesAsync(Utilisateur utilisateur, int[] selectedRoles)
        {
            // Récupérer l'utilisateur existant avec ses rôles
            var existingUser = await GetByIdAsync(utilisateur.Id);
            if (existingUser == null)
            {
                return null;
            }

            // Mettre à jour les propriétés de l'utilisateur
            _context.Entry(existingUser).CurrentValues.SetValues(utilisateur);

            // Mettre à jour les rôles associés à l'utilisateur
            var currentRoles = await GetUtilisateurRolesByUtilisateurIdAsync(utilisateur.Id);
            var currentRoleIds = currentRoles.Select(ur => ur.RoleId).ToList();

            // Supprimer les rôles décochés
            foreach (var roleId in currentRoleIds)
            {
                if (!selectedRoles.Contains(roleId))
                {
                    await DeleteUtilisateurRoleAsync(utilisateur.Id, roleId);
                }
            }

            // Ajouter les nouveaux rôles
            foreach (var roleId in selectedRoles)
            {
                if (!currentRoleIds.Contains(roleId))
                {
                    var utilisateurRole = new UtilisateurRole { UtilisateurId = utilisateur.Id, RoleId = roleId };
                    await AddUtilisateurRoleAsync(utilisateurRole);
                }
            }

            // Sauvegarder les changements
            await _context.SaveChangesAsync();

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
            await _context.SaveChangesAsync();
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
        public async Task<bool> ExistsUtilisateurByEmailAsync(string email) => await _context.Utilisateurs
                .AnyAsync(u => u.Email == email);
    }
}
