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
    public class RatingController: BaseApiController
    {
        private readonly IPostService _postService;

        public RatingController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpPost]
        public ActionResult<PostDto> AddRating(long postId,long userId,int value)
        {
            var result=_postService.AddRating(postId,userId,value);
            return CreateResponse(result);
        }

        [HttpDelete]
        public ActionResult<PostDto> DeleteRating(long postId,long userId) 
        {
            var result = _postService.DeleteRating(postId, userId);
            return CreateResponse(result);
        }
    }
}
