using Explorer.API.Controllers.Execution;
using Explorer.Tours.API.Public.Execution;
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
public class PositionSimulatorQueryTests : BaseToursIntegrationTest
{
    public PositionSimulatorQueryTests(ToursTestFactory factory) : base(factory){}

    [Fact]
    public void Retrieves_one()
    {
        //Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);

        //Act
        var result = ((ObjectResult)controller.Get(-1).Result);

        //Assert
        result.ShouldNotBe(null);
        result.StatusCode.ShouldBe(200);
    }

    private static PositionSimulatorController CreateController(IServiceScope scope)
    {
        return new PositionSimulatorController(scope.ServiceProvider.GetRequiredService<IPositionSimulatorService>())
        {
            ControllerContext = BuildContext("-1")
        };
    }
}
