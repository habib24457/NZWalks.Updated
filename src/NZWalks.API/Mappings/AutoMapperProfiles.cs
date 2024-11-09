using AutoMapper;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Mappings
{
    /*Instructions for me: 
     * Basic structure of automapper:(When all the properties/var names are same in the source and destination domain)
	 * Packages needs to install: 1.Automapper , 2.Automapper dependency injection
	 * CreateMap<SourceModel, DestinationModel>();
	 * When we need to map again from the same source and destination but in reverse
	 * CreateMap<SourceModel, DestinationModel>().Reverse();
	 * 
	 * When Source and Destination models doesn't have the similar properties
	 * There are methods for that as well: follow automapper docs
	 */
    public class AutoMapperProfiles : Profile
	{
        /*Remember: 
		 * While creating a map, the format is: CreateMap<SourceModel, DestinationModel>();
		 * But inside the controller the format is: mapper.Map<Destination>(source);
		 */
        public AutoMapperProfiles()
		{
			CreateMap<Region, RegionDTO>().ReverseMap();
			CreateMap<AddRegionRequestDto, Region>().ReverseMap();
            CreateMap<UpdateRegionRequestDto, Region>().ReverseMap();

			CreateMap<Walk, WalkDto>().ReverseMap();
			CreateMap<AddWalkRequestDto, Walk>().ReverseMap();
			CreateMap<Difficulty, DifficultyDto>().ReverseMap();

			CreateMap<UpdateWalkRequestDto, Walk>().ReverseMap();
        }
    }
}

