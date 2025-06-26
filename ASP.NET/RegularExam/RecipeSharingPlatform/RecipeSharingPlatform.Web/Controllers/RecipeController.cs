namespace RecipeSharingPlatform.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using RecipeSharingPlatform.Services.Core.Contracts;
    using RecipeSharingPlatform.ViewModels;
    using static RecipeSharingPlatform.GCommon.ValidationConstants.RecipeConstants;

    public class RecipeController : BaseController
    {
        private readonly IRecipeService _recipeService;
        private readonly ICategoryService _categoryService;

        public RecipeController(IRecipeService recipeService, ICategoryService categoryService)
        {
            _recipeService = recipeService;
            _categoryService = categoryService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            string? userId = GetUserId();

            var recipes = await _recipeService.GetAllRecipesAsync(userId);

            return View(recipes);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            string? userId = GetUserId();

            var recipeDetails = await _recipeService.GetRecipeDetailsAsync(id, userId);

            if (recipeDetails == null)
            {
                return NotFound();
            }

            return View(recipeDetails);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            RecipeAddViewModel model = new RecipeAddViewModel
            {
                CreatedOn = DateTime.UtcNow.ToString(DateFormat),
                Categories = await _categoryService.GetAllCategoriesAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(RecipeAddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = await _categoryService.GetAllCategoriesAsync();
                return View(model);
            }

            string userId = GetUserId()
                ?? throw new InvalidOperationException("User is not authenticated.");

            bool isCreated = await _recipeService.CreateRecipeAsync(model, userId);

            if (isCreated)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError(string.Empty, "Failed to create recipe. Please try again.");
            model.Categories = await _categoryService.GetAllCategoriesAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            string userId = GetUserId()
                ?? throw new InvalidOperationException("User is not authenticated.");

            var recipe = await _recipeService.GetRecipeForEditAsync(id, userId);

            if (recipe == null)
            {
                return NotFound();
            }

            recipe.Categories = await _categoryService.GetAllCategoriesAsync();

            return View(recipe);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RecipeEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = await _categoryService.GetAllCategoriesAsync();
                return View(model);
            }

            string userId = GetUserId()
                ?? throw new InvalidOperationException("User is not authenticated.");

            bool isEdited = await _recipeService.EditRecipeAsync(model, userId);

            if (isEdited)
            {
                return RedirectToAction(nameof(Details), new { id = model.Id });
            }

            ModelState.AddModelError(string.Empty, "Failed to edit recipe. Please try again.");
            model.Categories = await _categoryService.GetAllCategoriesAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Favorites()
        {
            string userId = GetUserId()
                ?? throw new InvalidOperationException("User is not authenticated.");

            var favoriteRecipes = await _recipeService.GetFavoriteRecipesAsync(userId);

            return View(favoriteRecipes);
        }

        [HttpPost]
        public async Task<IActionResult> Save(int id)
        {
            string userId = GetUserId()
                ?? throw new InvalidOperationException("User is not authenticated.");

            bool isFavorited = await _recipeService.FavoriteRecipeAsync(id, userId);

            if (isFavorited)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError(string.Empty, "Failed to favorite recipe. Please try again.");
            return RedirectToAction("Details", new { id });
        }

        [HttpPost]
        public async Task<IActionResult> Remove(int id)
        {
            string userId = GetUserId()
                ?? throw new InvalidOperationException("User is not authenticated.");

            bool isUnfavorited = await _recipeService.UnfavoriteRecipeAsync(id, userId);

            if (isUnfavorited)
            {
                return RedirectToAction("Favorites");
            }

            ModelState.AddModelError(string.Empty, "Failed to unfavorite recipe. Please try again.");
            return RedirectToAction("Details", new { id });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            string userId = GetUserId()
                ?? throw new InvalidOperationException("User is not authenticated.");

            var recipe = await _recipeService.SoftDeleteAsync(id, userId);

            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmDelete(RecipeDeleteViewModel model)
        {
            string userId = GetUserId()
                ?? throw new InvalidOperationException("User is not authenticated.");

            bool isDeleted = await _recipeService.HardDeleteAsync(model.Id, userId);

            if (isDeleted)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError(string.Empty, "Failed to delete recipe. Please try again.");
            return View(model);
        }
    }
}
