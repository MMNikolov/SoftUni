namespace Horizons.Data.Configurations
{
    using Horizons.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    public class UserDestinationConfiguration : IEntityTypeConfiguration<UserDestination>
    {
        public void Configure(EntityTypeBuilder<UserDestination> entity)
        {
            entity
                .HasKey(ud => new { ud.UserId, ud.DestinationId });

            entity
                .HasOne(ud => ud.User)
                .WithMany()
                .HasForeignKey(ud => ud.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            entity
                .HasOne(ud => ud.Destination)
                .WithMany(d => d.UserDestinations)
                .HasForeignKey(ud => ud.DestinationId)
                .OnDelete(DeleteBehavior.Restrict);

            entity
                .HasQueryFilter(ud => ud.Destination.IsDeleted == false);

        }
    }
}
