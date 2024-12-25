using Explorer.API.Controllers.Author.TourAuthoring;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Tests.Integration
{
    [Collection("Sequential")]
    public class SalesCommandTests : BasePaymentsIntegrationTest
    {
        public SalesCommandTests(PaymentsTestFactory factory) : base(factory)
        {
        }

        [Fact]
        public void CreatesSales()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<PaymentsContext>();
            var newSalesDto = new SalesDto
            {
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(7),
                DiscountPercentage = 20.0,
                AuthorId = 1,
                TourIds = new List<int> { 1, 2, 3 }
            };

            // Act
            var result = ((ObjectResult)controller.Create(newSalesDto).Result)?.Value as SalesDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.StartDate.ShouldBe(newSalesDto.StartDate);
            result.EndDate.ShouldBe(newSalesDto.EndDate);
            result.DiscountPercentage.ShouldBe(newSalesDto.DiscountPercentage);
            result.AuthorId.ShouldBe(newSalesDto.AuthorId);
            result.TourIds.ShouldBe(newSalesDto.TourIds);

            // Assert - Database
            var storedSales = dbContext.Sales.FirstOrDefault(s => s.AuthorId == newSalesDto.AuthorId);
            storedSales.ShouldNotBeNull();
            storedSales.DiscountPercentage.ShouldBe(result.DiscountPercentage);
        }

        [Fact]
        public void UpdatesSales()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<PaymentsContext>();

            // Retrieve existing sales entity from the database
            var existingSales = dbContext.Sales.FirstOrDefault();
            existingSales.ShouldNotBeNull();

            // Detach the existing entity from the context to avoid tracking conflicts
            dbContext.Entry(existingSales).State = Microsoft.EntityFrameworkCore.EntityState.Detached;

            // Create the updated DTO with new values
            var updatedSalesDto = new SalesDto
            {
                Id = (int)existingSales.Id,
                StartDate = existingSales.StartDate.AddDays(1),
                EndDate = existingSales.EndDate.AddDays(1),
                DiscountPercentage = 20.0,
                AuthorId = existingSales.AuthorId,
                TourIds = new List<int> { 1, 2, 3 }
            };

            // Act
            var result = ((ObjectResult)controller.Update((int)existingSales.Id, updatedSalesDto).Result)?.Value as SalesDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.StartDate.ShouldBe(updatedSalesDto.StartDate);
            result.EndDate.ShouldBe(updatedSalesDto.EndDate);
            result.DiscountPercentage.ShouldBe(updatedSalesDto.DiscountPercentage);

            // Assert - Database
            var storedSales = dbContext.Sales.FirstOrDefault(s => s.Id == existingSales.Id);
            storedSales.ShouldNotBeNull();
            storedSales.DiscountPercentage.ShouldBe(updatedSalesDto.DiscountPercentage);
        }


        [Fact]
        public void GetsAllSalesForUser()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<PaymentsContext>();
            var userId = 1; // Adjust this based on the test data

            // Act
            var result = ((ObjectResult)controller.GetAll(userId).Result)?.Value as List<SalesDto>;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Count.ShouldBeGreaterThan(0);

            // Assert - Database
            var storedSales = dbContext.Sales.Where(s => s.AuthorId == userId).ToList();
            storedSales.Count.ShouldBeGreaterThan(0);
        }

        [Fact]
        public void DeletesSales()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<PaymentsContext>();
            var existingSales = dbContext.Sales.FirstOrDefault();
            existingSales.ShouldNotBeNull();

            // Act
            var result = (OkResult)controller.Delete((int)existingSales.Id);

            // Assert - Response
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);

            // Assert - Database
            var deletedSales = dbContext.Sales.FirstOrDefault(s => s.Id == existingSales.Id);
            deletedSales.ShouldBeNull();
        }

        private static SalesController CreateController(IServiceScope scope)
        {
            return new SalesController(scope.ServiceProvider.GetRequiredService<ISalesService>())
            {
                ControllerContext = BuildContext("-1")  
            };
        }

    }
}
