using Explorer.API.Controllers.Administrator.Administration;
using Explorer.API.Controllers.Author.TourAuthoring;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.API.Public.TourAuthoring.KeypointAddition;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Tests.Integration.TourAuthoring;

[Collection("Sequential")]
public class KeyPointCommandTests : BaseToursIntegrationTest
{
    public KeyPointCommandTests(ToursTestFactory factory) : base(factory) { }

    [Fact]
    public void Creates()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
        var newEntity = new KeyPointDto
        {
            Name = "Test",
            Longitude = 2,
            Latitude = 2,
            Description = "Test",
            Image = "Test",
            UserId = 2
        };

        // Act
        var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as KeyPointDto;

        // Assert - Response
        result.ShouldNotBeNull();
        result.Id.ShouldNotBe(0);
        result.Name.ShouldBe(newEntity.Name);
        result.Longitude.ShouldBe(newEntity.Longitude);
        result.Latitude.ShouldBe(newEntity.Latitude);
        result.Description.ShouldBe(newEntity.Description);
        result.Image.ShouldBe(newEntity.Image);
        result.UserId.ShouldBe(newEntity.UserId);

        // Assert - Database
        var storedEntity = dbContext.KeyPoints.FirstOrDefault(i => i.Name == newEntity.Name);
        storedEntity.ShouldNotBeNull();
        storedEntity.Id.ShouldBe(result.Id);
    }


    [Fact]
    public void Updates()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
        var updatedEntity = new KeyPointDto
        {
            Id=-1,
            Name = "TestUpdate",
            Longitude = 2,
            Latitude = 2,
            Description = "Test",
            Image = "Test",
            UserId = 2
        };

        // Act
        var result = ((ObjectResult)controller.Update(updatedEntity).Result)?.Value as KeyPointDto;

        // Assert - Response
        result.ShouldNotBeNull();
        result.Id.ShouldBe(-1);
        result.Name.ShouldBe(updatedEntity.Name);
        result.Longitude.ShouldBe(updatedEntity.Longitude);
        result.Latitude.ShouldBe(updatedEntity.Latitude);
        result.Description.ShouldBe(updatedEntity.Description);
        result.Image.ShouldBe(updatedEntity.Image);
        result.UserId.ShouldBe(updatedEntity.UserId);

        // Assert - Database
        var storedEntity = dbContext.KeyPoints.FirstOrDefault(i => i.Name == "TestUpdate");
        storedEntity.ShouldNotBeNull();
        storedEntity.Description.ShouldBe(updatedEntity.Description);
        var oldEntity = dbContext.KeyPoints.FirstOrDefault(i => i.Name == "Colosseum");
        oldEntity.ShouldBeNull();
    }


    [Fact]
    public void Deletes()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

        // Act
        var result = (OkResult)controller.Delete(-3);

        // Assert - Response
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(200);

        // Assert - Database
        var storedCourse = dbContext.KeyPoints.FirstOrDefault(i => i.Id == -3);
        storedCourse.ShouldBeNull();
    }


    private static KeyPointController CreateController(IServiceScope scope)
    {
        return new KeyPointController(scope.ServiceProvider.GetRequiredService<IKeyPointService>(), scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>())
        {
            ControllerContext = BuildContext("-1")
        };
    }
}
