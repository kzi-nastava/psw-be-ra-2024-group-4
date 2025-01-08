using Explorer.API.Controllers.Badges;
using Explorer.API.Controllers.Tourist.TourReviewing;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.TourExecutionDtos;
using Explorer.Tours.API.Public.Badges;
using Explorer.Tours.API.Public.TourReviewing;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Tests.Integration.Badges
{
    [Collection("Sequential")]
    public class BadgesCommandTests : BaseToursIntegrationTest
    {
        public BadgesCommandTests(ToursTestFactory factory) : base(factory) { }

        [Fact]
        public void Read_ShouldReturnBadge_WhenBadgeExists()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
           

            // Act
            var result = ((ObjectResult)controller.Read(1).Result)?.Value as BadgeDto;

            // Assert
            result.Id.ShouldBe(1);
            result.IsRead.ShouldBeTrue();
        }

        [Fact]
        public void Read_ShouldReturnNotFound_WhenBadgeDoesNotExist()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            // Act
            var result = (ObjectResult)controller.Read(9999).Result; // Non-existing ID

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(404);
            result.Value.ShouldBe("Badge with ID 9999 not found.");
        }



        private static BadgeContoller CreateController(IServiceScope scope)
        {
            return new BadgeContoller(scope.ServiceProvider.GetRequiredService<IBadgeService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
