using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist {


    [Route("api/tourist/preference")]
    [ApiController]
    public class TourPreferenceController : BaseApiController {
        private readonly ITourPreferenceService _tourPreferenceService;

        public TourPreferenceController(ITourPreferenceService tourPreferenceService) {
            _tourPreferenceService = tourPreferenceService;
        }

        // GET
        [HttpGet("{touristId}")]
        [Authorize(Policy = "TouristPolicy")]
        public async Task<IActionResult> GetTourPreference(int touristId) {
            var result = _tourPreferenceService.GetTourPreference(touristId);
            if (result.IsFailed) return NotFound(result.Errors);
            return Ok(result.Value);
        }

        // PUT
        [HttpPut("{touristId}")]
        [Authorize(Policy = "touristPolicy")]
        public async Task<IActionResult> UpdateTourPreference(int touristId, [FromBody] TourPreferenceDto tourPreference) {
            var result = _tourPreferenceService.UpdateTourPreference(touristId, tourPreference);
            if (result.IsFailed) return BadRequest(result.Errors);
            return NoContent();
        }
    }
}
