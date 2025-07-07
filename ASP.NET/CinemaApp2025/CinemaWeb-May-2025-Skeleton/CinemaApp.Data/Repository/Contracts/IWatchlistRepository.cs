using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaApp.Web.Areas.Identity;


namespace CinemaApp.Data.Repository.Contracts
{
    public interface IWatchlistRepository : IRepository<UserMovie, string>
    {
        Task<UserMovie?> GetByCompositeKeyAsync(string userId, string movieId);
        Task<bool> ExistsAsync(string userId, string movieId);
    }
}
