using BiblioPlomb.Models;

namespace BiblioPlomb.DTO
{
    public class EmpruntDTO
    {
        public required int ID { get; set; }
        public required int UserID { get; set; }
        public required DateTime DateEmprunt { get; set; }
        public required DateTime DateRetour { get; set; }



        public EmpruntDTO() { }

        public EmpruntDTO(Emprunt emprunt)
        {
            ID = emprunt.ID;
            UserID = emprunt.UserID;
            DateEmprunt = emprunt.DateEmprunt;
            DateRetour = emprunt.DateRetour;
        }
    }
}