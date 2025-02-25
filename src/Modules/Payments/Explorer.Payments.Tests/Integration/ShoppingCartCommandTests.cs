﻿using Explorer.API.Controllers.Author.TourAuthoring;
using Explorer.API.Controllers.Tourist.TourShopping;
using Explorer.Payments.API.Dtos;
using Explorer.Tours.API.Public.TourAuthoring.KeypointAddition;
using Explorer.Payments.API.Public;
using Explorer.Payments.Infrastructure.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Payments.Tests;

namespace Explorer.Payments.Tests.Integration.Shopping;
[Collection("Sequential")]

public class ShoppingCartCommandTests : BasePaymentsIntegrationTest
{
    public ShoppingCartCommandTests(PaymentsTestFactory factory) : base(factory) { }

    [Fact]
    public void Creates()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<PaymentsContext>();
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
        var dbContext = scope.ServiceProvider.GetRequiredService<PaymentsContext>();
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
        var dbContext = scope.ServiceProvider.GetRequiredService<PaymentsContext>();

        // Act
        var result = (OkResult)controller.Delete(-3);

        // Assert - Response
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(200);

        // Assert - Database
        var storedCourse = dbContext.ShoppingCarts.FirstOrDefault(i => i.Id == -3);
        storedCourse.ShouldBeNull();
    }


    [Fact]
    public void GetCouponByPromoCode_ReturnsCoupon()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var validPromoCode = "SAVE1057"; // Pretpostavka: validan promo kod postoji u bazi

        // Act
        var actionResult = controller.GetCouponByPromoCode(validPromoCode);
        var okResult = actionResult.Result as OkObjectResult; // Proverava da li je rezultat OkObjectResult
        var result = okResult?.Value as CouponDto;

        // Assert
        result.ShouldNotBeNull();
        result.PromoCode.ShouldBe(validPromoCode);
    }

    [Fact]
    public void GetCouponByPromoCode_ReturnsBadRequest_ForInvalidPromoCode()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var invalidPromoCode = "grgr"; 

        // Act
        var actionResult = controller.GetCouponByPromoCode(invalidPromoCode);
        var badRequestResult = actionResult.Result as BadRequestObjectResult; 

        // Assert
        badRequestResult.ShouldNotBeNull();
        badRequestResult.StatusCode.ShouldBe(400); 
        var errors = badRequestResult.Value as List<string>;
        errors.ShouldNotBeNull();
        errors.First().ShouldBe("Coupon with the provided promo code does not exist.");
    }



    [Fact]
    public void ApplyCoupon_AppliesCouponSuccessfully()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var cartId = -1; 
        var promoCode = "WINTER15"; 

        // Act
        var actionResult = controller.ApplyCoupon(cartId, promoCode);
        var okResult = actionResult.Result as OkObjectResult; 
        var result = okResult?.Value as ShoppingCartDto;

        // Assert
        okResult.ShouldNotBeNull();
        okResult.StatusCode.ShouldBe(200); 
        result.ShouldNotBeNull();
        result.Id.ShouldBe(cartId);
    }

    [Fact]
    public void ApplyCoupon_ReturnsBadRequest_ForInvalidCartId()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var invalidCartId = -999; 
        var promoCode = "WINTER15";

        // Act
        // Act
        var actionResult = controller.ApplyCoupon(invalidCartId, promoCode);
        var badRequestResult = actionResult.Result as BadRequestObjectResult;
        var errorMessage = badRequestResult?.Value as string; 

        // Assert
        badRequestResult.ShouldNotBeNull();
        badRequestResult.StatusCode.ShouldBe(400); 
        errorMessage.ShouldBe("Cart not found.");
    }

    [Fact]
    public void ApplyCoupon_ReturnsBadRequest_ForExpiredCoupon()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var cartId = -2;
        var expiredPromoCode = "SPRING20"; 

        // Act
        var actionResult = controller.ApplyCoupon(cartId, expiredPromoCode);
        var badRequestResult = actionResult.Result as BadRequestObjectResult; 
        var errorMessage = badRequestResult?.Value as string; 

        // Assert
        badRequestResult.ShouldNotBeNull();
        badRequestResult.StatusCode.ShouldBe(400); 
        errorMessage.ShouldBe("Coupon has expired.");
    }


    private static ShoppingCartController CreateController(IServiceScope scope)
    {
        return new ShoppingCartController(
            scope.ServiceProvider.GetRequiredService<IShoppingCartService>(),
            scope.ServiceProvider.GetRequiredService<ICouponService>())
        {
            ControllerContext = BuildContext("-1")
        };
    }

}
