namespace BiblioPlomb.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Type { get; set; } = null!;

        // Propriété de navigation pour la relation plusieurs-à-plusieurs
        public ICollection<UtilisateurRole> UtilisateurRoles { get; set; }
    }
}
