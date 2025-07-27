using Explorer.API.Controllers.Badges;
using Explorer.API.Controllers.Execution;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Badges;
using Explorer.Tours.API.Public.Execution;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Infrastructure.Database;
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
    public class BadgesQueryTests : BaseToursIntegrationTest
    {
        public BadgesQueryTests(ToursTestFactory factory) : base(factory) { }

        [Fact]
        public void GetAll_ShouldReturnAllBadges()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            // Act
            var result = (ObjectResult)controller.GetAll().Result;

            // Assert
            result.StatusCode.ShouldBe(200);
            result.Value.ShouldNotBeNull();
            
        }

        [Fact]
        public void GetAll_ShouldReturnAllNotReadBadges()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            // Act
            var result = (ObjectResult)controller.GetAllNotRead().Result;

            // Assert
            result.StatusCode.ShouldBe(200);
            result.Value.ShouldNotBeNull();

        }

        public void GetAll_ShouldReturnAllBadgesById()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            // Act
            var result = (ObjectResult)controller.GetAllByUserId(-22).Result;

            // Assert
            result.StatusCode.ShouldBe(200);
            result.Value.ShouldNotBeNull();

        }

        [Fact]
        public void GetAll_ShouldReturnAllNotReadBadgesById()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            // Act
            var result = (ObjectResult)controller.GetAllNotReadByUserId(-22).Result;

            // Assert
            result.StatusCode.ShouldBe(200);
            result.Value.ShouldNotBeNull();

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
