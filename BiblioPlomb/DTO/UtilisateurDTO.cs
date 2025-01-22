using BiblioPlomb.Models;

namespace BiblioPlomb.DTO
{
    public class UtilisateurDTO
    {
        public int Id { get; set; }
        public required string Nom { get; set; }
        public required string Prenom { get; set; }
        public required string Email { get; set; }
        public string MotDePasse { get; set; }
        public required ICollection<UtilisateurRole> UtilisateurRoles { get; set; }

        public UtilisateurDTO() { }

        public UtilisateurDTO(Utilisateur utilisateur)
        {
            Id = utilisateur.Id;
            Nom = utilisateur.Nom;
            Prenom = utilisateur.Prenom;
            Email = utilisateur.Email;
            MotDePasse = utilisateur.MotDePasse;
            UtilisateurRoles = utilisateur.UtilisateurRoles;
        }
    }
}