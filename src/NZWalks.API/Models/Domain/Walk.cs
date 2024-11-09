using System;
namespace NZWalks.API.Models.Domain
{
	public class Walk
	{
		public Guid Id { get; set; } = default;
        public string Name { get; set; } = default;
        public string Description { get; set; } = default;
        public double LengthInKm { get; set; } = default;
        public string? WalkImageUrl { get; set; } 

        public Guid DifficultyId { get; set; } = default;
        public Guid RegionId { get; set; } = default;

        //Navigation Properties
        public Difficulty Difficulty { get; set; } = default;
        public Region Region { get; set; } = default;

    }
}

