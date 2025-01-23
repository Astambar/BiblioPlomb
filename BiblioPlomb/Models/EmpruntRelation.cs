namespace BiblioPlomb.Models
{
    public class EmpruntRelation
    {
        public int LivreId { get; set; }
        public Livre Livres { get; set; } = default!;
        public int UtilisateurId { get; set; }
        public Utilisateur Utilisateurs { get; set; } = default!;
        public int EmpruntId { get; set; } 
        public Emprunt Emprunts { get; set; }= default!;
    }
}
