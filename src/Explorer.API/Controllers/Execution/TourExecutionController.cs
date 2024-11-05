using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.TourExecutionDtos;
using Explorer.Tours.API.Public.Execution;
using Explorer.Tours.API.Public.TourAuthoring;
using Explorer.Tours.Core.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Execution
{
    //[Authorize(Policy = "touristPolicy")]
    [Route("api/tour/execution")]
    public class TourExecutionController : BaseApiController
    {
        private readonly ITourExecutionService _executionService;

        public TourExecutionController(ITourExecutionService tourService)
        {
            _executionService = tourService;
        }

        [HttpPost]
        public ActionResult<TourDto> Create([FromBody] TourExecutionDto tour)
        {
            var result = _executionService.Create(tour);
            return CreateResponse(result);
        }


        [HttpPost("tour/complete/{executionId:int}")]
        public ActionResult CompleteTourExecution(int executionId)
        {
            var result = _executionService.CompleteTourExecution(executionId);
            if (result == null)
            {
                return NotFound($"TourExecution with ID {executionId} not found.");
            }

            return CreateResponse(result);
        }

        [HttpPost("tour/abandon/{executionId:int}")]
        public ActionResult<TourDto> AbandonTourExecution(int executionId)
        {
            var result = _executionService.AbandonTourExecution(executionId);
            if (result == null)
            {
                return NotFound($"TourExecution with ID {executionId} not found.");
            }

            return CreateResponse(result);
        }
    }
}
