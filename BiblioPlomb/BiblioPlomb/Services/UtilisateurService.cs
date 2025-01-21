using BiblioPlomb.Models;
using BiblioPlomb.Repositories;

namespace BiblioPlomb.Services
{
    public class UtilisateurService : IUtilisateurService
    {
        private readonly IUtilisateurRepository _utilisateurRepository;
        private readonly IRoleRepository _roleRepository;

        public UtilisateurService(IUtilisateurRepository utilisateurRepository, IRoleRepository roleRepository)
        {
            _utilisateurRepository = utilisateurRepository;
            _roleRepository = roleRepository;
        }

        public async Task<Utilisateur> CreateUtilisateurAsync(string nom, string prenom, string email, string motDePasse, int[] selectedRoles)
        {
            if (string.IsNullOrWhiteSpace(nom))
                throw new ArgumentException("Le nom ne peut pas être vide.", nameof(nom));
            if (string.IsNullOrWhiteSpace(prenom))
                throw new ArgumentException("Le prénom ne peut pas être vide.", nameof(prenom));
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("L'email ne peut pas être vide.", nameof(email));
            if (string.IsNullOrWhiteSpace(motDePasse))
                throw new ArgumentException("Le mot de passe ne peut pas être vide.", nameof(motDePasse));

            var utilisateur = new Utilisateur
            {
                Nom = nom,
                Prenom = prenom,
                Email = email,
                MotDePasse = motDePasse
            };

            await _utilisateurRepository.AddAsync(utilisateur);
            await _utilisateurRepository.SaveChangesAsync();

            // Associer les rôles sélectionnés à l'utilisateur
            foreach (var roleId in selectedRoles)
            {
                var role = await _roleRepository.GetRoleByIdAsync(roleId);
                if (role == null)
                    throw new InvalidOperationException($"Le rôle avec l'ID {roleId} n'existe pas.");

                var utilisateurRole = new UtilisateurRole { UtilisateurId = utilisateur.Id, RoleId = roleId };
                await _utilisateurRepository.AddUtilisateurRoleAsync(utilisateurRole);
            }

            await _utilisateurRepository.SaveChangesAsync();

            return utilisateur;
        }

        public async Task<Utilisateur?> GetUtilisateurByIdAsync(int id)
        {
            return await _utilisateurRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Utilisateur>> GetAllUtilisateursAsync()
        {
            return await _utilisateurRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Utilisateur>> SearchUtilisateursAsync(string searchTerm)
        {
            return await _utilisateurRepository.SearchAsync(searchTerm);
        }

        public async Task<Utilisateur?> UpdateUtilisateurAsync(int id, string nom, string prenom, string email, string motdepasse, int[] selectedRoles)
        {
            var utilisateur = await _utilisateurRepository.GetByIdAsync(id);
            if (utilisateur == null)
                throw new InvalidOperationException("L'utilisateur n'existe pas.");

            utilisateur.Nom = nom;
            utilisateur.Prenom = prenom;
            utilisateur.Email = email;
            utilisateur.MotDePasse = motdepasse;

            await _utilisateurRepository.UpdateUtilisateurAsync(utilisateur);
            await _utilisateurRepository.SaveChangesAsync();

            // Gérer les rôles
            var utilisateurRoles = await _utilisateurRepository.GetUtilisateurRolesByUtilisateurIdAsync(id);
            var currentRoleIds = utilisateurRoles.Select(ur => ur.RoleId).ToList();

            // Supprimer les rôles décochés
            foreach (var roleId in currentRoleIds)
            {
                if (!selectedRoles.Contains(roleId))
                {
                    await _utilisateurRepository.DeleteUtilisateurRoleAsync(id, roleId);
                }
            }

            // Ajouter les nouveaux rôles
            foreach (var roleId in selectedRoles)
            {
                if (!currentRoleIds.Contains(roleId))
                {
                    var utilisateurRole = new UtilisateurRole { UtilisateurId = id, RoleId = roleId };
                    await _utilisateurRepository.AddUtilisateurRoleAsync(utilisateurRole);
                }
            }

            await _utilisateurRepository.SaveChangesAsync();

            return utilisateur;
        }

        public async Task<bool> DeleteUtilisateurAsync(int id)
        {
            var success = await _utilisateurRepository.DeleteUtilisateurAsync(id);
            if (success)
            {
                // Supprimer les rôles associés de la table d'association
                var utilisateurRoles = await _utilisateurRepository.GetUtilisateurRolesByUtilisateurIdAsync(id);
                foreach (var utilisateurRole in utilisateurRoles)
                {
                    Console.WriteLine("Début de la suppression du rôle avec ID : " + utilisateurRole.RoleId);
                    try
                    {
                        await _utilisateurRepository.DeleteUtilisateurRoleAsync(id, utilisateurRole.RoleId);
                        Console.WriteLine("Rôle supprimé avec succès.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Erreur lors de la suppression du rôle : " + ex.Message);
                    }
                }
                await _utilisateurRepository.SaveChangesAsync();
            }
            return success;
        }

        public async Task<IEnumerable<Role>> GetRolesByUtilisateurIdAsync(int utilisateurId)
        {
            var utilisateurRoles = await _utilisateurRepository.GetUtilisateurRolesByUtilisateurIdAsync(utilisateurId);
            var roles = new List<Role>();
            foreach (var utilisateurRole in utilisateurRoles)
            {
                Console.WriteLine("Récupération du rôle avec ID : " + utilisateurRole.RoleId);
                try
                {
                    roles.Add(await _roleRepository.GetRoleByIdAsync(utilisateurRole.RoleId));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erreur lors de la récupération du rôle : " + ex.Message);
                }
            }
            return roles;
        }

        public async Task<IEnumerable<UtilisateurRole>> GetUtilisateurRolesByUtilisateurIdAsync(int utilisateurId)
        {
            return await _utilisateurRepository.GetUtilisateurRolesByUtilisateurIdAsync(utilisateurId);
        }
    }
}
