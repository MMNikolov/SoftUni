using System.Globalization;
using CinemaApp.Data;
using CinemaApp.Data.Common;
using CinemaApp.Data.Repository.Contracts;
using CinemaApp.Services.Core.Interfaces;
using CinemaApp.Web.ViewModels.Movie;
using Microsoft.EntityFrameworkCore;
using static CinemaApp.Data.Common.EntityConstants.Movie;

namespace CinemaApp.Services.Core
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;

        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<IEnumerable<AllMoviesIndexViewModel>> GetAllMoviesAsync()
        {
            return await _movieRepository.GetAllAttached()
                .Where(m => !m.IsDeleted)
                .AsNoTracking()
                .Select(m => new AllMoviesIndexViewModel
                {
                    Id = m.Id.ToString(),
                    Title = m.Title,
                    Genre = m.Genre,
                    Director = m.Director,
                    ReleaseDate = m.ReleaseDate.ToString("yyyy-MM-dd"),
                    ImageUrl = m.ImageUrl
                })
                .ToListAsync();
        }

        public async Task AddAsync(MovieFormViewModel model)
        {
            var newMovie = new Data.Models.Movie
            {
                Title = model.Title,
                Genre = model.Genre,
                Director = model.Director,
                Description = model.Description,
                Duration = model.Duration,
                ReleaseDate = DateTime.ParseExact(model.ReleaseDate, ReleaseDateFormat, CultureInfo.InvariantCulture),
                ImageUrl = model.ImageUrl
            };

            await _movieRepository.AddAsync(newMovie);
            await _movieRepository.SaveChangesAsync();
        }

        public async Task<MovieDetailsViewModel> GetMovieByIdAsync(string id)
        {
            var movie = await _movieRepository.GetAllAttached()
                .Where(m => m.Id.ToString() == id && !m.IsDeleted)
                .Select(m => new MovieDetailsViewModel
                {
                    Id = m.Id.ToString(),
                    Title = m.Title,
                    Genre = m.Genre,
                    Director = m.Director,
                    Description = m.Description,
                    Duration = m.Duration,
                    ReleaseDate = m.ReleaseDate.ToString("yyyy-MM-dd"),
                    ImageUrl = m.ImageUrl
                })
                .FirstOrDefaultAsync();

            return movie ?? throw new InvalidOperationException("Movie not found.");
        }

        public async Task<MovieFormViewModel> GetForEditByIdAsync(string id)
        {
            var movie = await _movieRepository.GetAllAttached()
                .Where(m => m.Id.ToString() == id && !m.IsDeleted)
                .Select(m => new MovieFormViewModel
                {
                    Id = m.Id.ToString(),
                    Title = m.Title,
                    Genre = m.Genre,
                    Director = m.Director,
                    Description = m.Description,
                    Duration = m.Duration,
                    ReleaseDate = m.ReleaseDate.ToString(ReleaseDateFormat),
                    ImageUrl = m.ImageUrl
                })
                .FirstOrDefaultAsync();

            return movie ?? throw new InvalidOperationException("Movie not found.");
        }

        public async Task EditAsync(string id, MovieFormViewModel model)
        {
            var movie = await _movieRepository.GetAllAttached()
                .FirstOrDefaultAsync(m => m.Id.ToString() == id);

            if (movie == null)
            {
                throw new InvalidOperationException("Movie not found.");
            }

            movie.Title = model.Title;
            movie.Genre = model.Genre;
            movie.Director = model.Director;
            movie.Description = model.Description;
            movie.Duration = model.Duration;
            movie.ReleaseDate = DateTime.ParseExact(model.ReleaseDate, ReleaseDateFormat, CultureInfo.InvariantCulture);
            movie.ImageUrl = model.ImageUrl;

            _movieRepository.Update(movie);
            await _movieRepository.SaveChangesAsync();
        }

        public async Task SoftDeleteAsync(string id)
        {
            var movie = await _movieRepository.GetAllAttached()
                .FirstOrDefaultAsync(m => m.Id.ToString() == id);

            if (movie != null && !movie.IsDeleted)
            {
                movie.IsDeleted = true;
                await _movieRepository.SaveChangesAsync();
            }

        }

        public async Task HardDeleteAsync(string id)
        {
            var movie = await _movieRepository.GetAllAttached() 
                .FirstOrDefaultAsync(m => m.Id.ToString() == id);

            if (movie != null)
            {
                _movieRepository.Delete(movie);
                await _movieRepository.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Movie not found.");
            }
        }
    }
}
