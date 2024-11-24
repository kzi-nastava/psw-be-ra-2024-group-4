﻿using Explorer.Encounter.API.Dtos.Explorer.Encounters.API.Dtos;
using Explorer.Encounter.API.Public;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Encounter
{
    [Route("api/encounters")]
    public class EncounterController : BaseApiController
    {
        private readonly IEncounterService _encounterService;
        public EncounterController(IEncounterService encounterService) 
        {
            _encounterService = encounterService;
        }

        [HttpPost("create")]
        public Result<EncounterDto> Create([FromBody] EncounterDto encounter)
        {
            return _encounterService.CreateEncounter(encounter);
        }

        [HttpGet("radius")]
        public Result<BuildingBlocks.Core.UseCases.PagedResult<EncounterDto>> 
            GetInRadius([FromQuery] double radius, [FromQuery] double lat, [FromQuery] double lon)
        {
            return _encounterService.GetInRadius(radius, lat, lon);
        }

        [HttpPost("{id:long}/complete")]
        public ActionResult<EncounterDto> Complete(long id)
        {
            long userId = int.Parse(HttpContext.User.Claims.First(i => i.Type.Equals("id", StringComparison.OrdinalIgnoreCase)).Value);
            var result = _encounterService.CompleteEncounter(userId, id);
            return CreateResponse(result);
        }
    }
}