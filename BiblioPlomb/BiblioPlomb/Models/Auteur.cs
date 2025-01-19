using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Collections.Generic;
using BiblioPlomb.Models;

namespace Biblioplomb.Models
{
    public class Auteur
    {
        public int Id { get; set; }
        public string Nom { get; set; } = string.Empty;
        public string Prenom { get; set; } = string.Empty;

        // Propriété de navigation vers Livre avec jointure AuteurLivre
        public ICollection<AuteurLivre> AuteurLivres { get; set; } = new List<AuteurLivre>();
    }
}

