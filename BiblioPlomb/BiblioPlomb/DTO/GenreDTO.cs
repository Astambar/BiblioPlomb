namespace BiblioPlomb.DTO
{
    public class GenreDTO
    {
        public int Id { get; set; }
        public string Nom { get; set; }

        //Clé étrangère
        public int LivreId { get; set; }
    }
}
