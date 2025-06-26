namespace Horizons.Services.Core
{
    using Horizons.Data;
    using Horizons.Data.Models;
    using Horizons.Services.Core.Contracts;
    using Horizons.Web.ViewModels;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public class DestinationService : IDestinationService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> userManager;

        public DestinationService(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            this._context = context;
            this.userManager = userManager;
        }

        public async Task<IEnumerable<DestinationIndexViewModel>> GetAllDestinationsAsync(string? userId)
        {
            var destinations = await _context.Destinations
                .Where(d => d.IsDeleted == false)
                .Include(d => d.Terrain)
                .Include(d => d.UsersDestinations)
                .AsNoTracking()
                .Select(d => new DestinationIndexViewModel
                {
                    Id = d.Id,
                    Name = d.Name,
                    ImageUrl = d.ImageUrl,
                    Terrain = d.Terrain.Name,
                    FavoritesCount = d.UsersDestinations.Count(),
                    IsPublisher = d.PublisherId == userId,
                    IsFavorite = d.UsersDestinations.Any(f => f.UserId == userId)
                })
                .ToListAsync();

            return destinations;
        }

        public Task<bool> AddDestinationAsync(string? userId, DestinationAddInputModel model)
        {
            if (userId == null)
            {
                return Task.FromResult(false);
            }

            var destination = new Destination
            {
                Name = model.Name,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                TerrainId = model.TerrainId,
                PublisherId = userId,
                PublishedOn = DateTime.UtcNow
            };

            _context.Destinations.Add(destination);
            _context.SaveChanges();

            return Task.FromResult(true);
        }

        public async Task<DestinationDetailsViewModel?> GetDestinationDetailsAsync(int id, string? userId)
        {
            var destination = await _context.Destinations
                .Where(d => d.Id == id && d.IsDeleted == false)
                .Include(d => d.Terrain)
                .Include(d => d.UsersDestinations)
                .AsNoTracking()
                .Select(d => new DestinationDetailsViewModel
                {
                    Id = d.Id,
                    Name = d.Name,
                    Description = d.Description,
                    ImageUrl = d.ImageUrl,
                    Terrain = d.Terrain.Name,
                    Publisher = d.Publisher.UserName ?? "Unknown Publisher",
                    PublishedOn = d.PublishedOn.ToString("dd-MM-yyyy"),
                    IsPublisher = d.PublisherId == userId,
                    IsFavorite = d.UsersDestinations.Any(f => f.UserId == userId),
                })
                .FirstOrDefaultAsync();

            return destination;
        }

        public async Task<DestinationEditViewModel> GetForEditDestinationAsync(int id, string? userId)
        {
            var destination = await _context.Destinations
                .Where(d => d.Id == id && d.IsDeleted == false)
                .Include(d => d.Terrain)
                .AsNoTracking()
                .Select(d => new DestinationEditViewModel
                {
                    Id = d.Id,
                    Name = d.Name,
                    Description = d.Description,
                    ImageUrl = d.ImageUrl,
                    PublishedOn = d.PublishedOn.ToString("dd-MM-yyyy"),
                    TerrainId = d.TerrainId,
                    PublisherId = d.PublisherId
                })
                .FirstOrDefaultAsync();

            if (destination == null || destination.PublisherId != userId)
            {
                throw new ArgumentException("didnt happen g");
            }

            destination.Terrains = await _context.Terrains
                .AsNoTracking()
                .Select(t => new DestinationAddTerrainDropDownModel
                {
                    Id = t.Id,
                    Name = t.Name
                })
                .ToListAsync();

            return destination;

        }

        public async Task<bool> EditDestinationAsync(DestinationEditViewModel model, string? userId)
        {
            if (userId == null)
            {
                return false;
            }

            var destination = await _context.Destinations
                .Where(d => d.Id == model.Id && d.IsDeleted == false && d.PublisherId == userId)
                .FirstOrDefaultAsync();

            if (destination == null)
            {
                return false;
            }

            destination.Name = model.Name;
            destination.Description = model.Description;
            destination.ImageUrl = model.ImageUrl;
            destination.TerrainId = model.TerrainId;

            _context.Destinations.Update(destination);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<DestinationDeleteViewModel> GetForDeleteDestinationAsync(int id, string? userId)
        {
            var destination = await _context.Destinations
                .Where(d => d.Id == id && d.IsDeleted == false)
                .Include(d => d.Publisher)
                .AsNoTracking()
                .Select(d => new DestinationDeleteViewModel
                {
                    Id = d.Id,
                    Name = d.Name,
                    PublisherId = d.PublisherId,
                    Publisher = d.Publisher.UserName ?? "Unknown Publisher",
                })
                .FirstOrDefaultAsync();

            if (destination == null || destination.PublisherId != userId)
            {
                throw new ArgumentException("didnt happen g");
            }

            return destination;
        }

        public async Task<bool> DeleteDestinationAsync(int id, string? userId)
        {
            if (userId == null)
            {
                return false;
            }

            var destination = await _context.Destinations
                .Where(d => d.Id == id && d.IsDeleted == false && d.PublisherId == userId)
                .FirstOrDefaultAsync();

            if (destination == null)
            {
                return false;
            }

            destination.IsDeleted = true;

            _context.Destinations.Update(destination);

            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<IEnumerable<DestinationFavoritesViewModel>?> GetFavoritesAsync(string userId)
        {
            return await _context.UsersDestinations
                .Where(ud => ud.UserId.ToLower() == userId.ToLower())
                .Include(ud => ud.Destination)
                .ThenInclude(d => d.Terrain)
                .Select(d => new DestinationFavoritesViewModel
                {
                    Id = d.Destination.Id,
                    Name = d.Destination.Name,
                    ImageUrl = d.Destination.ImageUrl,
                    Terrain = d.Destination.Terrain.Name,
                })
                .ToListAsync();
        }

        public async Task<bool> AddToFavoritesAsync(int id, string? userId)
        {
            if (userId == null)
            {
                return false;
            }

            var destination = await _context.Destinations
                .Where(d => d.Id == id && d.IsDeleted == false)
                .FirstOrDefaultAsync();

            if (destination == null)
            {
                return false;
            }

            var userDestination = new UserDestination
            {
                UserId = userId,
                DestinationId = id
            };

            _context.UsersDestinations.Add(userDestination);
            await _context.SaveChangesAsync();

            return true;

        }

        public async Task<bool> RemoveFromFavoritesAsync(int id, string? userId)
        {
            if (userId == null)
            {
                return false;
            }

            var userDestination = await _context.UsersDestinations
                .Where(ud => ud.DestinationId == id && ud.UserId == userId)
                .FirstOrDefaultAsync();

            if (userDestination == null)
            {
                return false;
            }

            _context.UsersDestinations.Remove(userDestination);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
