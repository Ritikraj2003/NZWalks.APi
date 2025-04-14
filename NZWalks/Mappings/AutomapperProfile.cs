using AutoMapper;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Mappings
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Regions, ResgionDto>().ReverseMap();
            CreateMap<AddRegionRequestDto, Regions>().ReverseMap();
            CreateMap<updateRegionDto, Regions>().ReverseMap();


            CreateMap<AddWalkRequestDto, Walks>().ReverseMap();
            CreateMap<Walks, WalkDto>().ReverseMap();
            CreateMap<Difficulty,DificultyDto>().ReverseMap();
            CreateMap<UpdateWalkDto, Walks>().ReverseMap();

            
           
        }
    }
}
