using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.BuildingBlocks.Core.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.BlogFeedback
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/blogfeedback/comment")]
    public class CommentController : BaseApiController
    {
        private readonly ICommentService _commentService;
        private readonly IPostService _postService;

        public CommentController(ICommentService commentService, IPostService postService)
        {
            _commentService = commentService;
            _postService = postService;
        }

        [HttpGet]
        public ActionResult<PagedResult<CommentDto>> GetAll([FromQuery] int id, [FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _commentService.GetPaged(id,page, pageSize);
            return CreateResponse(result);
        }
        [HttpGet("posts")]
        public ActionResult<PagedResult<PostDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _postService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }
        [HttpPost]
        public ActionResult<CommentDto> Create([FromBody] CommentDto comment)
        {
            var result = _commentService.Create(comment);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<CommentDto> Update([FromBody] CommentDto comment)
        {
            var result = _commentService.Update(comment);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _commentService.Delete(id);
            return CreateResponse(result);
        }

        [HttpPost("{postId}")]
        public ActionResult<PostDto> AddCommentToPost(long postId, [FromBody] CommentDto commentDto)
        {
            var result = _postService.AddComment(postId, commentDto);
            if (result.IsFailed)
                return BadRequest(result.Errors);
            return CreateResponse(result);
        }

        [HttpDelete("{postId}/{commentId}")]
        public ActionResult<PostDto> DeleteCommentFromPost(long postId, long commentId)
        {
            var result = _postService.DeleteCommentFromPost(postId, commentId);
            if (result.IsFailed)
                return BadRequest(result.Errors);
            return CreateResponse(result);
        }
        [HttpPut("{postId}")]
        public ActionResult<PostDto> UpdateCommentInPost(long postId,[FromBody] CommentDto updatedCommentDto)
        {
            var result = _postService.UpdateCommentInPost(postId, updatedCommentDto);
            if (result.IsFailed)
                return BadRequest(result.Errors);
            return CreateResponse(result);
        }
    }
}
