using GameZone.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static GameZone.GlobalConstants.GameConstants;

namespace GameZone.Data.Configuration
{
    public class GameConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder
                .HasKey(g => g.Id);

            builder
                .Property(g => g.Title)
                .IsRequired()
                .HasMaxLength(TitleMaxLength);

            builder
                .Property(g => g.Description)
                .IsRequired()
                .HasMaxLength(DescriptionMaxLength);

            builder
                .Property(g => g.ImageUrl)
                .IsRequired(false);

            builder
                .Property(g => g.PublisherId)
                .IsRequired();

            builder
                .HasOne(g => g.Publisher)
                .WithMany()
                .HasForeignKey(g => g.PublisherId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Property(g => g.ReleasedOn)
                .IsRequired();

            builder
                .Property(g => g.GenreId)
                .IsRequired();

            builder
                .HasOne(g => g.Genre)
                .WithMany(ge => ge.Games)
                .HasForeignKey(g => g.GenreId);
        }
    }
    
}
