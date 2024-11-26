using Explorer.API.Controllers.Author.TourAuthoring;
using Explorer.API.Controllers.Tourist.TourShopping;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
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

    public class CouponQueryTests:BasePaymentsIntegrationTest
    {
        public CouponQueryTests(PaymentsTestFactory factory) : base(factory) { }

        [Fact]
        public void Retrieves_all()
        {
            //Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            // Act
            var result = ((ObjectResult)controller.GetAll(1,0,0).Result)?.Value as PagedResult<CouponDto>;

            //Assert
            result.ShouldNotBeNull();
            result.Results.Count.ShouldBe(2); //samo one koji nisu istekli, u tesnim podacima ima jedan koji je istekao
            result.TotalCount.ShouldBe(2);

        }
        [Fact]
        public void Retrieves_one_by_tour_id()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            // Act
            var result = ((ObjectResult)controller.GetByTourId(201).Result)?.Value as CouponDto;

            //Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(-1);

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
