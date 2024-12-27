using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/clubTour")]
    public class ClubTourController : BaseApiController
    {

        private readonly IClubTourService _clubTourService;

        public ClubTourController(IClubTourService clubTourService)
        {
            _clubTourService = clubTourService;
        }


        [HttpGet]
        public ActionResult<PagedResult<ClubTourDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _clubTourService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<ClubTourDto> Create([FromBody] ClubTourDto clubTour)
        {
            
            var result = _clubTourService.Create(clubTour);
            return CreateResponse(result);
        }
        [HttpPut("{id:int}")]
        public ActionResult<ClubTourDto> Update([FromBody] ClubTourDto clubTour)
        {
            var result = _clubTourService.Update(clubTour);
            return CreateResponse(result);
        }

    }
}
