namespace RecipeSharingPlatform.Services.Core.Contracts
{
    using RecipeSharingPlatform.ViewModels;
    public interface IRecipeService
    {
        Task<IEnumerable<RecipeIndexViewModel>> GetAllRecipesAsync(string? userId);
        Task<RecipeDetailsViewModel?> GetRecipeDetailsAsync(int recipeId, string? userId);
        Task<bool> CreateRecipeAsync(RecipeAddViewModel model, string userId);
        Task<RecipeEditViewModel?> GetRecipeForEditAsync(int recipeId, string userId);
        Task<bool> EditRecipeAsync(RecipeEditViewModel model, string userId);
        Task<IEnumerable<RecipeFavoriteViewModel>> GetFavoriteRecipesAsync(string userId);
        Task<bool> FavoriteRecipeAsync(int recipeId, string userId);
        Task<bool> UnfavoriteRecipeAsync(int recipeId, string userId);
        Task<RecipeDeleteViewModel?> SoftDeleteAsync(int recipeId, string userId);
        Task<bool> HardDeleteAsync(int recipeId, string userId);

    }
}
