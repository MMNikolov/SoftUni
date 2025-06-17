using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Identity;

namespace GameZone.Data.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!; 
        public string Description { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public string PublisherId { get; set; } = null!;
        public IdentityUser Publisher { get; set; } = null!;
        public DateTime ReleasedOn { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; } = null!;
        public ICollection<GamerGame> GamersGames { get; set; } = new HashSet< GamerGame>();

    }
}
