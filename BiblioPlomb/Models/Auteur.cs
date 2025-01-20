using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Collections.Generic;
using BiblioPlomb.Models;

namespace BiblioPlomb.Models
{
    public class Auteur
    {
        public int Id { get; set; }
        public string Nom { get; set; } = string.Empty;
        public string Prenom { get; set; } = string.Empty;

        // jointure AuteurLivre
        public ICollection<AuteurLivre> AuteurLivres { get; set; } = new List<AuteurLivre>();
    }
}