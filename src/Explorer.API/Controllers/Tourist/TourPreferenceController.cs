using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist {

    [ApiController]
    [Route("api/tourist/preference")]
    public class TourPreferenceController : BaseApiController {
        private readonly ITourPreferenceService _tourPreferenceService;

        public TourPreferenceController(ITourPreferenceService tourPreferenceService) {
            _tourPreferenceService = tourPreferenceService;
        }

        //GETALL
        [HttpGet("preferences")]
        //[Authorize(Policy = "touristPolicy")]
        public async Task<ActionResult<List<TourPreferenceDto>>> GetAllPreferences() {
            var preferences = await _tourPreferenceService.GetAllPreferencesAsync();
            if (preferences == null || !preferences.Any()) {
                return NotFound("No tour preferences found");
            }
            return Ok(preferences);
        }

        // GET
        [HttpGet("{touristId}")]
        [Authorize(Policy = "touristPolicy")]
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

        //POST
        [HttpPost("{touristId}")]
        [Authorize(Policy = "touristPolicy")]
        public async Task<IActionResult> AddTourPreference(int touristId, [FromBody] TourPreferenceDto preference) {
            var result = _tourPreferenceService.AddTourPreference(touristId, preference);
            if (result.IsFailed) return BadRequest(result.Errors);
            return NoContent();
        }
    }
}
