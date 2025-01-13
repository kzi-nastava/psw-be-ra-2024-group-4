using Explorer.API.Controllers.Execution;
using Explorer.Tours.API.Dtos.TourExecutionDtos;
using Explorer.Tours.API.Public.Badges;
using Explorer.Tours.API.Public.Execution;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Tests.Integration.Execution
{
    [Collection("Sequential")]
    public class TourExecutionQueryTests : BaseToursIntegrationTest
    {
        public TourExecutionQueryTests(ToursTestFactory factory) : base(factory) { }

        [Theory]
        [InlineData(-21, -2, true)]  
        [InlineData(-5, -7, false)] 
        public void GetByTourAndTouristId(long touristId, long tourId, bool shouldBeFound)
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();


            // Act
            var result = (ObjectResult)controller.GetByTourAndTouristId(touristId, tourId).Result;

            // Assert
            if (shouldBeFound)
            {
                result.ShouldNotBeNull();
                result.StatusCode.ShouldBe(200);
                var executionDto = result.Value as TourExecutionDto;
                executionDto.Id.ShouldBe(-2);
            }
            else
            {
                result.ShouldNotBeNull();
                result.StatusCode.ShouldBe(404);
                result.Value.ShouldBe("Tour execution not found for the specified tourist and tour.");
            }
        }

        private static TourExecutionController CreateController(IServiceScope scope)
        {
            return new TourExecutionController(scope.ServiceProvider.GetRequiredService<ITourExecutionService>(), scope.ServiceProvider.GetRequiredService<IBadgeService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }

    }
}
