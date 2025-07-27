using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.TourAuthoring.ObjectAddition;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/object")]
    public class ObjectController : BaseApiController
    {
        private readonly IObjectService _objectService;

        public ObjectController(IObjectService objectService) 
        {
            _objectService = objectService;
        }

        [HttpGet("getInRadius")]
        public ActionResult<List<ObjectDTO>> GetAllInRadius([FromQuery] double radius, [FromQuery] double lat, [FromQuery] double lon)
        {
            var result = _objectService.GetInRadius(radius, lat, lon);
            return CreateResponse(result);
        }
    }
}
