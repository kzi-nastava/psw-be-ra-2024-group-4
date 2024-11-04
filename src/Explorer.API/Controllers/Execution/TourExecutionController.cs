using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.TourExecutionDtos;
using Explorer.Tours.API.Public.Execution;
using Explorer.Tours.API.Public.TourAuthoring;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.TourExecutions;
using Explorer.Tours.Core.UseCases.Execution;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Execution
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tour/execution")]
    [Route("api/tourist/execution")]

    public class TourExecutionController : BaseApiController
    {
        private readonly ITourExecutionService _executionService;

        public TourExecutionController(ITourExecutionService tourService)
        {
            _executionService = tourService;
        }

        [HttpPost]
        public ActionResult<TourExecutionDto> Create([FromBody] TourExecutionDto tour)
        {
            var result = _executionService.Create(tour);
            return CreateResponse(result);
        }


        [HttpPost("complete/{executionId:long}")]
        public ActionResult<TourExecutionDto> CompleteTourExecution(int executionId)
        {
            var result = _executionService.CompleteTourExecution(executionId);
            if (result == null)
            {
                return NotFound($"TourExecution with ID {executionId} not found.");
            }

            return CreateResponse(result);
        }

        [HttpPost("abandon/{executionId:long}")]
        public ActionResult<TourExecutionDto> AbandonTourExecution(int executionId)
        {
            var result = _executionService.AbandonTourExecution(executionId);
            if (result == null)
            {
                return NotFound($"TourExecution with ID {executionId} not found.");
            }

            return CreateResponse(result);
        }

        [HttpPut("tour/completeKeyPoint/{executionId:long}/{keyPointId:long}")]
        public ActionResult<TourExecutionDto> CompleteKeyPoint(long executionId, long keyPointId)
        {
            try
            {
                var result = _executionService.CompleteKeyPoint(executionId, keyPointId);

                return result.IsFailed
                    ? Conflict(new { message = result.Errors.First().Message })
                    : CreateResponse(result);
            }
            catch (ArgumentException ex) 
            {
                return BadRequest(new { message = ex.Message }); 
            }
            catch (Exception ex) 
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }


        [HttpGet("by_tour_and_tourist/{touristId:long}/{tourId:long}")]
        public ActionResult<TourExecutionDto> GetByTourAndTouristId(long touristId, long tourId)
        {
            var result = _executionService.GetByTourAndTouristId(touristId, tourId);
            if (result == null)
            {
                return NotFound("Tour execution not found for the specified tourist and tour.");
            }
            return CreateResponse(result);
        }


    }
}
