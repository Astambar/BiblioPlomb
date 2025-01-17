namespace Biblioplomb_.Models
{
    public class Livre
    {
        public int Id { get; set; }
        public string Titre { get; set; } = string.Empty;
        public bool Disponibilite { get; set; }
        public string Etat { get; set; }

        // Clé étrangère vers Genre
        public int GenreId { get; set; }
        // Clé étrangère vers Auteur
        public int AuteurId { get; set; }
        public int ISBN { get; set; }

        // Propriété de navigation vers Genre
        public Genre Genre { get; set; }

    }