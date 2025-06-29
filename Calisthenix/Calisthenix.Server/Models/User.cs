namespace Calisthenix.Server.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public byte[] PasswordHash { get; set; } = null!;
        public byte[] PasswordSalt { get; set; } = null!;
        public ICollection<Exercise> Exercises { get; set; } = new List<Exercise>();
    }
}
