using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.Blog.Core.UseCases;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.TourAuthoring;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers
{
    public class FrontPageController: BaseApiController
    {
        private readonly ITourOverviewService _tourOverviewService;
        private readonly IPostService _postService;

        public FrontPageController(ITourOverviewService tourOverviewService, IPostService postService)
        {
            _tourOverviewService = tourOverviewService;
            _postService = postService;
        }

        [HttpGet("api/frontPage/tours")]
        public ActionResult<PagedResult<TourOverviewDto>> GetAllWithoutreviews([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _tourOverviewService.GetAllWithoutReviews(page, pageSize);
            return CreateResponse(result);
        }

        [HttpGet("api/frontPage/posts")]
        public ActionResult<PagedResult<PostDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _postService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }
    }
}
