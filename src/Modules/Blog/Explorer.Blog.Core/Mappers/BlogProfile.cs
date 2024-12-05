using AutoMapper;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.Core.Domain;
using Explorer.Blog.Core.Domain.Posts;

namespace Explorer.Blog.Core.Mappers;

public class BlogProfile : Profile
{
    public BlogProfile()
    {

        CreateMap<CommentDto, Comment>().ReverseMap();
        CreateMap<RatingDto, Rating>();
        CreateMap<PostDto, Post>()
            .ForMember(dest => dest.Ratings, opt => opt.MapFrom(src => src.Ratings.Select(r => new Rating(r.UserId, r.Value, r.CreatedAt))))
            .ReverseMap()
            .ForMember(dest => dest.Ratings, opt => opt.MapFrom(src => src.Ratings.Select(r => new RatingDto { UserId = r.UserId, Value = r.Value, CreatedAt = r.CreatedAt })));
        CreateMap<AdvertisementDto, Advertisement>();
    }

}
