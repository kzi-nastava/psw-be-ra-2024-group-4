using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounter.API.Dtos.Explorer.Encounters.API.Dtos;
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
        public Result<PagedResult<EncounterDto>> 
            GetInRadius([FromQuery] double radius, [FromQuery] double lat, [FromQuery] double lon)
        {
            return _encounterService.GetInRadius(radius, lat, lon);
        }

        [HttpGet]
        public Result<EncounterDto> GetByLatLong([FromQuery] double latitude, [FromQuery] double longitude)
        {
            return _encounterService.GetByLatLong(latitude, longitude);
        }
    }
}