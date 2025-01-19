using BiblioPlomb.Models;

namespace Biblioplomb.Models
{
    public class Genre
    {
        public int Id { get; set; }

        public string Nom { get; set; } = string.Empty;

        // gestion  pour les livres
        public ICollection<Livre> Livres { get; set; } = new List<Livre>();        
    }
}
