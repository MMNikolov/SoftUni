namespace Horizons.Data.Models
{
    public class Terrain    
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<Destination> Destinations { get; set; } = new HashSet<Destination>();
    }
}
