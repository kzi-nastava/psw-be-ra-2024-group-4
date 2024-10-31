using AutoMapper;
using Explorer.Modules.Core.Domain;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.Domain;
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
        CreateMap<OrderItemDto, OrderItem>().ReverseMap();
        CreateMap<ShoppingCartDto, ShoppingCart>().ReverseMap();
    }
}