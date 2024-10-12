using AutoMapper;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.Domain;

namespace Explorer.Tours.Core.Mappers;

public class KeyPointProfile : Profile
{
    public KeyPointProfile()
    {

        CreateMap<KeyPointDto, KeyPoint>().ReverseMap();
    }
}
