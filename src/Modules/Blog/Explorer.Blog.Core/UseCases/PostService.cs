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
    public class PostService : BaseService<PostDto, Post>, IPostService
    {
        private readonly IPostRepository repository;
        public PostService(IPostRepository repository,IMapper mapper) : base(mapper)
        {
            this.repository = repository;
        }

        public Result<PostDto> Create(PostDto post)
        {
            try
            {
                var result = repository.Create(MapToDomain(post));
                return MapToDto(result);
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

        public Result Delete(int id)
        {
            try
            {
                repository.Delete(id);
                return Result.Ok();
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        public Result<PostDto> Get(int id)
        {
            try
            {
                var result = repository.Get(id);
                return MapToDto(result);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        public Result<PagedResult<PostDto>> GetPaged(int page, int pageSize)
        {
            var result = repository.GetPaged(page, pageSize);
            return MapToDto(result);
        }

        public Result<PostDto> Update(PostDto post)
        {
            try
            {
                var result = repository.Update(MapToDomain(post));
                return MapToDto(result);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }
    }
}
