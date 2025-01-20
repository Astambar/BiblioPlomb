namespace BiblioPlomb.Models
{
    public class Emprunt
    {
        public required int ID { get; set; }
        public required int UserID { get; set; }
        public required DateTime DateEmprunt { get; set; } = DateTime.Now;
        public required DateTime DateRetour { get; set; }

    }
}