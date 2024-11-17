using Explorer.API.Controllers.Tourist.TourShopping;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Tests.Integration.Shopping;


[Collection("Sequential")]

public class TourPurchaseTokenQueryTests : BasePaymentsIntegrationTest
{
    public TourPurchaseTokenQueryTests(PaymentsTestFactory factory) : base(factory) { }

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
