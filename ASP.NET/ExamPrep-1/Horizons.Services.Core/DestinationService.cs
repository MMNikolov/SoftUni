namespace Horizons.Services.Core
{
    using System.Globalization;
    using Horizons.Data;
    using Horizons.Data.Models;
    using Horizons.Services.Core.Contracts;
    using Horizons.Web.ViewModels.Destination;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using static Horizons.GCommon.ValidationConstants.Destination;

    public class DestinationService : IDestinationService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public DestinationService(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public async Task<IEnumerable<DestinationIndexViewModel>> GetAllDestinationsAsync(string? userId)
        {
            return await _context.Destinations
                .Where(d => d.IsDeleted == false)
                .Include(d => d.Terrain)
                .Include(d => d.UserDestinations)
                .AsNoTracking()
                .Select(d => new DestinationIndexViewModel
                {
                    Id = d.Id,
                    Name = d.Name,
                    ImageUrl = d.ImageUrl,
                    TerrainName = d.Terrain.Name,
                    FavoritesCount = d.UserDestinations.Count(),
                    IsUserPublisher = userId != null && d.PublisherId == userId,
                    IsInUserFavorites = userId != null && d.UserDestinations.Any(ud => ud.UserId == userId)
                })
                .ToListAsync();

        }

        public async Task<DestinationDetailsViewModel> GetDestinationDetailsAsync(int id, string? userId)
        {
            var destination = await _context.Destinations
                .Where(d => d.Id == id && d.IsDeleted == false)
                .Include(d => d.Terrain)
                .Include(d => d.Publisher)
                .Include(d => d.UserDestinations)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (destination == null)
            {
                return null;
            }

            return new DestinationDetailsViewModel
            {
                Id = destination.Id,
                Name = destination.Name,
                Description = destination.Description,
                ImageUrl = destination.ImageUrl,
                PublisherName = destination.Publisher?.UserName ?? "Unknown",
                PublishedOn = destination.PublishedOn.ToString(DateFormat),
                Terrain = destination.Terrain.Name,
                IsPublisher = userId != null && destination.PublisherId == userId,
                IsFavorite = userId != null && destination.UserDestinations.Any(ud => ud.UserId == userId)
            };

        }

        public async Task<bool> AddDestinationAsync(string userId, DestinationAddInputModel model)
        {
            bool opResult = false;

            IdentityUser? user = await _userManager.FindByIdAsync(userId);

            Terrain? terrain = await _context.Terrains
                .FindAsync(model.TerrainId);

            bool isPublishedOnDateValid =
                DateTime.TryParseExact(model.PublishedOn, DateFormat, CultureInfo.InvariantCulture,
                DateTimeStyles.None, out DateTime publishedOnDate);

            if ((user != null) && (terrain != null) && (isPublishedOnDateValid))
            {
                Destination newDestination = new Destination
                {
                    Name = model.Name,
                    Description = model.Description,
                    ImageUrl = model.ImageUrl,
                    PublishedOn = publishedOnDate,
                    PublisherId = userId,
                    TerrainId = model.TerrainId,
                };

                await this._context.Destinations.AddAsync(newDestination);
                await this._context.SaveChangesAsync();

                opResult = true;
            }

            return opResult;
        }

        public async Task<EditDestinationInputModel?> GetEditDestinationAsync(string userId, int? destinationId)
        {
            if (destinationId == null)
            {
                return null;
            }

            var destination = await _context.Destinations
                .Where(d => d.Id == destinationId && d.PublisherId == userId && d.IsDeleted == false)
                .Include(d => d.Terrain)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (destination == null)
            {
                return null;
            }

            return new EditDestinationInputModel
            {
                Id = destination.Id,
                Name = destination.Name,
                Description = destination.Description,
                ImageUrl = destination.ImageUrl,
                PublishedOn = destination.PublishedOn.ToString(DateFormat),
                PublisherId = destination.PublisherId,
            };

        }

        public Task<bool> EditDestinationAsync(string userId, EditDestinationInputModel model)
        {
            bool opResult = false;

            IdentityUser? user = _userManager.FindByIdAsync(userId).Result;

            Terrain? terrain = _context.Terrains
                .Find(model.TerrainId);

            bool isPublishedOnDateValid =
                DateTime.TryParseExact(model.PublishedOn, DateFormat, CultureInfo.InvariantCulture,
                DateTimeStyles.None, out DateTime publishedOnDate);

            if ((user != null) && (terrain != null) && (isPublishedOnDateValid))
            {
                Destination destinationToEdit = new Destination
                {
                    Id = model.Id,
                    Name = model.Name,
                    Description = model.Description,
                    ImageUrl = model.ImageUrl,
                    PublishedOn = publishedOnDate,
                    PublisherId = userId,
                    TerrainId = model.TerrainId
                };

                _context.Destinations.Update(destinationToEdit);
                _context.SaveChanges();

                opResult = true;
            }

            return Task.FromResult(opResult);
        }

        public async Task<DeleteDestinationViewModel> GetForDeleteDestinationAsync(string userId, int? destinationId)
        {
            if (destinationId == null)
            {
                return null;
            }

            var destination = await _context.Destinations
                .Where(d => d.Id == destinationId && d.PublisherId == userId && d.IsDeleted == false)
                .Include(d => d.Publisher)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (destination == null)
            {
                return null;
            }

            return new DeleteDestinationViewModel
            {
                Id = destination.Id,
                Name = destination.Name,
                Publisher = destination.Publisher?.UserName ?? "Unknown",
                PublisherId = destination.PublisherId,
            };

        }

        public async Task<bool> SoftDeleteDestinationAsync(string userId, DeleteDestinationViewModel model)
        {
            bool opResult = false;
            IdentityUser? user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                Destination destinationToDelete = await _context.Destinations
                    .Where(d => d.Id == model.Id && d.PublisherId == userId && d.IsDeleted == false)
                    .FirstOrDefaultAsync();
                if (destinationToDelete != null)
                {
                    destinationToDelete.IsDeleted = true;
                    _context.Destinations.Update(destinationToDelete);
                    await _context.SaveChangesAsync();
                    opResult = true;
                }
            }
            return opResult;


        }
    }
}
