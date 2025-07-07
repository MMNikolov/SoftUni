using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaApp.Data;
using CinemaApp.Data.Repository.Contracts;
using CinemaApp.Services.Core.Interfaces;
using CinemaApp.Web.Areas.Identity;
using CinemaApp.Web.ViewModels.Watchlist;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Services.Core
{
    public class WatchlistService : IWatchlistService
    {
        private readonly IWatchlistRepository _watchlistRepository;

        public WatchlistService(IWatchlistRepository watchlistRepository)
        {
            _watchlistRepository = watchlistRepository;
        }

        public async Task<IEnumerable<WatchlistViewModel>> GetUserWatchlistAsync(string userId)
        {
            return await _watchlistRepository.GetAllAttached()
                .Where(w => w.UserId == userId)
                .Select(w => new WatchlistViewModel
                {
                    MovieId = w.Movie.Id.ToString(),
                    Title = w.Movie.Title,
                    Genre = w.Movie.Genre,
                    ImageUrl = w.Movie.ImageUrl,
                    ReleaseDate = w.Movie.ReleaseDate.ToString("yyyy-MM-dd"),
                })
                .ToListAsync();

        }

        public async Task<bool> IsMovieInWatchlistAsync(string userId, Guid movieId)
        {
            return await _watchlistRepository.GetAllAttached()
                .AnyAsync(w => w.UserId == userId && w.MovieId == movieId);
        }

        public async Task AddToWatchlistAsync(string userId, string movieId)
        {
            var userMovie = new UserMovie
            {
                UserId = userId,
                MovieId = Guid.Parse(movieId)
            };

            await _watchlistRepository.AddAsync(userMovie);
            await _watchlistRepository.SaveChangesAsync();
        }

        public async Task RemoveFromWatchlistAsync(string userId, string movieId)
        {
            var userMovie = await _watchlistRepository.GetAllAttached()
                .FirstOrDefaultAsync(w => w.UserId == userId && w.MovieId.ToString() == movieId);

            if (userMovie != null)
            {
                _watchlistRepository.Delete(userMovie);
                await _watchlistRepository.SaveChangesAsync();
            }
        }
    }
}
