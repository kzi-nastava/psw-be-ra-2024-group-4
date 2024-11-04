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
        private readonly ICommentService commentService;
        private readonly IMapper mapper;
        public PostService(IPostRepository repository, ICommentService commentService, IMapper mapper) : base(mapper)
        {
            this.repository = repository;
            this.commentService = commentService;
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

        public Result AddComment(long postId, CommentDto commentDto)
        {
            try
            {
                var post = repository.Get(postId);
                if (post == null)
                {
                    return Result.Fail("Post sa datim ID-jem nije pronađen.");
                }

                var comment = mapper.Map<Comment>(commentDto);
                post.AddComment(comment);
                repository.Update(post);

                return Result.Ok();
            }
            catch (InvalidOperationException ex)
            {
                return Result.Fail("Nije moguće dodati komentar zbog neispravne operacije: " + ex.Message);
            }
            catch (Exception e)
            {
                return Result.Fail("Došlo je do neočekivane greške: " + e.Message);
            }
        }

        public Result DeleteCommentFromPost(long postId, long commentId)
        {
            try
            {
                var post = repository.Get(postId);
                post.DeleteComment(commentId);
                repository.Update(post);
                return Result.Ok();
            }
            catch (Exception e)
            {
                return Result.Fail(e.Message);
            }
        }

        public Result UpdateCommentInPost(long postId, CommentDto updatedCommentDto)
        {
            try
            {
                var post = repository.Get(postId);
                if (post == null)
                {
                    return Result.Fail("Post sa datim ID-jem nije pronađen.");
                }

                var comment = post.Comments.FirstOrDefault(c => c.Id == updatedCommentDto.Id);
                if (comment == null)
                {
                    return Result.Fail("Komentar sa datim ID-jem nije pronađen u postu.");
                }

                mapper.Map(updatedCommentDto, comment);
                repository.Update(post);

                return Result.Ok();
            }
            catch (Exception e)
            {
                return Result.Fail("Došlo je do greške: " + e.Message);
            }
        }


        public Result<PagedResult<CommentDto>> GetCommentsForPost(int postId, int page, int pageSize)
        {
            Post post = repository.Get(postId);
            if (post == null)
                return Result.Fail("Post not found");
            var pagedComments = post.GetAll();
            var commentDtos = pagedComments.Select(c => mapper.Map<CommentDto>(c)).ToList();
            var pagedResult = new PagedResult<CommentDto>(commentDtos, post.Comments.Count);
            return Result.Ok(pagedResult);
        }

    }
}


