﻿using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.TourAuthoring;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/tour")]
    public class TourController : BaseApiController
    {
        private readonly ITourService _tourService;

        public TourController(ITourService tourService)
        {
            _tourService = tourService;
        }

        [HttpGet("getById/{id:long}")]
        public ActionResult<TourDto> GetById(long id)
        {
            var result = _tourService.GetById(id);
            return CreateResponse(result);
        }

        [HttpGet("getByTourId/{id:long}")]
        public ActionResult<TourDto> GetTourById(long id)
        {
            var result = _tourService.GetTourById(id);
            return CreateResponse(result);
        }

        [HttpGet("getPublished")]
        public ActionResult<PagedResult<TourDto>> GetPublished()
        {
            var result = _tourService.GetPublised();
            return CreateResponse(result);
        }
    }
}