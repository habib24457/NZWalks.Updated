using System;
namespace NZWalks.API.Models.DTO
{
	public class AddRegionRequestDto
	{
        public string Name { get; set; } = default;
        public string Code { get; set; } = default;
        public string? RegionImageUrl { get; set; }
    }
}

