using Horizons.Services.Core.Contracts;
using Horizons.Web.ViewModels.Destination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using static Horizons.GCommon.ValidationConstants.Destination;

namespace Horizons.Web.Controllers
{
    public class DestinationController : BaseController
    {
        private readonly IDestinationService DestinationService;
        private readonly ITerrainService TerrainService;

        public DestinationController(IDestinationService destinationService, ITerrainService terrainService)
        {
            this.DestinationService = destinationService;
            TerrainService = terrainService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            string? userId = this.GetUserId();
            IEnumerable<DestinationIndexViewModel> allDestinations =
                await this.DestinationService.GetAllDestinationsAsync(userId);

            return View(allDestinations);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            string? userId = this.GetUserId();

            if (id <= 0)
            {
                return BadRequest("Invalid destination ID.");
            }

            DestinationDetailsViewModel destinationDetails =
                await this.DestinationService.GetDestinationDetailsAsync(id, userId);

            if (destinationDetails == null)
            {
                return RedirectToAction("Index", "Destination");
            }


            return View(destinationDetails);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            DestinationAddInputModel model = new DestinationAddInputModel()
            {
                PublishedOn = DateTime.UtcNow.ToString(DateFormat),
                Terrains = await this.TerrainService.GetAllTerrainsAsync()
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(DestinationAddInputModel model)
        {

            try
            {
                if (!this.ModelState.IsValid)
                {
                    return this.RedirectToAction(nameof(Add));
                }

                bool isAdded = await this.DestinationService.AddDestinationAsync(this.GetUserId()!, model);

                if (isAdded == false)
                {
                    ModelState.AddModelError(string.Empty, "Failed to add destination. Please try again.");
                    return RedirectToAction(nameof(Add));
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                return this.RedirectToAction(nameof(Index));
            }

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? Id)
        {
            string userId = GetUserId()!;

            EditDestinationInputModel? editInputModel = await this.DestinationService.GetEditDestinationAsync(userId, Id);

            if (editInputModel == null)
            {
                return RedirectToAction(nameof(Index));
            }

            editInputModel.Terrains = await this.TerrainService.GetAllTerrainsAsync();

            return View(editInputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditDestinationInputModel model)
        {
            var userId = this.GetUserId()!;

            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction(nameof(Edit), new { Id = model.Id });
            }

            try
            {
                bool isEdited = await this.DestinationService.EditDestinationAsync(userId, model);

                if (isEdited == false)
                {
                    ModelState.AddModelError(string.Empty, "Failed to edit destination. Please try again.");
                    return RedirectToAction(nameof(Edit), new { Id = model.Id });
                }

                return RedirectToAction(nameof(Details), new { id = model.Id });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return RedirectToAction(nameof(Index));
            }

        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            string userId = this.GetUserId()!;

            if (id <= 0)
            {
                return BadRequest("Invalid destination ID.");
            }

            DeleteDestinationViewModel? deleteInputModel = await this.DestinationService.GetForDeleteDestinationAsync(userId, id);

            if (deleteInputModel == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(deleteInputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteDestinationViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Invalid model state. Please check the input data.");
                return this.View(model);
            }

            bool isDeleted = await this.DestinationService.SoftDeleteDestinationAsync(this.GetUserId()!, model);

            if (isDeleted == false)
            {
                this.ModelState.AddModelError(string.Empty, "Failed to delete destination. Please try again.");
                return this.View(model);
            }

            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public async Task<IActionResult> Favorites()
        {
            string userId = this.GetUserId()!;

            IEnumerable<FavoriteDestinationsViewModel>? favoriteInputModels = await this.DestinationService.GetForFavoriteDestinationAsync(userId);

            if (favoriteInputModels == null )
            {
                return RedirectToAction(nameof(Index));
            }

            return View(favoriteInputModels);
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

                bool favAddResult = await this.DestinationService
                    .AddToFavoriteDestinationAsync(userId, id.Value);

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

                bool favRemoveResult = await this.DestinationService
                    .RemoveFromFavoriteDestinationAsync(userId, id.Value);

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
