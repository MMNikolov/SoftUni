using System.ComponentModel.DataAnnotations;
using static RecipeSharingPlatform.GCommon.ValidationConstants.RecipeConstants;

namespace RecipeSharingPlatform.ViewModels
{
    public class RecipeAddViewModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(TitleMinLength)]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        [MinLength(InstructionsMinLength)]
        [MaxLength(InstructionsMaxLength)]
        public string Instructions { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public string CreatedOn { get; set; } = null!;
        public IEnumerable<CategoryDropDownViewModel>? Categories { get; set; }
    }
}
