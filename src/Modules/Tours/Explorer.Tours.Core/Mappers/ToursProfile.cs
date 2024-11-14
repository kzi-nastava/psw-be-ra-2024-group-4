using AutoMapper;
using Explorer.Modules.Core.Domain;
using Explorer.Tours.API.Dtos.TourExecutionDtos;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.Domain.TourExecutions;
using Explorer.Tours.Core.Domain.Tours;
using Explorer.Tours.Core.Domain;

public class ToursProfile : Profile
{
    public ToursProfile()
    {
        CreateMap<EquipmentDto, Equipment>().ReverseMap();
        CreateMap<TourPreferenceDto, TourPreference>().ReverseMap();
        CreateMap<KeyPointDto, KeyPoint>().ReverseMap();

        // Mapping TourDto to Tour with specific handling for TourDurations
        CreateMap<TourDto, Tour>()
            .ForMember(dest => dest.Durations, opt => opt.MapFrom(src =>
                src.Durations
                    .Where(t => t.Duration > 0) // Only map positive durations
                    .Select(t => new TourDuration(t.Transportation, t.Duration)) ?? new List<TourDuration>()))
            .ReverseMap()
            .ForMember(dest => dest.Durations, opt => opt.MapFrom(src =>
                src.Durations.Select(t => new TourDurationDTO(t.Transportation, t.Duration))));

        CreateMap<ObjectDTO, Explorer.Tours.Core.Domain.Object>().ReverseMap();
        CreateMap<TourReviewDto, TourReview>().ReverseMap();
        CreateMap<CompletedKeyPointDto, CompletedKeyPoint>().ReverseMap();
        CreateMap<PositionSimulatorDto, PositionSimulator>().ReverseMap();
        CreateMap<TourExecutionDto, TourExecution>().ReverseMap();
        CreateMap<TourExecutionDto, TourExecution>()
            .IncludeAllDerived()
            .ForMember(dest => dest.CompletedKeys, opt =>
                opt.MapFrom(src => src.CompletedKeys.Select(completedKey =>
                    new CompletedKeyPoint(completedKey.KeyPointId, completedKey.CompletedTime))));
    }
}
