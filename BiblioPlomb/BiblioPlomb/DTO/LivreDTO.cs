namespace Biblioplomb_.DTO
{
    public class LivreDTO
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public bool Disponibilite { get; set; }
        public string Etat { get; set; }

        //Clé étrangère
        public int GenreId { get; set; } 
    }
}