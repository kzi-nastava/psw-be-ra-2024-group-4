using Explorer.API.Controllers.Tourist;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;

namespace Explorer.Tours.Tests.Integration.Tourist {
    [Collection("Sequential")]
    public class TourPreferenceControllerTests : BaseToursIntegrationTest {
        public TourPreferenceControllerTests(ToursTestFactory factory) : base(factory) { }

        [Fact]
        public async Task GetTourPreference_ReturnsOk() {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var touristId = 1;

            // Act
            var result = await controller.GetTourPreference(touristId) as OkObjectResult;

            // Assert
            result.ShouldNotBeNull();
            var preference = result.Value as TourPreferenceDto;
            preference.ShouldNotBeNull();
            preference.TouristId.ShouldBe(touristId);
        }

        [Fact]
        public async Task GetTourPreference_ReturnsNotFound() {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var touristId = -1; // Non-existent tourist ID

            // Act
            var result = await controller.GetTourPreference(touristId) as OkObjectResult;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);
        }

        [Fact]
        public async Task AddTourPreference_ReturnsNoContent() {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var touristId = 2; // New tourist ID
            var preference = new TourPreferenceDto {
                TouristId = touristId,
                WeightPreference = 7,
                WalkingRating = 5,
                BikeRating = 4,
                CarRating = 3,
                BoatRating = 2,
                Tags = new List<string> { "culture", "art" }
            };

            // Act
            var result = await controller.AddTourPreference(touristId, preference) as NoContentResult;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(204);
        }

        [Fact]
        public async Task UpdateTourPreference_ReturnsNoContent() {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var touristId = 1; 
            var preference = new TourPreferenceDto {
                Id = 1,
                TouristId = touristId,
                WeightPreference = 2, 
                WalkingRating = 2,
                BikeRating = 2,
                CarRating = 2,
                BoatRating = 2,
                Tags = new List<string> { "nature", "adventure" }
            };

            // Act
            var result = await controller.UpdateTourPreference(touristId, preference) as NoContentResult;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(204);
        }
        [Fact]
        public async Task UpdateTourPreference_ReturnsBadRequest_WhenTouristNotFound() {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var touristId = -1;
            var preference = new TourPreferenceDto {
                TouristId = touristId,
                WeightPreference = 3,
                WalkingRating = 3,
                BikeRating = 3,
                CarRating = 2,
                BoatRating = 1,
                Tags = new List<string> { "culture", "history" }
            };

            // Act
            var result = await controller.UpdateTourPreference(touristId, preference) as BadRequestObjectResult;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(400);
        }

        private static TourPreferenceController CreateController(IServiceScope scope) {
            return new TourPreferenceController(scope.ServiceProvider.GetRequiredService<ITourPreferenceService>()) {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
