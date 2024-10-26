using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Tours.API.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/problem")]
    public class ProblemControllerTourist : BaseApiController
    {
        private readonly IProblemService _problemService;
        
        public ProblemControllerTourist(IProblemService problemService)
        {
            _problemService = problemService;
        }

        [HttpGet("byTourist/{userId:long}")]
        public ActionResult<List<ProblemDTO>> GetByTouristId(long userId)
        {
            var result = _problemService.GetByTouristId(userId);
            return CreateResponse(result);
        }

        [HttpGet("byTour/{tourId:long}")]
        public ActionResult<List<ProblemDTO>> GetByTourId(long tourId)
        {
            var result = _problemService.GetByTourId(tourId);
            return CreateResponse(result);
        }


        [HttpPost]
        public ActionResult<ProblemDTO> Create([FromBody] ProblemDTO problemDTO)
        {
            var result = _problemService.Create(problemDTO);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<ProblemDTO> Update([FromBody] ProblemDTO problemDTO)
        {
            var result = _problemService.Update(problemDTO);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _problemService.Delete(id);
            return CreateResponse(result);
        }
    }
}
