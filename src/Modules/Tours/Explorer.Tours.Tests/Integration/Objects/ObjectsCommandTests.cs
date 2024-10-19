using Explorer.API.Controllers.Author.ObjectCreation;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.TourAuthoring.ObjectAddition;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Tests.Integration.Objects;

[Collection("Sequential")]
public class ObjectsCommandTests : BaseToursIntegrationTest
{
    public ObjectsCommandTests(ToursTestFactory factory) : base(factory) { }

    [Fact]
    public void Creates()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
        var newObject = new ObjectDTO
        {
            Name = "Primer restorana",
            Description = "Test.",
            Image = "primer.jpg",
            Longitude = 10.1234,
            Latitude = 12.4321,
            UserId = 10
        };

        // Act
        var result = ((ObjectResult)controller.Create(newObject).Result)?.Value as ObjectDTO;

        // Assert - Response
        result.ShouldNotBeNull();
        result.Id.ShouldNotBe(0);
        result.Name.ShouldBe(newObject.Name);

        // Assert - Database
        var storedObject = dbContext.Objects.FirstOrDefault(o => o.Name == newObject.Name);
        storedObject.ShouldNotBeNull();
        storedObject.Id.ShouldBe(result.Id);
    }

    [Fact]
    public void Updates()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
        var updatedObject = new ObjectDTO
        {
            Id = -1,
            Name = "Updated Object",
            Description = "Updated Description",
            Image = "updated_image.jpg",
            Longitude = 11.1234,
            Latitude = 13.4321,
            UserId = 1
        };

        // Act
        var result = ((ObjectResult)controller.Update(updatedObject).Result)?.Value as ObjectDTO;

        // Assert - Response
        result.ShouldNotBeNull();
        result.Id.ShouldBe(-1);
        result.Name.ShouldBe(updatedObject.Name);
        result.Description.ShouldBe(updatedObject.Description);

        // Assert - Database
        var storedObject = dbContext.Objects.FirstOrDefault(o => o.Name == updatedObject.Name);
        storedObject.ShouldNotBeNull();
        storedObject.Description.ShouldBe(updatedObject.Description);
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
        var storedObject = dbContext.Objects.FirstOrDefault(o => o.Id == -3);
        storedObject.ShouldBeNull();
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

    private static ObjectController CreateController(IServiceScope scope)
    {
        return new ObjectController(scope.ServiceProvider.GetRequiredService<IObjectService>())
        {
            ControllerContext = BuildContext("-1")
        };
    }
}