using Explorer.API.Controllers.Administrator.Administration;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.API.Public.TourAuthoring.KeypointAddition;
using Explorer.Tours.Core.UseCases;
using Explorer.Tours.Core.UseCases.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author.TourAuthoring
{

    [Authorize(Policy = "authorPolicy")]
    [Route("api/keypointaddition/keypoint")]

    public class KeyPointController : BaseApiController
    {
        private readonly IKeyPointService _keyPointService;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public KeyPointController(IKeyPointService keyPointService, IWebHostEnvironment webHostEnvironment)
        {
            _keyPointService = keyPointService;
            _webHostEnvironment = webHostEnvironment;

        }

        [HttpPost]
        public ActionResult<KeyPointDto> Create([FromBody] KeyPointDto keyPoint)
        {
            if (!string.IsNullOrEmpty(keyPoint.ImageBase64))
            {
                var imageData = Convert.FromBase64String(keyPoint.ImageBase64.Split(',')[1]);
                var fileName = Guid.NewGuid() + ".png"; // ili format prema potrebi
                var folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "keypoints");

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                var filePath = Path.Combine(folderPath, fileName);
                System.IO.File.WriteAllBytes(filePath, imageData);
                keyPoint.Image = $"images/keypoints/{fileName}";
            }

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

        [HttpGet("next-id/{userid:long}")]
        public ActionResult<int> GetNextClubInvitationId(long userid)
        {

            var nextId = _keyPointService.GetMaxId(userid) + 1;
            return Ok(nextId);
        }

        [HttpPut("addtotour/{tourid:long}")]
        public ActionResult<KeyPointDto> AddKeypointToTour(long tourid, [FromBody] KeyPointDto keypoint)
        {
            var result = _keyPointService.AddKeypointToTour(tourid, keypoint);
            return CreateResponse(result);
        }

    }
}
