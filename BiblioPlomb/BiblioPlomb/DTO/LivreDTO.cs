using Biblioplomb.Models;

namespace Biblioplomb.DTO
{
    public class LivreDTO
    {
        public int Id { get; set; }
        public string Titre { get; set; } = string.Empty;
        public bool Dispo { get; set; }
        public EtatLivre Etat { get; set; }

        //Clé étrangère
        public int GenreId { get; set; } 
        public int AuteurId { get; set; }
    }
}