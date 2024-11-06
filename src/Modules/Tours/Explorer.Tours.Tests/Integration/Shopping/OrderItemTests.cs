using Explorer.API.Controllers.Tourist.TourShopping;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.TourShopping;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Tests.Integration.Shopping
{
    [Collection("Sequential")]
    public class OrderItemTests : BaseToursIntegrationTest
    {
        public OrderItemTests(ToursTestFactory factory) : base(factory) { }

        [Fact]
        public void CreatesOrderItem()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            var newItem = new OrderItemDto
            {
                TourName = "Tour A",
                Price = 100.00m,
                TourId = 1,
                CartId = -1
            };

            // Act
            var result = ((ObjectResult)controller.Create(newItem).Result)?.Value as OrderItemDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(0);
            result.TourName.ShouldBe(newItem.TourName);
            result.Price.ShouldBe(newItem.Price);
            result.TourId.ShouldBe(newItem.TourId);

            // Assert - Database
            var storedItem = dbContext.OrderItems.FirstOrDefault(i => i.Id == result.Id);
            storedItem.ShouldNotBeNull();
            storedItem.TourName.ShouldBe(newItem.TourName);
            storedItem.Price.ShouldBe(newItem.Price);
        }

        [Fact]
        public void GetOrderItemById_ReturnsItem_WhenExists()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var expectedItemId = -1;

            // Act
            var result = ((ObjectResult)controller.Get(expectedItemId).Result)?.Value as OrderItemDto;

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(expectedItemId);
            result.TourId.ShouldBe(1);
            result.Price.ShouldBe(100.00m);
        }

        [Fact]
        public void DeleteOrderItem_DeletesItem_WhenExists()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var itemIdToDelete = -1; // Predefined item ID for testing

            // Act
            var result = (OkResult)controller.Delete(itemIdToDelete);

            // Assert - Response
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);

            // Assert - Database
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            var storedItem = dbContext.OrderItems.FirstOrDefault(i => i.Id == itemIdToDelete);
            storedItem.ShouldBeNull();
        }

        [Fact]
        public void CalculateTotalPrice_ReturnsCorrectTotal()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var cartId = -1; // ID of the cart to calculate total price for

            // Act
            var result = ((ObjectResult)controller.CalculateTotalPrice(cartId).Result)?.Value as decimal?;

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBe(200.00m); // Expected total price for the cart
        }

        [Fact]
        public void GetAllFromCart_ReturnsItems()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var cartId = -1;

            // Act
            var result = ((ObjectResult)controller.GetAll(cartId).Result)?.Value as List<OrderItemDto>;

            // Assert
            result.ShouldNotBeNull();
            result.Count.ShouldBeGreaterThan(0);

            var firstItem = result.FirstOrDefault();
            firstItem.ShouldNotBeNull();
            firstItem.TourId.ShouldBeGreaterThan(0);
            firstItem.Price.ShouldBeGreaterThan(0);
        }

        private static OrderItemController CreateController(IServiceScope scope)
        {
            return new OrderItemController(scope.ServiceProvider.GetRequiredService<IOrderItemService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }


    }
}
