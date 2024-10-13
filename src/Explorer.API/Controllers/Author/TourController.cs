﻿using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author
{
    [Authorize(Policy = "authorPolicy")]
    [Route("api/author/tour")]
    public class TourController : BaseApiController
    {
        private readonly ITourService _tourService;

        public TourController(ITourService tourService)
        {
            _tourService = tourService;
        }

        [HttpPost]
        public ActionResult<TourDto> Create([FromBody] TourDto tour)
        {
            var result = _tourService.Create(tour);
            return CreateResponse(result);
        }

        [HttpGet("{id:int}")]
        public ActionResult<List<EquipmentDto>> GetAllByUserId(int id)
        {
            var result = _tourService.GetByUserId(id);
            return CreateResponse(result);
        }

        [HttpPost("{tourId:int}/equipment/{equipmentId:int}/add")]
        public ActionResult<TourDto> AddEquipmentToTour([FromBody] TourDto tour, int equipmentId)
        {
            var result = _tourService.AddEquipmentToTour(tour, equipmentId);
            return CreateResponse(result);
        }

        [HttpPut("{tourId:int}/equipment/{equipmentId:int}/remove")]
        public ActionResult<TourDto> RemoveEquipmentFromTour([FromBody] TourDto tour, int equipmentId)
        {
            var result = _tourService.RemoveEquipmentFromTour(tour, equipmentId);
            return CreateResponse(result);
        }
    }
}
