using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaApp.Data.Repository.Contracts;
using CinemaApp.Web.Areas.Identity;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Data.Repository
{
    public class WatchlistRepository : BaseRepository<UserMovie, string>, IWatchlistRepository
    {
        private readonly CinemaAppDbContext Context;
        public WatchlistRepository(CinemaAppDbContext dbcontext)
            : base(dbcontext)
        {
            this.Context = dbcontext;
        }

        public async Task<UserMovie?> GetByCompositeKeyAsync(string userId, string movieId)
        {
            return await this.GetAllAttached()
                .FirstOrDefaultAsync(um => um.UserId == userId && um.MovieId.ToString().ToLower() == movieId.ToString().ToLower());
        }

        public async Task<bool> ExistsAsync(string userId, string movieId)
        {
            return await this.Context.UserMovies
                .AnyAsync(um => um.UserId == userId && um.MovieId.ToString().ToLower() == movieId.ToString().ToLower());
        }

    }
}
