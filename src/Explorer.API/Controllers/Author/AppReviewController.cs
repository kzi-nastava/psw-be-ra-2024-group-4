﻿
using Microsoft.AspNetCore.Mvc;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.API.Dtos;
using Microsoft.AspNetCore.Authorization;

namespace Explorer.API.Controllers.Author
{
    [Authorize(Policy = "authorPolicy")]
    [Route("api/author/appReview")]
    public class AppReviewController : BaseApiController
    {
        private readonly IAppReviewService _appReviewService;

        public AppReviewController(IAppReviewService appReviewService)
        {
            _appReviewService = appReviewService;
        }

        [HttpGet("{id:int}")]
        public ActionResult<AppReviewDto> Get(int id)
        {
            var result = _appReviewService.Get(id);
            return CreateResponse(result);
        }


        [HttpPost]
        public ActionResult<AppReviewDto> Create([FromBody] AppReviewDto appReview)
        {
            var result = _appReviewService.Create(appReview);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<AppReviewDto> Update([FromBody] AppReviewDto appReview)
        {
            var result = _appReviewService.Update(appReview);
            return CreateResponse(result);
        }
    }
}

