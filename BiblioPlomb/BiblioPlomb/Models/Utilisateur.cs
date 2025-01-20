using BiblioPlomb.Models;

namespace BiblioPlomb.Models
{
    public class Utilisateur
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Email { get; set; }
        public string MotDePasse { get; set; }
        public ICollection<UtilisateurRole> UtilisateurRoles { get; set; }
    }
}
