using Explorer.Blog.API.Dtos;
using Explorer.BuildingBlocks.Core.UseCases;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.API.Public
{
    public interface IPostService
    {
        Result<PagedResult<PostDto>> GetPaged(int page, int pageSize);

        Result<PostDto> Create(PostDto post);
        Result<PostDto> Update(PostDto post);
        Result Delete(int id);
        Result<PostDto> AddComment(long postId, CommentDto commentDto);
        Result<PostDto> DeleteCommentFromPost(long postId, long commentId);
        Result<PostDto> Get(int id);
        Result<PostDto> UpdateCommentInPost(long postId, CommentDto updatedCommentDto);
        Result<PostDto> AddRating(long postId,long userId,int value);
        Result<PostDto> UpdateRating(long postId, long userId,int newValue);

        Result<PostDto> PublishPost(long postId);
    }
}
