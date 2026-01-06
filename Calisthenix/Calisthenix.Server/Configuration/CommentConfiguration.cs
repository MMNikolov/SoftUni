namespace Calisthenix.Server.Configuration
{
    using Calisthenix.Server.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder
                .HasOne(c => c.Exercise)
                .WithMany(e => e.Comments)
                .HasForeignKey(c => c.ExerciseId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
