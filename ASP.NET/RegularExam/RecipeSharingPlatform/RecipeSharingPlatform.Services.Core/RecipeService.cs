using Microsoft.EntityFrameworkCore;
using RecipeSharingPlatform.Data;
using RecipeSharingPlatform.Data.Models;
using RecipeSharingPlatform.Services.Core.Contracts;
using RecipeSharingPlatform.ViewModels;
using static RecipeSharingPlatform.GCommon.ValidationConstants.RecipeConstants;

namespace RecipeSharingPlatform.Services.Core
{
    public class RecipeService : IRecipeService
    {
        private readonly ApplicationDbContext _context;

        public RecipeService(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<RecipeIndexViewModel>> GetAllRecipesAsync(string? userId)
        {
            var recipes = await _context.Recipes
                .Where(r => r.IsDeleted == false)
                .Include(r => r.Category)
                .Include(r => r.UsersRecipes)
                .AsNoTracking()
                .Select(r => new RecipeIndexViewModel
                {
                    Id = r.Id,
                    Title = r.Title,
                    ImageUrl = r.ImageUrl,
                    CategoryName = r.Category.Name,
                    SavedCount = r.UsersRecipes.Count(),
                    IsAuthor = r.AuthorId == userId,
                    IsSaved = r.UsersRecipes.Any(ur => ur.UserId == userId)
                })
                .ToListAsync();

            return recipes;
        }

        public Task<RecipeDetailsViewModel?> GetRecipeDetailsAsync(int recipeId, string? userId)
        {
            return _context.Recipes
                .Where(r => r.Id == recipeId && r.IsDeleted == false)
                .Include(r => r.Category)
                .Include(r => r.Author)
                .Include(r => r.UsersRecipes)
                .AsNoTracking()
                .Select(r => new RecipeDetailsViewModel
                {
                    Id = r.Id,
                    Title = r.Title,
                    Instructions = r.Instructions,
                    ImageUrl = r.ImageUrl,
                    CategoryName = r.Category.Name,
                    AuthorName = r.Author.UserName ?? "Unknown User",
                    CreatedOn = r.CreatedOn.ToString("MMMM dd, yyyy"),
                    IsAuthor = r.AuthorId == userId,
                    IsSaved = r.UsersRecipes.Any(ur => ur.UserId == userId)
                })
                .FirstOrDefaultAsync();
        }

        public async Task<bool> CreateRecipeAsync(RecipeAddViewModel model, string userId)
        {
            if (model == null || string.IsNullOrWhiteSpace(model.Title) || string.IsNullOrWhiteSpace(model.Instructions))
            {
                return false;
            }

            var recipe = new Recipe
            {
                Title = model.Title,
                Instructions = model.Instructions,
                ImageUrl = model.ImageUrl,
                CategoryId = model.CategoryId,
                AuthorId = userId,
                CreatedOn = DateTime.UtcNow
            };

            await _context.Recipes.AddAsync(recipe);
            await _context.SaveChangesAsync();

            return true;


        }

        public async Task<RecipeEditViewModel?> GetRecipeForEditAsync(int recipeId, string userId)
        {
            var recipe = await _context.Recipes
                .Where(r => r.Id == recipeId && r.AuthorId == userId && r.IsDeleted == false)
                .Select(r => new RecipeEditViewModel
                {
                    Id = r.Id,
                    Title = r.Title,
                    Instructions = r.Instructions,
                    ImageUrl = r.ImageUrl,
                    CategoryId = r.CategoryId,
                    CreatedOn = r.CreatedOn,
                })
                .FirstOrDefaultAsync();

            if (recipe == null)
            {
                return null;
            }

            recipe.Categories = await _context.Categories
                .AsNoTracking()
                .Select(c => new CategoryDropDownViewModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();

            return recipe;
        }

        public async Task<bool> EditRecipeAsync(RecipeEditViewModel model, string userId)
        {
            if (model == null || string.IsNullOrWhiteSpace(model.Title) || string.IsNullOrWhiteSpace(model.Instructions))
            {
                return false;
            }

            var recipe = await _context.Recipes
                .FirstOrDefaultAsync(r => r.Id == model.Id && r.AuthorId == userId && r.IsDeleted == false);

            if (recipe == null)
            {
                return false;
            }

            recipe.Title = model.Title;
            recipe.Instructions = model.Instructions;
            recipe.ImageUrl = model.ImageUrl;
            recipe.CategoryId = model.CategoryId;

            _context.Recipes.Update(recipe);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<RecipeFavoriteViewModel>> GetFavoriteRecipesAsync(string userId)
        {
            return await _context.UsersRecipes
                .Where(ur => ur.UserId == userId)
                .Include(ur => ur.Recipe)
                .ThenInclude(r => r.Category)
                .Select(ur => new RecipeFavoriteViewModel
                {
                    Id = ur.Recipe.Id,
                    Title = ur.Recipe.Title,
                    ImageUrl = ur.Recipe.ImageUrl,
                    CategoryName = ur.Recipe.Category.Name,
                })
                .ToListAsync();
        }

        public async Task<bool> FavoriteRecipeAsync(int recipeId, string userId)
        {
            var recipe = await _context.Recipes
                .FirstOrDefaultAsync(r => r.Id == recipeId && r.IsDeleted == false);

            if (recipe == null)
            {
                return false;
            }

            var userRecipe = await _context.UsersRecipes
                .FirstOrDefaultAsync(ur => ur.RecipeId == recipeId && ur.UserId == userId);

            if (userRecipe != null)
            {
                _context.UsersRecipes.Remove(userRecipe);
            }
            else
            {
                userRecipe = new UserRecipe
                {
                    RecipeId = recipeId,
                    UserId = userId
                };
                await _context.UsersRecipes.AddAsync(userRecipe);
            }

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UnfavoriteRecipeAsync(int recipeId, string userId)
        {
            var userRecipe = await _context.UsersRecipes
                .FirstOrDefaultAsync(ur => ur.RecipeId == recipeId && ur.UserId == userId);

            if (userRecipe == null)
            {
                return false;
            }

            _context.UsersRecipes.Remove(userRecipe);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<RecipeDeleteViewModel?> SoftDeleteAsync(int recipeId, string userId)
        {
            var recipe = await _context.Recipes
                .Where(r => r.Id == recipeId && r.AuthorId == userId && r.IsDeleted == false)
                .Include(r => r.Author)
                .AsNoTracking()
                .Select(r => new RecipeDeleteViewModel
                {
                    Id = r.Id,
                    Title = r.Title,
                    AuthorId = r.AuthorId,
                    Author = r.Author.UserName ?? "Unknown User",
                })
                .FirstOrDefaultAsync();

            if (recipe == null)
            {
                return null;
            }
            return recipe;
        }

        public async Task<bool> HardDeleteAsync(int recipeId, string userId)
        {
            var recipe = await _context.Recipes
                .FirstOrDefaultAsync(r => r.Id == recipeId && r.AuthorId == userId && r.IsDeleted == false);

            if (recipe == null)
            {
                return false;
            }

            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
