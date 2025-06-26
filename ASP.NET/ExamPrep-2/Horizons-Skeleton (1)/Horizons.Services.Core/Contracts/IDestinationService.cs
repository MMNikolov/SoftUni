using Horizons.Data.Models;
using Horizons.Web.ViewModels;

namespace Horizons.Services.Core.Contracts
{
    public interface IDestinationService
    {
        Task<IEnumerable<DestinationIndexViewModel>> GetAllDestinationsAsync(string? userId);
        Task<bool> AddDestinationAsync(string? userId, DestinationAddInputModel model);
        Task<DestinationDetailsViewModel?> GetDestinationDetailsAsync(int id, string? userId);
        Task<DestinationEditViewModel> GetForEditDestinationAsync(int id, string? userId);
        Task<bool> EditDestinationAsync(DestinationEditViewModel model, string? userId);
        Task<DestinationDeleteViewModel> GetForDeleteDestinationAsync(int id, string? userId);
        Task<bool> DeleteDestinationAsync(int id, string? userId);
        Task<IEnumerable<DestinationFavoritesViewModel>?> GetFavoritesAsync(string userId);
        Task<bool> AddToFavoritesAsync(int id, string? userId);
        Task<bool> RemoveFromFavoritesAsync(int id, string? userId);

    }
}
