﻿namespace NZWalks.API.Models.DTO
{
    public class WalkDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double lengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        //public Guid? RegionId { get; set; }
        //public Guid DifficultyId { get; set; }

        public ResgionDto Region { get; set; }
        public DificultyDto Difficulty { get; set; }
    }
}
