using AutoMapper;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.Blog.Core.Domain.Posts;
using Explorer.Blog.Core.Domain.RepositoryInterfaces;
using Explorer.BuildingBlocks.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.Core.UseCases
{
    public class PostService : CrudService<PostDto, Post>,IPostService
    {
        private readonly IPostRepository postRepository; //pomocu ovoga ce se pozivati metode dodatne mimo crud osnovnih kada se definisu PostDatabaseRepo
        public PostService(IPostRepository postRepository,IMapper mapper) : base(postRepository,mapper) 
        { 
            this.postRepository = postRepository;
        }
    }
}
