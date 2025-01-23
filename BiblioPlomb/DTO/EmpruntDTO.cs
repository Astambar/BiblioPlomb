using BiblioPlomb.Models;

namespace BiblioPlomb.DTO
{
    public class EmpruntDTO
    {
        public int ID { get; set; }
        public ICollection<EmpruntRelation> EmpruntUtilisateurs { get; set; }
        public ICollection<EmpruntRelation> EmpruntLivres { get; set; }
        public DateTime DateEmprunt { get; set; }
        public DateTime DateRetour { get; set; }



        public EmpruntDTO() { }

        public EmpruntDTO(Emprunt emprunt)
        {
            ID = emprunt.Id;
            EmpruntUtilisateurs = emprunt.EmpruntUtilisateurs;
            EmpruntLivres = emprunt.EmpruntLivres;
            DateEmprunt = emprunt.DateEmprunt;
            DateRetour = emprunt.DateRetour;
        }
    }
}