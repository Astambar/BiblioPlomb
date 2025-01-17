using BiblioPlomb.Models;
namespace BiblioPlomb.DTO
{
    public class UtilisateurDTO
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Email { get; set; }
        public string MotDePasse { get; set; }
        public ICollection<UtilisateurRole> Roles { get; set; }
    }
}
