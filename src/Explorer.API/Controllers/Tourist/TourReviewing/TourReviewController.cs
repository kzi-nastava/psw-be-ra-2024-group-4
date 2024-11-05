﻿using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Modules.Core.Domain;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.TourReviewing;
using Explorer.Tours.Core.UseCases.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.TourReviewing
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourReviewing/tourReview")]
    public class TourReviewController : BaseApiController
    {
        private readonly ITourReviewService _tourReviewService;

        public TourReviewController(ITourReviewService tourReviewService)
        {
            _tourReviewService = tourReviewService;
        }

        [HttpGet]
        public ActionResult<PagedResult<TourReviewDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _tourReviewService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpGet("byTourist/{userId:long}")]
        public ActionResult<PagedResult<TourReviewDto>> GetAllFromTour([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _tourReviewService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpGet("byTourist/{userId:long}")]
        public ActionResult<PagedResult<TourReviewDto>> GetAllFromTourist([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _tourReviewService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<TourReviewDto> Create([FromBody] TourReviewDto tourReview)
        {
            var result = _tourReviewService.Create(tourReview);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<TourReviewDto> Update([FromBody] TourReviewDto tourReview)
        {
            var result = _tourReviewService.Update(tourReview);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _tourReviewService.Delete(id);
            return CreateResponse(result);
        }
    }
}
