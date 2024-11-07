using AutoMapper;
using Explorer.Modules.Core.Domain;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.TourExecutionDtos;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.TourExecutions;
using Explorer.Tours.Core.Domain.Tours;

namespace Explorer.Tours.Core.Mappers;

public class ToursProfile : Profile
{
    public ToursProfile()
    {
        CreateMap<EquipmentDto, Equipment>().ReverseMap();
        CreateMap<TourPreferenceDto, TourPreference>().ReverseMap();
        CreateMap<KeyPointDto, KeyPoint>().ReverseMap();
        CreateMap<TourDto, Tour>().ReverseMap();
        CreateMap<ObjectDTO, Explorer.Tours.Core.Domain.Object>().ReverseMap();
        CreateMap<TourReviewDto, TourReview>().ReverseMap();
        CreateMap<RegistrationRequestDto, RegistrationRequest>().ReverseMap();
        CreateMap<CompletedKeyPointDto, CompletedKeyPoint>().ReverseMap();
        CreateMap<PositionSimulatorDto, PositionSimulator>().ReverseMap();
        CreateMap<TourExecutionDto, TourExecution>().ReverseMap();
        CreateMap<TourExecutionDto, TourExecution>().IncludeAllDerived() //sluzi da budemo sigurni da lista keyPoints u dto bude ista kao i u klasi
            .ForMember(dest => dest.CompletedKeys, opt => 
            opt.MapFrom(src => src.CompletedKeys.Select((completedKey) => 
            new CompletedKeyPoint(completedKey.KeyPointId, completedKey.CompletedTime))));
    }
}