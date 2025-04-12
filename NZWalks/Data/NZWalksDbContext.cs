using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
    public class NZWalksDbContext : DbContext
    {
        public NZWalksDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Regions> Regions { get; set; }
        public DbSet<Walks> Walks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data for Difficulties
            var difficulties = new List<Difficulty>
            {
                new Difficulty
                {
                    id = Guid.Parse("a5846353-4058-4d25-8009-5ce53bc477a2"),
                    name = "Easy"
                },
                new Difficulty
                {
                    id = Guid.Parse("6fa3457b-0b32-435a-9655-72fec73bec45"),
                    name = "Hard"
                },
                new Difficulty
                {
                    id = Guid.Parse("666b45b2-e595-43f4-9ddf-22bfa3aefbf7"),
                    name = "Medium"
                }
            };

            modelBuilder.Entity<Difficulty>().HasData(difficulties);

            // Seed data for Regions
            var regions = new List<Regions>
            {
                new Regions
                {
                    id = Guid.Parse("69a8231e-22f4-4f72-99b6-e2b7bdc430e7"),
                    Name = "Auckland",
                    Code = "AKL",
                    RegionImageUrl = "https://www.shutterstock.com/image-photo/beautiful-mountains-landscape-pictures-arang-kel-kashmir-2499596223"
                },
                new Regions
                {
                    id = Guid.Parse("c60012f0-1dfe-4f2f-9af5-d8d7c5d499d6"),
                    Name = "Kashmir",
                    Code = "KSM",
                    RegionImageUrl = "https://www.shutterstock.com/image-photo/stunning-view-kashmir's-aru-valley-sunset-where-2473158391"
                }
            };

            modelBuilder.Entity<Regions>().HasData(regions);
        }
    }
}
