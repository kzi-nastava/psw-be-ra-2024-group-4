using Explorer.API.Controllers.Execution;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Execution;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Tests.Integration.TourExecution;
[Collection("Sequential")]
public class PositionSimulatorCommandTests : BaseToursIntegrationTest
{
    public PositionSimulatorCommandTests(ToursTestFactory factory) : base(factory) {}

    [Fact]
    public void Creates()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
        var newEntity = new PositionSimulatorDto
        {
            Id = 0,
            Latitude = 44,
            Longitude = 24,
            TouristId = -21,
        };

        // Act
        var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as PositionSimulatorDto;

        // Assert - Response
        result.ShouldNotBeNull();
        result.Id.ShouldNotBe(0);
        result.Longitude.ShouldBe(newEntity.Longitude);

        // Assert - Database
        var storedEntity = dbContext.Positions.FirstOrDefault(i => i.Longitude == newEntity.Longitude);
        storedEntity.ShouldNotBeNull();
        storedEntity.Id.ShouldBe(result.Id);
    }


    [Fact]
    public void Update()
    {
        //Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
        var updatedEntity = new PositionSimulatorDto
        {
            Id = 1,
            Longitude = 10,
            Latitude = 10,
            TouristId = 3
        };

        //Act
        var result = ((ObjectResult)controller.Update(updatedEntity).Result)?.Value as PositionSimulatorDto;

        // Assert - Response
        result.ShouldNotBeNull();
        result.Id.ShouldBe(1);
        result.Latitude.ShouldBe(updatedEntity.Latitude);
        result.Longitude.ShouldBe(updatedEntity.Longitude);
        result.TouristId.ShouldBe(updatedEntity.TouristId);

        // Assert - Database
        var storedEntity = dbContext.Positions.FirstOrDefault(i => i.Id == 1);
        storedEntity.ShouldNotBeNull();
        storedEntity.Id.ShouldBe(result.Id);
    }
    private static PositionSimulatorController CreateController(IServiceScope scope)
    {
        return new PositionSimulatorController(scope.ServiceProvider.GetRequiredService<IPositionSimulatorService>())
        {
            ControllerContext = BuildContext("-1")
        };
    }
}
