namespace Calisthenix.Server.Data
{
    using System.Reflection;
    using Calisthenix.Server.Models;
    using Microsoft.EntityFrameworkCore;

    public class CalisthenixDbContext : DbContext
    {
        public virtual DbSet<Exercise> Exercises { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        public CalisthenixDbContext(DbContextOptions<CalisthenixDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
