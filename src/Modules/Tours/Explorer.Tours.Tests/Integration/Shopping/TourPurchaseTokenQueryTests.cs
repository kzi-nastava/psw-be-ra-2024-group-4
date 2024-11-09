using Explorer.API.Controllers.Tourist.TourShopping;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.TourShopping;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Tests.Integration.Shopping;


[Collection("Sequential")]

public class TourPurchaseTokenQueryTests : BaseToursIntegrationTest
{
    public TourPurchaseTokenQueryTests(ToursTestFactory factory) : base(factory) { }

    [Fact]
    public void Retrieves_all_from_cart()
    {
        //Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);

        // Act
        var result = ((ObjectResult)controller.GetAll(-1).Result)?.Value as List<TourPurchaseTokenDto>;

        //Assert
        result.ShouldNotBeNull();
        result.Count.ShouldBe(2);



    }

    [Fact]
    public void Retrieves_all_from_user()
    {
        //Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);

        // Act
        var result = ((ObjectResult)controller.GetByUser(-12).Result)?.Value as List<TourPurchaseTokenDto>;

        //Assert
        result.ShouldNotBeNull();
        result.Count.ShouldBe(2);

    }

    private static TourPurchaseTokenController CreateController(IServiceScope scope)
    {
        return new TourPurchaseTokenController(scope.ServiceProvider.GetRequiredService<ITourPurchaseTokenService>())
        {
            ControllerContext = BuildContext("-1")
        };
    }

}
