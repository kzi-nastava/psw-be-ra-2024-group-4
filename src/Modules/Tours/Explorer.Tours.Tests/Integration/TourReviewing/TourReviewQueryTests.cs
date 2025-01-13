using Explorer.API.Controllers.Tourist.TourReviewing;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.TourReviewing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Tours.Tests.Integration.TourReviewing
{
    [Collection("Sequential")]
    public class TourReviewQueryTests : BaseToursIntegrationTest
    {
        public TourReviewQueryTests(ToursTestFactory factory) : base(factory) { }

        [Fact]
        public void Retrieves_all()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            // Act
            var result = ((ObjectResult)controller.GetAll(0, 0).Result)?.Value as PagedResult<TourReviewDto>;

            // Assert
            result.ShouldNotBeNull();
            result.Results.Count.ShouldBe(3);
            result.TotalCount.ShouldBe(3);
        }




        [Fact]
        public void Retrieves_one_by_userId_and_tourId()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            // Act
            var result = ((ObjectResult)controller.GetByTouristAndTour(1,1).Result)?.Value as TourReviewDto;

            // Assert
            result.ShouldNotBeNull();
            result.IdTour.ShouldBe(1);
            result.IdTourist.ShouldBe(1);
            result.Rating.ShouldBe(5);
        }



        private static TourReviewController CreateController(IServiceScope scope)
        {
            return new TourReviewController(scope.ServiceProvider.GetRequiredService<ITourReviewService>(), scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>(), scope.ServiceProvider.GetRequiredService<IImageService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
