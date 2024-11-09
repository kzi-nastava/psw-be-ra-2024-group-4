﻿using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Tours.API.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Administrator.Administration
{
    [Authorize(Policy = "administratorPolicy")]
    [Route("api/problem")]
    public class ProblemControllerAdmin : BaseApiController
    {
        private readonly IProblemService _problemService;

        public ProblemControllerAdmin(IProblemService problemService)
        {
            _problemService = problemService;
        }

        [HttpGet]

        public ActionResult<PagedResult<ProblemDTO>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _problemService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }


        [HttpPut("close/{id:int}")]
        public ActionResult<ProblemDTO> UpdateActiveStatus(int id, [FromBody] bool isActive)
        {
            var result = _problemService.UpdateActiveStatus(id, isActive);
            return CreateResponse(result);
        }

        [HttpDelete("admin/{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _problemService.Delete(id);
            return CreateResponse(result);
        }

    }  
}
