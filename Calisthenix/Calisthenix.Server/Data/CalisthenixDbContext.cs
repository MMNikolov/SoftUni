using System.Reflection;
using Calisthenix.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace Calisthenix.Server.Data
{
    public class CalisthenixDbContext : DbContext
    {
        public DbSet<Exercise> Exercises { get; set; }

        public CalisthenixDbContext(DbContextOptions<CalisthenixDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
