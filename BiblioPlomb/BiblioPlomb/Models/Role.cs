namespace BiblioPlomb.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Type { get; set; } = null!;
        public ICollection<UtilisateurRole> UtilisateurRoles { get; set; }
    }
}
