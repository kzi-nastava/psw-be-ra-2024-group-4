using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.API.Dtos;

namespace Explorer.API.Controllers.Person2
{
    [Route("api/person/appReview")]
    public class AppReviewController: BaseApiController
    {
        private readonly IAppReviewService _appReviewService;

        public AppReviewController(IAppReviewService appReviewService)
        {
            _appReviewService = appReviewService;
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
