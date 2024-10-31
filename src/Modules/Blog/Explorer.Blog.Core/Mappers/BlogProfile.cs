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
        CreateMap<PostDto, Post>().ReverseMap();
       
    }
}