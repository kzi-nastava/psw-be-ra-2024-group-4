using AutoMapper;
using Explorer.Encounter.API.Dtos.Explorer.Encounters.API.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounter.Core.Mappers
{
    public class EncounterProfile : Profile
    {
            public EncounterProfile()
        {
            // Main mapping between EncounterDto and Encounter
            CreateMap<EncounterDto, Explorer.Encounter.Core.Domain.Encounter>()
                .ForMember(dest => dest.SocialData, opt => opt.MapFrom(src => src.SocialData))
                .ForMember(dest => dest.HiddenLocationData, opt => opt.MapFrom(src => src.HiddenLocationData))
                .ForMember(dest => dest.MiscData, opt => opt.MapFrom(src => src.MiscData))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Instances, opt => opt.MapFrom(src => src.Instances)) 
                .ReverseMap()
                .ForMember(dest => dest.SocialData, opt => opt.MapFrom(src => src.SocialData))
                .ForMember(dest => dest.HiddenLocationData, opt => opt.MapFrom(src => src.HiddenLocationData))
                .ForMember(dest => dest.MiscData, opt => opt.MapFrom(src => src.MiscData))
                .ForMember(dest => dest.Instances, opt => opt.MapFrom(src => src.Instances));   


            // Nested data mappings
            CreateMap<Explorer.Encounter.API.Dtos.SocialDataDto, Explorer.Encounter.Core.Domain.SocialData>()
                .ReverseMap();

            CreateMap<Explorer.Encounter.API.Dtos.HiddenLocationDataDto, Explorer.Encounter.Core.Domain.HiddenLocationData>()
                .ReverseMap();

            CreateMap<Explorer.Encounter.API.Dtos.MiscDataDto, Explorer.Encounter.Core.Domain.MiscData>()
                .ReverseMap();

            CreateMap<Explorer.Encounter.Core.Domain.EncounterInstance, Explorer.Encounter.API.Dtos.EncounterInstanceDto>()
                .ReverseMap();

        }
    }
}
