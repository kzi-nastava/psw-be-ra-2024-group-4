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

public class ShoppingCartQueryTests : BaseToursIntegrationTest
{
    public ShoppingCartQueryTests(ToursTestFactory factory) : base(factory) { }

    [Fact]
    public void Retrieves_all()
    {
        //Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);

        // Act
        var result = ((ObjectResult)controller.GetAll(-12).Result)?.Value as List<ShoppingCartDto>;

        //Assert
        result.ShouldNotBeNull();
        result.Count.ShouldBe(1);

    }

    private static ShoppingCartController CreateController(IServiceScope scope)
    {
        return new ShoppingCartController(scope.ServiceProvider.GetRequiredService<IShoppingCartService>())
        {
            ControllerContext = BuildContext("-1")
        };
    }

}
