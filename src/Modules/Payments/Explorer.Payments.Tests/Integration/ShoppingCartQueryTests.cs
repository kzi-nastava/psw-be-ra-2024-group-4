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

public class ShoppingCartQueryTests : BasePaymentsIntegrationTest
{
    public ShoppingCartQueryTests(PaymentsTestFactory factory) : base(factory) { }

    [Fact]
    public void Retrieves_all()
    {
        //Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);

        // Act
        var result = ((ObjectResult)controller.GetAll(-13).Result)?.Value as List<ShoppingCartDto>;

        //Assert
        result.ShouldNotBeNull();
        result.Count.ShouldBe(1);

    }

    private static ShoppingCartController CreateController(IServiceScope scope)
    {
        return new ShoppingCartController(scope.ServiceProvider.GetRequiredService<IShoppingCartService>(), scope.ServiceProvider.GetRequiredService<ICouponService>())
        {
            ControllerContext = BuildContext("-1")
        };
    }

}
