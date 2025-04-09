namespace NZWalks.API.Models.Domain
{
    public class Walks
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double lengthInKm {  get; set; }
        public string? WalkImageUrl {  get; set; }
        public Guid RegionId { get; set; }
        public Guid DifficultyId { get; set; }


        // navigation property
        public Difficulty Difficulty { get; set; }
        public Regions Regions { get; set; }    
    }
}
