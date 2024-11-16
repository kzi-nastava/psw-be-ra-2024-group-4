using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Tours.API.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author
{
    [Authorize(Policy = "authorPolicy")]
    [Route("api/author/problem")]
    public class ProblemControllerTourist : BaseApiController
    {
        private readonly IProblemService _problemService;

        public ProblemControllerTourist(IProblemService problemService)
        {
            _problemService = problemService;
        }

        [HttpPost("postComment")]
        public ActionResult<ProblemDTO> PostComment([FromBody] ProblemCommentDto commentDto)
        {
            var result = _problemService.PostComment(commentDto);
            return CreateResponse(result);
        }
        [HttpGet("find/{id:long}")]
        public ActionResult<ProblemDTO> GetById(long id)
        {
            var result = _problemService.GetById(id);
            return CreateResponse(result);
        }
    }
}
