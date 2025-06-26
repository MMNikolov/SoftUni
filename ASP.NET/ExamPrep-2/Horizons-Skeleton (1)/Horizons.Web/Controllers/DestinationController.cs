namespace Horizons.Web.Controllers
{
    using System.Security.Claims;
    using Horizons.Services.Core.Contracts;
    using Horizons.Web.ViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class DestinationController : BaseController
    {
        private readonly IDestinationService _destinationService;
        private readonly ITerrainService _terrainService;

        public DestinationController(IDestinationService destinationService, ITerrainService terrainService)
        {
            _destinationService = destinationService;
            _terrainService = terrainService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            string? userId = User?.FindFirstValue(ClaimTypes.NameIdentifier);

            var destinations =
                await _destinationService.GetAllDestinationsAsync(userId);

            return View(destinations);

        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            DestinationAddInputModel model = new DestinationAddInputModel()
            {
                PublishedOn = DateTime.UtcNow.ToString("dd-MM-yyyy"),
                Terrains = await _terrainService.GetAllTerrainsAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(DestinationAddInputModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Terrains = await _terrainService.GetAllTerrainsAsync();
                return View(model);
            }

            string? userId = GetUserId();

            bool isAdded = await _destinationService.AddDestinationAsync(userId, model);

            if (isAdded)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError(string.Empty, "Failed to add destination.");
            model.Terrains = await _terrainService.GetAllTerrainsAsync();

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            string? userId = GetUserId();

            var destinationDetails = await _destinationService.GetDestinationDetailsAsync(id, userId);

            if (destinationDetails == null)
            {
                return NotFound();
            }
            return View(destinationDetails);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            string? userId = GetUserId();

            var destinationEditModel = await _destinationService.GetForEditDestinationAsync(id, userId);

            if (destinationEditModel == null)
            {
                return NotFound();
            }

            destinationEditModel.Terrains = await _terrainService.GetAllTerrainsAsync();

            return View(destinationEditModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(DestinationEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Terrains = await _terrainService.GetAllTerrainsAsync();
                return View(model);
            }
            string? userId = GetUserId();

            bool isEdited = await _destinationService.EditDestinationAsync(model, userId);

            if (isEdited)
            {
                return RedirectToAction(nameof(Details), new { id = model.Id });
            }

            ModelState.AddModelError(string.Empty, "Failed to edit destination.");
            model.Terrains = await _terrainService.GetAllTerrainsAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            string? userId = GetUserId();

            var destinationDeleteModel = await _destinationService.GetForDeleteDestinationAsync(id, userId);

            if (destinationDeleteModel == null)
            {
                return NotFound();
            }

            return View(destinationDeleteModel);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            string? userId = GetUserId();

            bool isDeleted = await _destinationService.DeleteDestinationAsync(id, userId);

            if (isDeleted)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError(string.Empty, "Failed to delete destination.");

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Favorites(int? id)
        {
            string? userId = GetUserId();

            IEnumerable<DestinationFavoritesViewModel?>? favoritesModel = await _destinationService.GetFavoritesAsync(userId);

            return View(favoritesModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddToFavorites(int? id)
        {
            try
            {
                string userId = this.GetUserId()!;
                if (id == null)
                {
                    return RedirectToAction(nameof(Index));
                }

                bool favAddResult = await this._destinationService
                    .AddToFavoritesAsync(id.Value, userId);

                if (favAddResult == false)
                {
                    return RedirectToAction(nameof(Index));
                }

                return RedirectToAction(nameof(Favorites));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                return RedirectToAction(nameof(Index));
            }


        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromFavorites(int? id)
        {
            try
            {
                string userId = this.GetUserId()!;

                if (id == null)
                {
                    return RedirectToAction(nameof(Index));
                }

                bool favRemoveResult = await this._destinationService
                    .RemoveFromFavoritesAsync(id.Value, userId);

                if (favRemoveResult == false)
                {
                    return RedirectToAction(nameof(Index));
                }

                return RedirectToAction(nameof(Favorites));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                return RedirectToAction(nameof(Index));
            }

        }
    }
}
