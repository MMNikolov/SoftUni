using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaApp.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Data.Models
{
    [Comment("Movie in the system")]
    public class Movie
    {
        [Comment("Movie Identifier")]
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Comment("Movie Title")]
        [Required(ErrorMessage = "Title is required.")]
        [StringLength(EntityConstants.Movie.TitleMaxLength, ErrorMessage = "Title cannot exceed 100 characters.")]
        public string Title { get; set; } = null!;

        [Comment("Movie Genre")]
        [Required(ErrorMessage = "Genre is required.")]
        [StringLength(EntityConstants.Movie.GenreMaxLength, ErrorMessage = "Genre cannot exceed 50 characters.")]
        public string Genre { get; set; } = null!;

        [Comment("Movie Release Date")]
        [Required(ErrorMessage = "Release date is required.")]
        public DateTime ReleaseDate { get; set; }

        [Comment("Movie Director")]
        [Required(ErrorMessage = "Director is required.")]
        [StringLength(EntityConstants.Movie.DirectorNameMaxLength, ErrorMessage = "Director cannot exceed 100 characters.")]
        public string Director { get; set; } = null!;

        [Comment("Movie Duration in minutes")]
        [Required(ErrorMessage = "Duration is required.")]
        [Range(EntityConstants.Movie.DurationMin, EntityConstants.Movie.DurationMax, ErrorMessage = "Duration must be between 1 and 300 minutes.")]
        public int Duration { get; set; }

        [Comment("Movie Description")]
        [Required(ErrorMessage = "Description is required.")]
        [StringLength(EntityConstants.Movie.DescriptionMaxLength, ErrorMessage = "Description cannot exceed 1000 characters.")]
        public string Description { get; set; } = null!;

        [Comment("Movie Image URL")]
        [Url(ErrorMessage = "Invalid URL format.")]
        public string? ImageUrl { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
