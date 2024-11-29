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
        private readonly IImageService _imageService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public EncounterController(IEncounterService encounterService, IImageService imageService, IWebHostEnvironment webHostEnvironment, IPersonService personService) 
        {
            _encounterService = encounterService;
            _imageService = imageService;
            _webHostEnvironment = webHostEnvironment;
            _personService = personService;
        }

        [HttpPost("/create")]
        public Result<EncounterDto> Create([FromBody] EncounterDto encounter)
        {
            if (!string.IsNullOrEmpty(encounter.HiddenLocationData?.ImageBase64))
            {
                var imageData = Convert.FromBase64String(encounter.HiddenLocationData.ImageBase64.Split(',')[1]);
                var folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "encounters");


                encounter.HiddenLocationData.ImageUrl= _imageService.SaveImage(folderPath, imageData, "encounters");
            }

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
        [HttpPut("{id:long}/approve")]
        public IActionResult ApproveEncounter(long id) {
            try {
                _encounterService.ApproveEncounter(id);
                return Ok(new { Message = "Encounter approved successfully" });
            }
            catch (Exception e) {
                return BadRequest(new { Error = e.Message });
            }
        }

        [HttpPut("{id:long}/reject")]
        public IActionResult RejectEncounter(long id) {
            try {
                _encounterService.RejectEncounter(id);
                return Ok(new { Message = "Encounter rejected successfully" });
            }
            catch (Exception e) {
                return BadRequest(new { Error = e.Message });
            }
        }

        [HttpGet("pending")]
        public IActionResult GetPendingPaged() {
            var result = _encounterService.GetPendingRequest();
            if (result.IsSuccess) {
                return Ok(result.Value); // Map to HTTP 200 response with the data.
            }
            else {
                return BadRequest(new { Error = result.Errors }); // Map to HTTP 400 response with errors.
            }
        }

    }
}
