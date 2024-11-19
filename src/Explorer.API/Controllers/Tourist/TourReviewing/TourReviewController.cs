using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Modules.Core.Domain;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.TourReviewing;
using Explorer.Tours.Core.Domain.Tours;
using Explorer.Tours.Core.UseCases.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace Explorer.API.Controllers.Tourist.TourReviewing
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourReviewing/tourReview")]
    public class TourReviewController : BaseApiController
    {
        private readonly ITourReviewService _tourReviewService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IImageService _imageService;

        public TourReviewController(ITourReviewService tourReviewService, IWebHostEnvironment webHostEnvironment, IImageService imageService)
        {
            _tourReviewService = tourReviewService;
            _webHostEnvironment = webHostEnvironment;
            _imageService = imageService;
        }

        [HttpGet]
        public ActionResult<PagedResult<TourReviewDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _tourReviewService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpGet("by_tourist_and_tour/{userId:long}/{tourId:long}")]
        public ActionResult<PagedResult<TourReviewDto>> GetByTouristAndTour(long userId, long tourId)
        {
            var result = _tourReviewService.Get(userId, tourId);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<TourReviewDto> Create([FromBody] TourReviewDto tourReview)
        {
            if (!string.IsNullOrEmpty(tourReview.ImageBase64))
            {
                var imageData = Convert.FromBase64String(tourReview.ImageBase64.Split(',')[1]);
                var folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "reviews");


                tourReview.Image = _imageService.SaveImage(folderPath, imageData, "reviews");
            }
            var result = _tourReviewService.Create(tourReview);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<TourReviewDto> Update([FromBody] TourReviewDto tourReview)
        {
            if (!string.IsNullOrEmpty(tourReview.ImageBase64))
            {

                // Brisanje stare slike ako postoji
                if (!string.IsNullOrEmpty(tourReview.Image))
                {
                    var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, tourReview.Image);
                    _imageService.DeleteOldImage(oldImagePath);
                }


                // Konvertovanje slike iz base64 formata
                var imageData = Convert.FromBase64String(tourReview.ImageBase64.Split(',')[1]);

                var folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "reviews");

                tourReview.Image = _imageService.SaveImage(folderPath, imageData, "reviews");
            }
            var result = _tourReviewService.Update(tourReview);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _tourReviewService.Delete(id);
            return CreateResponse(result);
        }
    }
}
