namespace NZWalks.API.Models.Domain
{
    public class Walks
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double lengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }

        // Foreign Keys
        public Guid RegionId { get; set; }
        public Guid DifficultyId { get; set; }

        // Navigation Properties
        public Regions Region { get; set; }    // singular
        public Difficulty Difficulty { get; set; }
    }
}
