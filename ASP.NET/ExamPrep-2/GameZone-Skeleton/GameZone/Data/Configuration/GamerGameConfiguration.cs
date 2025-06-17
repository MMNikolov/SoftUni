using GameZone.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameZone.Data.Configuration
{
    public class GamerGameConfiguration : IEntityTypeConfiguration<GamerGame>
    {
        public void Configure(EntityTypeBuilder<GamerGame> builder)
        {
            builder
                .HasKey(gg => new { gg.GamerId, gg.GameId });

            builder
                .HasOne(gg => gg.Gamer)
                .WithMany()
                .HasForeignKey(gg => gg.GamerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(gg => gg.Game)
                .WithMany(g => g.GamersGames)
                .HasForeignKey(gg => gg.GameId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
