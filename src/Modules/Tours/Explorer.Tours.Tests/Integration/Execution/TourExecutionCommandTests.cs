using Explorer.API.Controllers.Author.ObjectCreation;
using Explorer.API.Controllers.Execution;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.TourExecutionDtos;
using Explorer.Tours.API.Public.Execution;
using Explorer.Tours.API.Public.TourAuthoring.ObjectAddition;
using Explorer.Tours.Core.Domain.TourExecutions;
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
    public class TourExecutionCommandTests : BaseToursIntegrationTest
    {
        public TourExecutionCommandTests(ToursTestFactory factory) : base(factory) { }
        
        [Fact]
        public void Creates()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            var newEntity = new TourExecutionDto
            {
                Id = -4,
                TourId = -1,
                TouristId = -21,
                LocationId = -3,
                LastActivity = DateTime.UtcNow,
                Status = API.Dtos.TourExecutionDtos.TourExecutionStatus.Active,
                CompletedKeys = new List<CompletedKeyPointDto>()
            };

            // Act
            var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as TourExecutionDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(0);
            result.LocationId.ShouldBe(newEntity.LocationId);

            // Assert - Database
            var storedEntity = dbContext.TourExecution.FirstOrDefault(i => i.LocationId == newEntity.LocationId);
            storedEntity.ShouldNotBeNull();
            storedEntity.Id.ShouldBe(result.Id);


        }

        [Theory]
        [InlineData(-1, true)] 
        [InlineData(-2, false)] 
        public void CompleteTourExecution_VariousStatuses_HandlesAsExpected(int executionId, bool shouldComplete)
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            // Act & Assert
            if (shouldComplete)
            {
                // Act
                var result = ((ObjectResult)controller.CompleteTourExecution(executionId).Result)?.Value as TourExecutionDto;

                // Assert - Response
                result.ShouldNotBeNull();
                var completedExecution = dbContext.TourExecution.FirstOrDefault(i => i.Id == executionId);
                completedExecution.ShouldNotBeNull();
                completedExecution.Status.ShouldBe(Core.Domain.TourExecutions.TourExecutionStatus.Completed);
                completedExecution.LastActivity.ShouldNotBeNull();
            }
            else
            {
                // Act & Assert
                var exception = Assert.Throws<ArgumentException>(() => controller.CompleteTourExecution(executionId));
                exception.Message.ShouldBe("Invalid end status.");
            }
        }

        [Theory]
        [InlineData(-2, false)]
        [InlineData(-3, true)]
        public void AbandonTourExecution_VariousStatuses_HandlesAsExpected(int executionId, bool shouldAbandon)
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            // Act & Assert
            if (shouldAbandon)
            {
                // Act
                var result = ((ObjectResult)controller.AbandonTourExecution(executionId).Result)?.Value as TourExecutionDto;

                // Assert - Response
                //result.ShouldNotBeNull();
                var completedExecution = dbContext.TourExecution.FirstOrDefault(i => i.Id == executionId);
                completedExecution.ShouldNotBeNull();
                completedExecution.Status.ShouldBe(Core.Domain.TourExecutions.TourExecutionStatus.Abandoned);
                completedExecution.LastActivity.ShouldNotBeNull();
            }
            else
            {
                // Act & Assert
                var exception = Assert.Throws<ArgumentException>(() => controller.AbandonTourExecution(executionId));
                exception.Message.ShouldBe("Invalid end status.");
            }
        }

        [Theory]
        [InlineData(-5, -1, 200)]
        public void CompleteKeyPoint(int executionId, int keyPointId, int expectedResponseCode)
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            var result = (ObjectResult)controller.CompleteKeyPoint(executionId, keyPointId).Result;

            // Assert - Response
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(expectedResponseCode);

            // Assert - Database
            var storedEntity = dbContext.TourExecution.FirstOrDefault(t => t.Id == executionId);
            var rating = storedEntity.CompletedKeys.FirstOrDefault(t => t.KeyPointId == keyPointId);
            rating.ShouldNotBeNull();
        }



        private static TourExecutionController CreateController(IServiceScope scope)
        {
            return new TourExecutionController(scope.ServiceProvider.GetRequiredService<ITourExecutionService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
