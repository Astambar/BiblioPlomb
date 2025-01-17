using System.ComponentModel.DataAnnotations.Schema;

namespace BiblioPlomb.Models
{
    public class AuteursLivres
    {
        [ForeignKey("Auteur - Livre")]
        public int AuteurId {  get; set; }
        public string LivreId { get; set;} 

    }
}
