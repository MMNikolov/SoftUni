﻿namespace RecipeSharingPlatform.ViewModels
{
    public class RecipeDetailsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Instructions { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public string CategoryName { get; set; } = null!;
        public string AuthorName { get; set; } = null!;
        public string CreatedOn { get; set; } = null!;
        public bool IsAuthor { get; set; }
        public bool IsSaved { get; set; }
    }
}
