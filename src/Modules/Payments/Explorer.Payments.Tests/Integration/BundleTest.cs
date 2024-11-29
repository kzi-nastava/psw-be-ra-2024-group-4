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
    public class BundleTest: BasePaymentsIntegrationTest
    {
        public BundleTest(PaymentsTestFactory factory) : base(factory) { }

        [Fact]
        public void Creates()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<PaymentsContext>();
            var newEntity = new BundleDto
            {
                Id=-1,
                Name = "Rimsko jezero",
                Price = 1000,
                TourIds = new List<long> { 1, 2 },
                Status =BundleDto.BundleStatus.DRAFT,
                AuthorId = 1,

            };

            // Act
            var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as BundleDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Name.ShouldBe(newEntity.Name);
            result.Price.ShouldBe(newEntity.Price);

            // Assert - Database
            var storedEntity = dbContext.Bundles.FirstOrDefault(i => i.Name == newEntity.Name);
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
            var updatedEntity = new BundleDto
            {
                Id = -1,
                Name = "Rimsko jezero",
                Price = 1000,
                TourIds = new List<long> { 1, 2 },
                Status =BundleDto.BundleStatus.PUBLISHED,
                AuthorId = 1,

            };

            // Act
            var result = ((ObjectResult)controller.Update(updatedEntity).Result)?.Value as BundleDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldBe(-1);
            result.Id.ShouldBe(updatedEntity.Id);

            // Assert - Database
            var storedEntity = dbContext.Bundles.FirstOrDefault(i => i.Status == Core.Domain.Bundle.BundleStatus.PUBLISHED);
            storedEntity.ShouldNotBeNull();
            storedEntity.Name.ShouldBe(updatedEntity.Name);
            var oldEntity = dbContext.Bundles.FirstOrDefault(i => i.Status == Core.Domain.Bundle.BundleStatus.DRAFT);
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
            var result = (OkResult)controller.Delete(-1);

            // Assert - Response
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);

            // Assert - Database
            var storedCourse = dbContext.ShoppingCarts.FirstOrDefault(i => i.Id == -1);
            storedCourse.ShouldBeNull();
        }



        private static BundleController CreateController(IServiceScope scope)
        {
            return new BundleController(scope.ServiceProvider.GetRequiredService<IBundleService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
