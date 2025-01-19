using System.ComponentModel.DataAnnotations.Schema;
using Biblioplomb.Models;

namespace Biblioplomb.Models
{
    public class AuteurLivre
    {
        //[ForeignKey("Auteur - Livre")]
        public int AuteurId { get; set; }
        public Auteur Auteur { get; set; } = default!;

        public int LivreId { get; set; }
        public Livre Livre { get; set; } = default!;
    }
}
