using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaApp.Web.ViewModels.Watchlist;

namespace CinemaApp.Services.Core.Interfaces
{
    public interface IWatchlistService
    {
        Task<IEnumerable<WatchlistViewModel>> GetUserWatchlistAsync(string userId);

        Task<bool> IsMovieInWatchlistAsync(string userId, Guid movieId);

        Task AddToWatchlistAsync(string userId, string movieId);

        Task RemoveFromWatchlistAsync(string userId, string movieId);
    }
}
