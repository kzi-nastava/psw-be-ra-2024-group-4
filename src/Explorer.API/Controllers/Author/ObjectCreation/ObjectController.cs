using Explorer.BuildingBlocks.Core.UseCases;
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
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IImageService _imageService;

        public ObjectController(IObjectService objectService,IWebHostEnvironment webHostEnvironment,IImageService imageService)
        {
            _objectService = objectService;
            _webHostEnvironment = webHostEnvironment;
            _imageService = imageService;
        }

        [HttpGet]
        public ActionResult<PagedResult<ObjectDTO>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        { 
            var result = _objectService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<ObjectDTO> Create([FromBody] ObjectDTO objectDTO)
        {
            if (!string.IsNullOrEmpty(objectDTO.ImageBase64))
            {
                var imageData = Convert.FromBase64String(objectDTO.ImageBase64.Split(',')[1]);
                var folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "objects");

                objectDTO.Image = _imageService.SaveImage(folderPath, imageData, "objects");
            }

            var result = _objectService.Create(objectDTO);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<ObjectDTO> Update([FromBody] ObjectDTO objectDTO)
        {
            if (!string.IsNullOrEmpty(objectDTO.ImageBase64)) {
                var imageData = Convert.FromBase64String(objectDTO.ImageBase64.Split(',')[1]);
                var folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "objects");

                objectDTO.Image = _imageService.SaveImage(folderPath, imageData, "objects");
            }

            var result = _objectService.Update(objectDTO);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _objectService.Delete(id);
            return CreateResponse(result);
        }

        [HttpGet("objectcategories")]
        public IActionResult GetObjectCategories()
        {
            var categories = Enum.GetValues(typeof(ObjectCategory))
                .Cast<ObjectCategory>()
                .Select(cat => new
                {
                    id = (int)cat,
                    name = cat.ToString()
                })
                .ToList();
            return Ok(categories);
        }

    }
}

