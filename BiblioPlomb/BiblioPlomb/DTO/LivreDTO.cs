using BiblioPlomb.Models;

namespace BiblioPlomb.DTO
{
    public class LivreDTO
    {
        
        public int Id { get; set; }
        public string Titre { get; set; } = string.Empty;
        public bool Dispo { get; set; } 
        public EtatLivre Etat { get; set; }
        public long ISBN { get; set; }
        public int GenreId { get; set; }
        public int AuteurId { get; set; }
    }
}
