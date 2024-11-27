using Explorer.Encounter.API.Dtos;
﻿using Explorer.BuildingBlocks.Core.UseCases;
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
        private readonly IImageService _imageService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public EncounterController(IEncounterService encounterService, IImageService imageService, IWebHostEnvironment webHostEnvironment) 
        {
            _encounterService = encounterService;
            _imageService = imageService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost("create")]
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
            return CreateResponse(result);
        }

        [HttpGet]
        public Result<EncounterDto> GetByLatLong([FromQuery] double latitude, [FromQuery] double longitude)
        {
            return _encounterService.GetByLatLong(latitude, longitude);
        }
    }
}
