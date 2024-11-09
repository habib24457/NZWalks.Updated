using System;
namespace NZWalks.API.Models.DTO
{
	public class UpdateRegionRequestDto
	{
        public string Name { get; set; } = default;
        public string Code { get; set; } = default;
        public string? RegionImageUrl { get; set; }
    }
}

