using System;
namespace NZWalks.API.Models.DTO
{
	public class AddWalkRequestDto
	{
        public string Name { get; set; } = default;
        public string Description { get; set; } = default;
        public double LengthInKm { get; set; } = default;
        public string? WalkImageUrl { get; set; }
        public Guid DifficultyId { get; set; } = default;
        public Guid RegionId { get; set; } = default;
    }
}

