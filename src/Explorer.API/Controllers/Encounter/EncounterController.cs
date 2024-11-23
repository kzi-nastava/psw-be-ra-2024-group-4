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
            return null;
        }
    }
}