using Explorer.API.Controllers.Author.TourAuthoring;
using Explorer.API.Controllers.Tourist.TourShopping;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.TourAuthoring.KeypointAddition;
using Explorer.Tours.API.Public.TourShopping;
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


namespace Explorer.Tours.Tests.Integration.Shopping;
[Collection("Sequential")]

public class ShoppingCartCommandTests : BaseToursIntegrationTest
{
    public ShoppingCartCommandTests(ToursTestFactory factory) : base(factory) { }

    [Fact]
    public void Creates()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
        var newEntity = new ShoppingCartDto
        {
            UserId = -23,
            Items = new List<OrderItemDto>(),
            PurchaseTokens = new List<TourPurchaseTokenDto>()
        };

        // Act
        var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as ShoppingCartDto;

        // Assert - Response
        result.ShouldNotBeNull();
        result.UserId.ShouldBe(newEntity.UserId);

        // Assert - Database
        var storedEntity = dbContext.ShoppingCarts.FirstOrDefault(i => i.UserId == newEntity.UserId);
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
        var updatedEntity = new ShoppingCartDto
        {
            Id = -1,
            UserId = -11,
            Items = new List<OrderItemDto>(),
            PurchaseTokens = new List<TourPurchaseTokenDto>()

        };

        // Act
        var result = ((ObjectResult)controller.Update(updatedEntity).Result)?.Value as ShoppingCartDto;

        // Assert - Response
        result.ShouldNotBeNull();
        result.Id.ShouldBe(-1);
        result.UserId.ShouldBe(updatedEntity.UserId);

        // Assert - Database
        var storedEntity = dbContext.ShoppingCarts.FirstOrDefault(i => i.UserId == -11);
        storedEntity.ShouldNotBeNull();
        storedEntity.UserId.ShouldBe(updatedEntity.UserId);
        var oldEntity = dbContext.ShoppingCarts.FirstOrDefault(i => i.UserId == -12);
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
        var storedCourse = dbContext.ShoppingCarts.FirstOrDefault(i => i.Id == -3);
        storedCourse.ShouldBeNull();
    }


    private static ShoppingCartController CreateController(IServiceScope scope)
    {
        return new ShoppingCartController(scope.ServiceProvider.GetRequiredService<IShoppingCartService>())
        {
            ControllerContext = BuildContext("-1")
        };
    }

}
