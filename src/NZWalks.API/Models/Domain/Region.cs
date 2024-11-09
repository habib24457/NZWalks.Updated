using System;
namespace NZWalks.API.Models.Domain
{
	public class Region
	{
        //we can't pass any null values except RegionImageUrl. That's why we have used optional chain '?'
        public Guid Id { get; set; } = default;
        public string Name { get; set; } = default;
        public string Code { get; set; } = default;
        public string? RegionImageUrl { get; set; }

    }
}

