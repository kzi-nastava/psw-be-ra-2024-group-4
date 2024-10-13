using Explorer.API.Controllers.Administrator.Administration;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.API.Public.TourAuthoring.KeypointAddition;
using Explorer.Tours.Core.UseCases;
using Explorer.Tours.Core.UseCases.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author.TourAuthoring
{

    [Authorize(Policy = "authorPolicy")]
    [Route("api/keypointaddition/keypoint")]

    public class KeyPointController : BaseApiController
    {
        private readonly IKeyPointService _keyPointService;

        public KeyPointController(IKeyPointService keyPointService)
        {
            _keyPointService = keyPointService;
        }

        [HttpPost]
        public ActionResult<KeyPointDto> Create([FromBody] KeyPointDto keyPoint)
        {
            var result = _keyPointService.Create(keyPoint);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<KeyPointDto> Update([FromBody] KeyPointDto keyPoint)
        {
            var result = _keyPointService.Update(keyPoint);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _keyPointService.Delete(id);
            return CreateResponse(result);
        }

        [HttpGet("getbyuser/{userId:long}")]
        public ActionResult<List<KeyPointDto>> GetAllByUserId(long userId)
        {
            var result = _keyPointService.GetByUserId(userId);
            return CreateResponse(result);
        }

        [HttpGet("getbyid/{id:int}")]
        public ActionResult<KeyPointDto> GetById(int id)
        {
            var result = _keyPointService.Get(id);
            return CreateResponse(result);
        }

    }
}
