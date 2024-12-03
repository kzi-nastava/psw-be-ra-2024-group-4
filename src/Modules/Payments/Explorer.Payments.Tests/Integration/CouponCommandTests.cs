using Explorer.API.Controllers.Author.PostManagement;
using Explorer.API.Controllers.Author.TourAuthoring;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.Blog.Infrastructure.Database;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
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

namespace Explorer.Payments.Tests.Integration
{
    public class CouponCommandTests : BasePaymentsIntegrationTest
    {
        public CouponCommandTests(PaymentsTestFactory factory) : base(factory) { }

        [Fact]
        public void Creates()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<PaymentsContext>();
            var newEntity = new CouponDto
            {
                DiscountPercentage = 10,
                ExpirationDate = new DateTime(2024, 12, 31, 12, 30, 0, DateTimeKind.Utc),
                AuthorId = 2,
                TourId = 1
            };
            var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as CouponDto;
            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(0);
            result.DiscountPercentage.ShouldBe(newEntity.DiscountPercentage);
            result.ExpirationDate.ShouldBe(newEntity.ExpirationDate);
            result.AuthorId.ShouldBe(newEntity.AuthorId);
            result.TourId.ShouldBe(newEntity.TourId);

            // Assert - Database
            var storedEntity = dbContext.Coupons.FirstOrDefault(i => i.PromoCode == newEntity.PromoCode); //?
            storedEntity.ShouldNotBeNull();
            storedEntity.Id.ShouldBe(result.Id);
        }
        [Fact]
        public void Create_fails_invalid_data()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedEntity = new CouponDto
            {
                DiscountPercentage = 20
            };

            // Act
            var result = (ObjectResult)controller.Create(updatedEntity).Result;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(400);
        }
        [Fact]
        public void Updates()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<PaymentsContext>();
            var updatedEntity = new CouponDto
            {
                Id = -1,
                PromoCode= "SAVE1058",
                DiscountPercentage=13,
                ExpirationDate= DateTime.UtcNow,
                AuthorId=1,
                TourId=201
            };

            // Act
            var result = ((ObjectResult)controller.Update(updatedEntity).Result)?.Value as CouponDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldBe(-1);
            result.PromoCode.ShouldBe(updatedEntity.PromoCode);
            result.ExpirationDate.ShouldNotBe(default);
            result.DiscountPercentage.ShouldBe(updatedEntity.DiscountPercentage);
            result.AuthorId.ShouldBe(updatedEntity.AuthorId);
            result.TourId.ShouldBe(updatedEntity.TourId);
            // Assert - Database
            var storedEntity = dbContext.Coupons.FirstOrDefault(i => i.PromoCode.Equals ("SAVE1058"));
            storedEntity.ShouldNotBeNull();
            storedEntity.PromoCode.ShouldBe(updatedEntity.PromoCode);
            var oldEntity = dbContext.Coupons.FirstOrDefault(i => i.PromoCode.Equals("SAVE1057"));
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
            var storedCourse = dbContext.Coupons.FirstOrDefault(i => i.Id == -3);
            storedCourse.ShouldBeNull();
        }


        private static CouponController CreateController(IServiceScope scope)
        {
            return new CouponController(scope.ServiceProvider.GetRequiredService<ICouponService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }

    }
}
