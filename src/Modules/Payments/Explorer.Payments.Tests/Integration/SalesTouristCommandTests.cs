using Explorer.API.Controllers.Author.TourAuthoring;
using Explorer.API.Controllers.Tourist.TourShopping;
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
    public class SalesTouristCommandTests : BasePaymentsIntegrationTest
    {
        public SalesTouristCommandTests(PaymentsTestFactory factory) : base(factory)
        {
        }

        [Fact]
        public void GetsAllSalesForTourist()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<PaymentsContext>();

            // Act
            var result = ((ObjectResult)controller.GetAll().Result)?.Value as List<SalesDto>;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Count.ShouldBeGreaterThan(0);

            // Assert - Database
            var storedSales = dbContext.Sales.ToList();
            storedSales.Count.ShouldBeGreaterThan(0);
        }

        private static SalesTouristController CreateController(IServiceScope scope)
        {
            return new SalesTouristController(scope.ServiceProvider.GetRequiredService<ISalesService>())
            {
                ControllerContext = BuildContext("-1")  
            };
        }
    }
}
