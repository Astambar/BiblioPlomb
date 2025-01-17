using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Biblioplomb_.Models
{
    public class Auteur
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nom { get; set; } = string.Empty;
        public bool Prenom { get; set; }

        // Clé étrangère vers Livre
        [ForeignKey("Livre")]
        public int LivreId { get; set; }

        // Propriété de navigation vers livre
        public Livre Livre { get; set; }
        public ICollection<AuteursLivres> AuteurLivres { get; set; } = new List<Auteur_Livres>();

    }
}
