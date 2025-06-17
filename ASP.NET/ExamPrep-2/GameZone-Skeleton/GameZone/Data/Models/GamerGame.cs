using Microsoft.AspNetCore.Identity;

namespace GameZone.Data.Models
{
    public class GamerGame
    {
        public int GameId { get; set; }
        public Game Game { get; set; } = null!;

        public string GamerId { get; set; } = null!;
        public IdentityUser Gamer { get; set; } = null!;
    }
}
