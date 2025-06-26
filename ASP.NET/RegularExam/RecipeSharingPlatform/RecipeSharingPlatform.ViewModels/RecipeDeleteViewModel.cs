using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeSharingPlatform.ViewModels
{
    public class RecipeDeleteViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string AuthorId { get; set; } = null!;
        public string Author { get; set; } = null!;
    }
}
