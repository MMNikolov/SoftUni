namespace RecipeSharingPlatform.ViewModels
{
    public class RecipeIndexViewModel
    {
        public int Id { get; set; }
        public string? ImageUrl { get; set; }
        public string Title { get; set; } = null!;
        public string CategoryName { get; set; } = null!;
        public int SavedCount { get; set; }
        public bool IsAuthor { get; set; }
        public bool IsSaved { get; set; }
    }
}
