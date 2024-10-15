using Explorer.API.Controllers.Administrator.Administration;
using Explorer.API.Controllers.Author.TourAuthoring;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.API.Public.TourAuthoring.KeypointAddition;
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

public class KeyPointQueryTests : BaseToursIntegrationTest
{
    public KeyPointQueryTests(ToursTestFactory factory) : base(factory) { }

    [Fact]
    public void Retrieves_all()
    {
        //Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);

        // Act
        var result = ((ObjectResult)controller.GetAllByUserId(2).Result)?.Value as List<KeyPointDto>;

        //Assert
        result.ShouldNotBeNull();
        result.Count.ShouldBe(3);

    }

    private static KeyPointController CreateController(IServiceScope scope)
    {
        return new KeyPointController(scope.ServiceProvider.GetRequiredService<IKeyPointService>())
        {
            ControllerContext = BuildContext("-1")
        };
    }
}
