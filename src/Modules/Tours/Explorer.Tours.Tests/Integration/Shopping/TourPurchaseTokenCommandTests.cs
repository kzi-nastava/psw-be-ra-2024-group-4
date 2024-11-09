using Explorer.API.Controllers.Tourist.TourShopping;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.TourShopping;
using Explorer.Tours.Infrastructure.Database;
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
public class TourPurchaseTokenCommandTests : BaseToursIntegrationTest
{
    public TourPurchaseTokenCommandTests(ToursTestFactory factory) : base(factory) { }

    [Fact]
    public void Creates()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
        var newEntity = new TourPurchaseTokenDto
        {
            CartId = -1,
            UserId = 1,
            TourId = -2
        };

        // Act
        var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as TourPurchaseTokenDto;

        // Assert - Response
        result.ShouldNotBeNull();
        result.UserId.ShouldBe(newEntity.UserId);
        result.CartId.ShouldBe(newEntity.CartId);
        result.TourId.ShouldBe(newEntity.TourId);

        // Assert - Database
        var storedEntity = dbContext.PurchaseTokens.FirstOrDefault(i => i.UserId == newEntity.UserId);
        storedEntity.ShouldNotBeNull();
        storedEntity.Id.ShouldBe(result.Id);
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
        var storedCourse = dbContext.PurchaseTokens.FirstOrDefault(i => i.Id == -3);
        storedCourse.ShouldBeNull();
    }


    private static TourPurchaseTokenController CreateController(IServiceScope scope)
    {
        return new TourPurchaseTokenController(scope.ServiceProvider.GetRequiredService<ITourPurchaseTokenService>())
        {
            ControllerContext = BuildContext("-1")
        };
    }
}
