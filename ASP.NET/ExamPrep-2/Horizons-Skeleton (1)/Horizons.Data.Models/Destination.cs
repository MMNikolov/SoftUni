using Microsoft.AspNetCore.Identity;

namespace Horizons.Data.Models
{
    public class Destination
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string? ImageUrl { get; set; } = null!;
        public string PublisherId { get; set; } = null!;
        public IdentityUser Publisher { get; set; } = null!;
        public DateTime PublishedOn { get; set; }
        public int TerrainId { get; set; }
        public Terrain Terrain { get; set; } = null!;
        public bool IsDeleted { get; set; } = false;
        public ICollection<UserDestination> UsersDestinations { get; set; } = new HashSet<UserDestination>();

    }

}
