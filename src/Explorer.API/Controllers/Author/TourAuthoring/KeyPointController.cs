using Explorer.API.Controllers.Administrator.Administration;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.API.Public.TourAuthoring.KeypointAddition;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.UseCases;
using Explorer.Tours.Core.UseCases.Administration;
using FluentResults;
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
        private readonly IImageService _imageService;

        public KeyPointController(IKeyPointService keyPointService, IWebHostEnvironment webHostEnvironment, IImageService imageService)
        {
            _keyPointService = keyPointService;
            _webHostEnvironment = webHostEnvironment;
            _imageService = imageService;

        }

        [HttpPost]
        public ActionResult<KeyPointDto> Create([FromBody] KeyPointDto keyPoint)
        {
            if (!string.IsNullOrEmpty(keyPoint.ImageBase64))
            {
                var imageData = Convert.FromBase64String(keyPoint.ImageBase64.Split(',')[1]);
                var folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "keypoints");

               
                keyPoint.Image = _imageService.SaveImage(folderPath, imageData, "keypoints");
            }

            var result = _keyPointService.Create(keyPoint);
            return CreateResponse(result);
        }

       

        [HttpPut("{id:int}")]
        public ActionResult<KeyPointDto> Update([FromBody] KeyPointDto keyPoint)
        {
            if (!string.IsNullOrEmpty(keyPoint.ImageBase64))
            {

                // Brisanje stare slike ako postoji
                if (!string.IsNullOrEmpty(keyPoint.Image))
                {
                    var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, keyPoint.Image);
                    _imageService.DeleteOldImage(oldImagePath);
                }


                // Konvertovanje slike iz base64 formata
                var imageData = Convert.FromBase64String(keyPoint.ImageBase64.Split(',')[1]);

                var folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "keypoints");

                keyPoint.Image = _imageService.SaveImage(folderPath, imageData, "keypoints");
            }

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
        /*     [HttpGet]
             public ActionResult<KeyPointDto> GetAll()
             {
                 var result = _keyPointService.GetByCoordinated(12, 12, 1);
                 return CreateResponse(result);
             }*/
    }
}
