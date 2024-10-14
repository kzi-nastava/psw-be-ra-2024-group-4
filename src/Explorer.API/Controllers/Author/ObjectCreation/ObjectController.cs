using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.API.Public.TourAuthoring.ObjectAddition;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author.ObjectCreation
{
    [Authorize(Policy = "authorPolicy")]
    [Route("api/objectaddition/object")]
    public class ObjectController : BaseApiController
    {
        private readonly IObjectService _objectService;

        public ObjectController(IObjectService objectService)
        {
            _objectService = objectService;
        }

        [HttpPost]
        public ActionResult<ObjectDTO> Create([FromBody] ObjectDTO objectDTO)
        {
            var result = _objectService.Create(objectDTO);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<ObjectDTO> Update([FromBody] ObjectDTO objectDTO)
        {
            var result = _objectService.Update(objectDTO);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _objectService.Delete(id);
            return CreateResponse(result);
        }

    }
}

