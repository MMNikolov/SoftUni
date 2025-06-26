namespace Horizons.Web.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    using static Horizons.GCommon.ValidationConstants.DestinationValidationConstants;
    public class DestinationAddInputModel
    {
        [Required]
        [MinLength(NameMinLength)]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MinLength(DescriptionMinLength)]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        public string? ImageUrl { get; set; } = null!;

        public int TerrainId { get; set; }
        public string PublishedOn { get; set; } = null!;
        public IEnumerable<DestinationAddTerrainDropDownModel>? Terrains { get; set; }
    }
}
