using BiblioPlomb.Models;

namespace BiblioPlomb.Models
{
    public class Auteur
    {
        public int Id { get; set; }
        public string Nom { get; set; } = string.Empty;

        // Ajoute la collection des livres de cet auteur
        public ICollection<AuteurLivre> AuteurLivres { get; set; } = new List<AuteurLivre>();
    }
}
