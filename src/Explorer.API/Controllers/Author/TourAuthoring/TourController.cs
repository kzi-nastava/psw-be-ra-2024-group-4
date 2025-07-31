﻿using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.TourAuthoring;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Explorer.API.Controllers.Author.TourAuthoring
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
        public ActionResult<List<TourDto>> GetAllByUserId(int id)
        {
            var result = _tourService.GetByUserId(id);
            return CreateResponse(result);
        }


        [HttpGet("tourEquipment/{tourId:int}")]
        public ActionResult GetEquipment(int tourId)
        {
            var result = _tourService.GetEquipment(tourId);
            return CreateResponse(result);
        }

        [HttpPost("{tourId:int}/equipment")]
        public ActionResult AddEquipmentToTour(int tourId,[FromBody] List<long> equipmentIds)
        {
            var result = _tourService.AddEquipmentToTour(tourId, equipmentIds);
            return CreateResponse(result);
        }


        [HttpDelete("{tourId:int}/equipment/{equipmentId:int}")]
        public ActionResult RemoveEquipmentFromTour(int tourId, int equipmentId)
        {
            var result = _tourService.RemoveEquipmentFromTour(tourId, equipmentId);
            return CreateResponse(result);
        }

        [HttpPut("archive/{tourId:int}")]
        public ActionResult<TourDto> Archive(long tourId)
        {
            var result = _tourService.Archive(tourId);
            return CreateResponse(result);
        }

        [HttpPut("publish/{tourId:int}")]
        public ActionResult<TourDto> Publish(long tourId)
        {
            var result = _tourService.Publish(tourId);
            return CreateResponse(result);
        }


        [HttpPut("reactivate/{tourId:int}")]
        public ActionResult<TourDto> Reactivate(long tourId)
        {
            var result = _tourService.Reactivate(tourId);
            return CreateResponse(result);
        }

        [HttpPut("updateDistance/{tourId:int}")]
        public ActionResult<TourDto> UpdateDistance(long tourId, [FromBody] double distance)
        {
            var result = _tourService.UpdateDistance(tourId, distance);
            return CreateResponse(result);
        }

        [HttpGet("getById/{id:long}")]
        public ActionResult<TourDto> GetById(long id)
        {
            var result = _tourService.GetById(id);
            return CreateResponse(result);
        }
        [HttpGet("getByTourId/{id:long}")]
        public ActionResult<TourDto> GetTourById(long id)
        {
            var result = _tourService.GetTourById(id);
            return CreateResponse(result);
        }

    }
}
