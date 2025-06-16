using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horizons.Web.ViewModels.Destination
{
    public class EditDestinationInputModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string PublishedOn { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public int TerrainId { get; set; }
        public string PublisherId { get; set; } = null!;
        public IEnumerable<AddDestinationTerrainDropDownModel>? Terrains { get; set; } = null!;
    }
}
