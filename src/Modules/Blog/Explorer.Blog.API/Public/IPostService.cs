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
        Result AddComment(long postId, CommentDto commentDto);
        Result DeleteCommentFromPost(long postId, long commentId);
    
        Result<PagedResult<CommentDto>> GetCommentsForPost(int postId, int page, int pageSize);
        Result<PostDto> Get(int id);
        Result UpdateCommentInPost(long postId, CommentDto updatedCommentDto);
    }
}
