using Explorer.Encounter.API.Dtos;
﻿using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounter.API.Dtos.Explorer.Encounters.API.Dtos;
using Explorer.Encounter.API.Public;
using Explorer.Stakeholders.API.Public;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Encounter
{
    [Route("api/encounters")]
    public class EncounterController : BaseApiController
    {
        private readonly IEncounterService _encounterService;
        private readonly IPersonService _personService;
        public EncounterController(IEncounterService encounterService, IPersonService personService) 
        {
            _encounterService = encounterService;
            _personService = personService;
        }

        [HttpPost("/create")]
        public Result<EncounterDto> Create([FromBody] EncounterDto encounter)
        {
            return _encounterService.CreateEncounter(encounter);
        }

        [HttpGet("radius")]
        public Result<PagedResult<EncounterDto>> 
            GetInRadius([FromQuery] double radius, [FromQuery] double lat, [FromQuery] double lon)
        {
            return _encounterService.GetInRadius(radius, lat, lon);
        }

        [HttpPost("{id:long}/activate")]
        public ActionResult<EncounterDto> Activate([FromBody] TouristPositionCreateDto position, long id)
        {
            long userId = int.Parse(HttpContext.User.Claims.First(i => i.Type.Equals("id", StringComparison.OrdinalIgnoreCase)).Value);
            var result = _encounterService.ActivateEncounter(userId, id, position.Longitude, position.Latitude);
            return CreateResponse(result);
        }

        [HttpPost("{id:long}/complete")]
        public ActionResult<EncounterDto> Complete(long id)
        {
            long userId = int.Parse(HttpContext.User.Claims
                .First(i => i.Type.Equals("id", StringComparison.OrdinalIgnoreCase)).Value);
            var result = _encounterService.CompleteEncounter(userId, id);

            if (!result.IsSuccess) return BadRequest("Encounter failed to complete!");

            var xp = result.Value.XP;
            var xpResult = _personService.AddXP(userId, xp);
            if (!xpResult.IsSuccess) return NotFound("Person not found!");

            return CreateResponse(result);
        }

        [HttpGet]
        public Result<EncounterDto> GetByLatLong([FromQuery] double latitude, [FromQuery] double longitude)
        {
            return _encounterService.GetByLatLong(latitude, longitude);
        }
    }
}
