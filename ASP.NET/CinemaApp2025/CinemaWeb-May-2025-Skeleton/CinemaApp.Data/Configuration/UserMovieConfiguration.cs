using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaApp.Web.Areas.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CinemaApp.Data.Configuration
{
    public class UserMovieConfiguration : IEntityTypeConfiguration<UserMovie>
    {
        public void Configure(EntityTypeBuilder<UserMovie> builder)
        {
            builder
                .HasKey(um => new { um.UserId, um.MovieId });

            builder
                .HasOne(um => um.User)
                .WithMany()
                .HasForeignKey(um => um.UserId);

            builder
                .HasOne(um => um.Movie)
                .WithMany()
                .HasForeignKey(um => um.MovieId);
        }
    }
}
