using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.BuildingBlocks.Core.UseCases;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.BlogFeedback
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/blogfeedback/rating")]
    public class RatingController : BaseApiController
    {
        private readonly IPostService _postService;

        public RatingController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpPost("{id:int}")]
        public ActionResult<PostDto> AddRating(int id, [FromBody] RatingDto ratingDto)
        {
            var result=_postService.AddRating(id,ratingDto.UserId,ratingDto.Value);
            return CreateResponse(result);
        }

        [HttpPut("{postId:int}")]
        public ActionResult<PostDto> UpdateRating(long postId,[FromBody] RatingDto ratingDto) 
        {
            var result = _postService.UpdateRating(postId,ratingDto.UserId,ratingDto.Value);
            return CreateResponse(result);
        }
    }
}
