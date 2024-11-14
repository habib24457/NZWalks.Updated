using AutoMapper;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Map AddWalkRequestDto to Walk
        CreateMap<AddWalkRequestDto, Walk>()
            .ForMember(dest => dest.DifficultyId, opt => opt.MapFrom(src => src.DifficultyId))
            .ForMember(dest => dest.RegionId, opt => opt.MapFrom(src => src.RegionId));

        // Map Walk to WalkDto
        CreateMap<Walk, WalkDto>().ReverseMap();
    }
}