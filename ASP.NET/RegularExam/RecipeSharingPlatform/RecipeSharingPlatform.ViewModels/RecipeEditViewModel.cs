using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeSharingPlatform.ViewModels
{
    public class RecipeEditViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Instructions { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public DateTime CreatedOn { get; set; }
        public IEnumerable<CategoryDropDownViewModel>? Categories { get; set; }
    }
}
