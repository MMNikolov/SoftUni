namespace Horizons.Web.ViewModels.Destination
{
    public class DestinationIndexViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public string TerrainName { get; set; } = null!;
        public long FavoritesCount { get; set; }
        public bool IsUserPublisher { get; set; }
        public bool IsInUserFavorites { get; set; }
    }
}
