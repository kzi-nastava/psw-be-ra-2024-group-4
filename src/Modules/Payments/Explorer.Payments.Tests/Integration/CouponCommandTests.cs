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
                AuthorId = 1,
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
        private static CouponController CreateController(IServiceScope scope)
        {
            return new CouponController(scope.ServiceProvider.GetRequiredService<ICouponService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }

    }
}
