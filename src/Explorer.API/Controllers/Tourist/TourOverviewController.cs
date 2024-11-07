using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.TourAuthoring;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tour/tourOverview")]
    public class TourOverviewController : BaseApiController
    {
        private readonly ITourOverviewService _tourOverviewService;

        public TourOverviewController(ITourOverviewService tourOverviewService)
        {
            _tourOverviewService = tourOverviewService;
        }

        [HttpGet]
        public ActionResult<PagedResult<TourOverviewDto>> GetAllWithoutreviews([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _tourOverviewService.GetAllWithoutReviews(page, pageSize);
            return CreateResponse(result);
        }

        [HttpGet("{id:int}")]
        public ActionResult<PagedResult<TourOverviewDto>> GetAllByTourId(long id, [FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _tourOverviewService.GetAllByTourId(page, pageSize, id);
            return CreateResponse(result);
        }

        [HttpGet("average/{id:int}")]
        public ActionResult<TourOverviewDto> GetAveragerating(long id)
        {
            var result = _tourOverviewService.GetAverageRating(id);
            return CreateResponse(result);
        }

        [HttpGet("search/{longitude:long};{latitude:long};{distance:int}")]
        public ActionResult<PagedResult<TourOverviewDto>> GetByCoordinated(double latitude, double longitude, int distance, [FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _tourOverviewService.GetByCoordinated(latitude, longitude, distance, page, pageSize);
            return CreateResponse(result);
        }

    }
}
