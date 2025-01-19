using BiblioPlomb.Models;

namespace Biblioplomb.Models

{
    public enum EtatLivre
    { 
        Bon,
        Moyen,
        Dégradé
    }
    public class Livre
    {
        public int Id { get; set; }
        public string Titre { get; set; } = string.Empty;
        public string Auteur {  get; set; } = string.Empty;
        public bool Dispo { get; set; }
        public EtatLivre Etat { get; set; } = EtatLivre.Bon; //par défaut quand on crée un livre
        public long ISBN { get; set; }

        // clé étrangère pour Genre
        public int GenreId { get; set; }
        public int AuteurId { get; set; }

        // chercher vers Genre
        public Genre Genre { get; set; }

        // cherche vers Auteur avec jointure AuteurLivre
        public ICollection<AuteurLivre> AuteurLivres { get; set; } = new List<AuteurLivre>();
        //public ICollection<LivreGenre> LivreGenres { get; set; }
    }
}
