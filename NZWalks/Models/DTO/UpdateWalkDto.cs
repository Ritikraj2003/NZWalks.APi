﻿namespace NZWalks.API.Models.DTO
{
    public class UpdateWalkDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double lengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }

     
        public Guid RegionId { get; set; }
        public Guid DifficultyId { get; set; }
    }
}
