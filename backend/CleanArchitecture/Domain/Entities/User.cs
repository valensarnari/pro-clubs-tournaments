namespace Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string Role { get; set; } = "User";
        public DateTime DateCreated { get; set; } = DateTime.Now;

        public ICollection<Tournament> Tournaments { get; set; } = new List<Tournament>();
    }
}
