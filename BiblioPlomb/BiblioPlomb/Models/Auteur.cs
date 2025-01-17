using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Biblioplomb_.Models
{
    public class Auteur
    {
        public int Id { get; set; }
        public string Nom { get; set; } = string.Empty;
        public bool Prenom { get; set; }

        // Clé étrangère vers Livre
        public int LivreId { get; set; }

        // Propriété de navigation vers livre
        public Livre Livre { get; set; }
        //public ICollection<Auteur_Livres> AuteurLivres { get; set; } = new List<Auteur_Livres>();

    }
}