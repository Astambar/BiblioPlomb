namespace BiblioPlomb.Models
{
    public class UtilisateurRole
    {
        public int UtilisateurId { get; set; }
        public Utilisateur Utilisateur { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }
    }

}