using Explorer.API.Controllers.Tourist.TourReviewing;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.TourReviewing;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Tours.Tests.Integration.TourReviewing;

[Collection("Sequential")]
public class TourReviewCommandTests : BaseToursIntegrationTest
{
    public TourReviewCommandTests(ToursTestFactory factory) : base(factory) { }

    [Fact]
    public void Creates()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
        var newEntity = new TourReviewDto
        {
            //"Id", "IdTour", "IdTourist", "Rating", "Comment", "DateTour", "DateComment", "Image"
            IdTour = -1,
            IdTourist = -2,
            Rating = 3,
            Comment = "Nije nit dobro nit lose",
            DateTour = DateTime.Now.ToUniversalTime(),
            DateComment = DateTime.Now.ToUniversalTime(),
            Image = "Test",
            PercentageCompleted = 55,
        };

        // Act
        var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as TourReviewDto;

        // Assert - Response
        result.ShouldNotBeNull();
        result.Id.ShouldNotBe(0);
        result.Comment.ShouldBe(newEntity.Comment);

        // Assert - Database
        var storedEntity = dbContext.TourReview.FirstOrDefault(i => i.Comment == newEntity.Comment);
        storedEntity.ShouldNotBeNull();
        storedEntity.Id.ShouldBe(result.Id);
    }

    [Fact]
    public void Create_fails_invalid_data()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var updatedEntity = new TourReviewDto
        {
            IdTour = 1,
            IdTourist = 2,
            Rating = 3,
            Comment = "Nije nit dobro nit lose"
        };

        // Act
        var result = (ObjectResult)controller.Create(updatedEntity).Result;

        // Assert
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(400);
    }

    [Fact]
    public void Updates()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
        var updatedEntity = new TourReviewDto
        {
            Id = -1,
            IdTour = 1,
            IdTourist = 2,
            Rating = 4,
            Comment = "dzouns",
            DateTour = DateTime.Now.ToUniversalTime(),
            DateComment = DateTime.Now.ToUniversalTime(),
            Image = "Test",
            PercentageCompleted = 55,
        };

        // Act
        var result = ((ObjectResult)controller.Update(updatedEntity).Result)?.Value as TourReviewDto;

        // Assert - Response
        result.ShouldNotBeNull();
        result.Id.ShouldBe(-1);
        result.IdTour.ShouldBe(updatedEntity.IdTour);
        result.IdTourist.ShouldBe(updatedEntity.IdTourist);
        result.Rating.ShouldBe(updatedEntity.Rating);
        result.Comment.ShouldBe(updatedEntity.Comment);
        result.DateTour.ShouldBe(updatedEntity.DateTour);
        result.DateComment.ShouldBe(updatedEntity.DateComment);

        // Assert - Database
        var storedEntity = dbContext.TourReview.FirstOrDefault(i => i.Comment == "dzouns");
        storedEntity.ShouldNotBeNull();
        storedEntity.Rating.ShouldBe(updatedEntity.Rating);
        var oldEntity = dbContext.TourReview.FirstOrDefault(i => i.Comment == "Mnogo dobra tura.");
        oldEntity.ShouldBeNull();
    }

    [Fact]
    public void Update_fails_invalid_id()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var updatedEntity = new TourReviewDto
        {
            Id = -1000,
            IdTour = 1,
            IdTourist = 2,
            Rating = 3,
            Comment = "Test",
            DateTour = DateTime.Now.ToUniversalTime(),
            DateComment = DateTime.Now.ToUniversalTime(),
        };

        // Act
        var result = (ObjectResult)controller.Update(updatedEntity).Result;

        // Assert
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(404);
    }

    [Fact]
    public void Deletes()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

        // Act
        var result = controller.Delete(-3);

        // Assert - Response
        result.ShouldNotBeNull();

        // Assert - Database
        var storedCourse = dbContext.TourReview.FirstOrDefault(i => i.Id == -3);
        storedCourse.ShouldBeNull();
    }

    [Fact]
    public void Delete_fails_invalid_id()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);

        // Act
        var result = (ObjectResult)controller.Delete(-1000);

        // Assert
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(404);
    }

    private static TourReviewController CreateController(IServiceScope scope)
    {
        return new TourReviewController(scope.ServiceProvider.GetRequiredService<ITourReviewService>(), scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>(), scope.ServiceProvider.GetRequiredService<IImageService>())
        {
            ControllerContext = BuildContext("-1")
        };
    }
}
