namespace Horizons.Data.Configurations
{
    using Horizons.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using static Horizons.GCommon.ValidationConstants.Terrain;
    public class TerrainConfiguration : IEntityTypeConfiguration<Terrain>
    {
        public void Configure(EntityTypeBuilder<Terrain> entity)
        {
            entity
                .HasKey(t => t.Id);

            entity
                .Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(TerrainNameMaxLength);

            //entity
            //    .HasMany(t => t.Destinations)
            //    .WithOne(d => d.Terrain)
            //    .HasForeignKey(d => d.TerrainId)
            //    .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
