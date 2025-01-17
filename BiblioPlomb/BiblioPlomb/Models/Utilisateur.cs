using BiblioPlomb.Models.BiblioPlomb.Models;

namespace BiblioPlomb.Models
{
    public class Utilisateur
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Email { get; set; }
        public string MotDePasse { get; set; }

        // Propriété de navigation pour la relation plusieurs-à-plusieurs
        public ICollection<UtilisateurRole> UtilisateurRoles { get; set; }
    }
}
