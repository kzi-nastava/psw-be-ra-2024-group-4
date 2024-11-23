using AutoMapper;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.Blog.Core.Domain;
using Explorer.Blog.Core.Domain.Posts;
using Explorer.Blog.Core.Domain.RepositoryInterfaces;
using Explorer.Blog.Core.UseCases.Administration;
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
        private readonly IMapper mapper;
        public PostService(IPostRepository repository, IMapper mapper) : base(mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
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

        public Result<PostDto> AddComment(long postId, CommentDto commentDto)
        {
            try
            {
                var post = repository.Get(postId);
                if (post == null) return Result.Fail("Post sa datim ID-jem nije pronađen.");
                var comment = mapper.Map<Comment>(commentDto);
                post.AddComment(comment);
                post.UpdateStatus();
                var result=repository.Update(post);
                return MapToDto(result);
            }
            catch (Exception e)
            {
                return Result.Fail("Došlo je do neočekivane greške: " + e.Message);
            }
        }

        public Result<PostDto> DeleteCommentFromPost(long postId, long commentId)
        {
            try
            {
                var post = repository.Get(postId);
                post.DeleteComment(commentId);
                post.UpdateStatus();
                var result=repository.Update(post);
                return MapToDto(result);
            }
            catch (Exception e)
            {
                return Result.Fail(e.Message);
            }
        }

        public Result<PostDto> UpdateCommentInPost(long postId, CommentDto updatedCommentDto)
        {
            try
            {
                var post = repository.Get(postId);
                if (post == null) return Result.Fail("Post sa datim ID-jem nije pronađen.");
                var comment = mapper.Map<Comment>(updatedCommentDto);
                post.UpdateComment(comment);
                var result=repository.Update(post);

                return MapToDto(result);
            }
            catch (Exception e)
            {
                return Result.Fail("Doslo je do greske: " + e.Message);
            }
        }

        public Result<PostDto> AddRating(long postId, long userId, int value)
        {
            try
            {
                var post = repository.Get(postId);
                if (post == null) return Result.Fail("Post with this id: "+  postId + "does not exist.");
                post.AddRating(value,userId);
                post.TotalRating();
                post.UpdateStatus();
                var result = repository.Update(post);
                return MapToDto(result); 
            }
            catch(Exception e)
            {
                return Result.Fail(e.Message);
            }
        }
        public Result<PostDto> UpdateRating(long postId, long userId, int newValue)
        {
            try
            {
                var post = repository.Get(postId);
                var existingRating = post.Ratings.FirstOrDefault(r => r.UserId == userId);
                if (existingRating != null) post.DeleteRating(userId);
                post.AddRating(newValue, userId);
                post.TotalRating();
                post.UpdateStatus();
                var result = repository.Update(post);
                return MapToDto(result);

            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
            catch (Exception e)
            {
                return Result.Fail(e.Message);
            }
        }

        public Result<PostDto> PublishPost(long postId)
        {
            try
            {
                var post = repository.Get(postId);
                post.Publish();
                var result = repository.Update(post);
                return MapToDto(result);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
            catch (Exception e)
            {
                return Result.Fail(e.Message);
            }
        }
    }
}


