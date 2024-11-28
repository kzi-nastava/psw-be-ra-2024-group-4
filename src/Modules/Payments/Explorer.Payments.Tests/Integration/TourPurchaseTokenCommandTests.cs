using Explorer.API.Controllers.Tourist.TourShopping;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Infrastructure.Database;
using Explorer.Tours.Infrastructure.Database;
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
public class TourPurchaseTokenCommandTests : BasePaymentsIntegrationTest
{
    public TourPurchaseTokenCommandTests(PaymentsTestFactory factory) : base(factory) { }

    [Fact]
    public void Creates()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<PaymentsContext>();
        var newEntity = new TourPurchaseTokenDto
        {
            CartId = -1,
            UserId = 1,
            TourId = -2,
            Price = 500.0m,
            PurchaseDate = DateTime.UtcNow,

        };

        // Act
        var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as TourPurchaseTokenDto;

        // Assert - Response
        result.ShouldNotBeNull();
        result.UserId.ShouldBe(newEntity.UserId);
        result.CartId.ShouldBe(newEntity.CartId);
        result.TourId.ShouldBe(newEntity.TourId);
        result.Price.ShouldBe(newEntity.Price);
        result.PurchaseDate.ShouldBe(newEntity.PurchaseDate);

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
        var dbContext = scope.ServiceProvider.GetRequiredService<PaymentsContext>();

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
