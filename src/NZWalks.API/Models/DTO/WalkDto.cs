using System;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Models.DTO
{
	public class WalkDto
	{
        public Guid Id { get; set; } = default;
        public string Name { get; set; } = default;
        public string Description { get; set; } = default;
        public double LengthInKm { get; set; } = default;
        public string? WalkImageUrl { get; set; }

        public RegionDTO Region { get; set; }
        public DifficultyDto Difficulty { get; set; } 

    }
}

