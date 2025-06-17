
namespace Horizons.Services.Core.Contracts
{
    using Horizons.Web.ViewModels.Destination;
    public interface IDestinationService
    {
        Task<IEnumerable<DestinationIndexViewModel>> GetAllDestinationsAsync(string? userId);

        Task<DestinationDetailsViewModel> GetDestinationDetailsAsync(int id, string? userId);

        Task<bool> AddDestinationAsync(string userId, DestinationAddInputModel model);

        Task<EditDestinationInputModel?> GetEditDestinationAsync(string userId, int? destinationId);

        Task<bool> EditDestinationAsync(string userId, EditDestinationInputModel model);

        Task<DeleteDestinationViewModel> GetForDeleteDestinationAsync(string userId, int? destinationId);

        Task<bool> SoftDeleteDestinationAsync(string userId, DeleteDestinationViewModel model);

        Task<IEnumerable<FavoriteDestinationsViewModel>?> GetForFavoriteDestinationAsync(string userId);

        Task<bool> AddToFavoriteDestinationAsync(string userId, int destId);

        Task<bool> RemoveFromFavoriteDestinationAsync(string userId, int destId);

    }
}
