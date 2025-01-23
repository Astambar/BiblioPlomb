namespace BiblioPlomb.Models
{
    public class Emprunt
    {
        public int Id { get; set; }
        public ICollection<EmpruntRelation> EmpruntUtilisateurs { get; set; } = new List<EmpruntRelation>();
        public ICollection<EmpruntRelation> EmpruntLivres { get; set; } = new List<EmpruntRelation>();
        public DateTime DateEmprunt { get; set; } = DateTime.Now;
        public DateTime DateRetour { get; set; }

    }
}