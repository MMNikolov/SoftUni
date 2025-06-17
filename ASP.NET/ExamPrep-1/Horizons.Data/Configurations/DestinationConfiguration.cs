namespace Horizons.Data.Configurations
{
    using Horizons.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using static Horizons.GCommon.ValidationConstants.Destination;

    public class DestinationConfiguration : IEntityTypeConfiguration<Destination>
    {
        public void Configure(EntityTypeBuilder<Destination> entity)
        {
            entity
                .HasKey(d => d.Id);

            entity
                .Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(NameMaxLength);

            entity
                .Property(d => d.Description)
                .IsRequired()
                .HasMaxLength(DescriptionMaxLength);

            entity
                .Property(d => d.ImageUrl)
                .IsRequired(false);

            entity
                .Property(d => d.PublisherId)
                .IsRequired();

            entity
                .Property(d => d.PublishedOn)
                .IsRequired();
                

            entity
                .Property(d => d.IsDeleted)
                .HasDefaultValue(false);

            entity
                .HasOne(d => d.Terrain)
                .WithMany(t => t.Destinations)
                .HasForeignKey(d => d.TerrainId)
                .OnDelete(DeleteBehavior.Restrict);

            entity
                .HasOne(d => d.Publisher)
                .WithMany()
                .HasForeignKey(d => d.PublisherId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
