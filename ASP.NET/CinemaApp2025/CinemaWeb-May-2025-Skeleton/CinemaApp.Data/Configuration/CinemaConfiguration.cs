using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CinemaApp.Data.Configuration
{
    public class CinemaConfiguration : IEntityTypeConfiguration<Cinema>
    {
        public void Configure(EntityTypeBuilder<Cinema> builder)
        {
            builder
                .HasKey(c => c.Id);

            builder
                .Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(80);

            builder
                .Property(c => c.Location)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .Property(c => c.IsDeleted)
                .HasDefaultValue(false);

            builder
                .HasMany(c => c.CinemaMovies)
                .WithOne(cm => cm.Cinema)
                .HasForeignKey(cm => cm.CinemaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasMany(c => c.Tickets)
                .WithOne(t => t.Cinema)
                .HasForeignKey(t => t.CinemaId)
                .OnDelete(DeleteBehavior.Restrict);
                
        }
    }
}
