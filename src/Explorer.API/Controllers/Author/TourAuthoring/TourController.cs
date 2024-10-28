using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.TourAuthoring;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost("{tourId:int}/equipment/{equipmentId:int}")]
        public ActionResult AddEquipmentToTour(int tourId, int equipmentId)
        {
            var result = _tourService.AddEquipmentToTour(tourId, equipmentId);
            return CreateResponse(result);
        }


        [HttpDelete("{tourId:int}/equipment/{equipmentId:int}")]
        public ActionResult RemoveEquipmentFromTour(int tourId, int equipmentId)
        {
            var result = _tourService.RemoveEquipmentFromTour(tourId, equipmentId);
            return CreateResponse(result);
        }

      
    }
}
