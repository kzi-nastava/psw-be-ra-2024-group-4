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
    public class PaymentRecordCommandTests : BasePaymentsIntegrationTest
    {
        public PaymentRecordCommandTests(PaymentsTestFactory factory) : base(factory)
        {
        }


        [Fact]
        public void Creates()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<PaymentsContext>();
            var newEntity = new PaymentRecordDto
            {
                BundleId = -1,
                TouristId = -12,
                Price = 50.00m,
                Date = DateTime.UtcNow,



            };

            // Act
            var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as PaymentRecordDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.TouristId.ShouldBe(newEntity.TouristId);
            result.BundleId.ShouldBe(newEntity.BundleId);
            result.Price.ShouldBe(newEntity.Price);
            result.Date.ShouldBe(newEntity.Date);

            // Assert - Database
            var storedEntity = dbContext.PaymentRecords.FirstOrDefault(i => i.Id == result.Id);
            storedEntity.ShouldNotBeNull();
            storedEntity.Id.ShouldBe(result.Id);
        }


        private static PaymentRecordController CreateController(IServiceScope scope)
        {
            return new PaymentRecordController(scope.ServiceProvider.GetRequiredService<IPaymentRecordService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
