using Explorer.API.Controllers.Administrator.Administration;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.TourAuthoring;
using Explorer.Tours.Core.Domain.Tours;
using Explorer.Tours.Infrastructure.Database;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Explorer.Tours.Tests.Integration.Administration
{
    [Collection("Sequential")]
    public class TourDeleteCommandTest : BaseToursIntegrationTest
    {
        public TourDeleteCommandTest(ToursTestFactory factory) : base(factory) { }

        [Fact]
        public void DeletesTourSuccessfully()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            var newEntity = new TourDto
            {
                Id = -5,
                Name = "Obuća za grub teren",
                Description = "Patike sa tvrdim đonom i kramponima koje daju stabilnost na neravnom i rastresitom terenu.",
                Difficulty = "Medium",
                Tags = new List<API.Dtos.TourTags> { API.Dtos.TourTags.Adventure, API.Dtos.TourTags.Wildlife },
                Status = API.Dtos.TourStatus.Draft,
                Price = 100.00,
                UserId = 1,
                EquipmentIds = new List<long>(),
                KeyPoints = new List<KeyPointDto>()
            };

            // Act
            var result = controller.DeleteTour(-5);

            // Assert - Response
            var noContentResult = result as NoContentResult;
            noContentResult.StatusCode.ShouldBe(204);

            // Assert - Database
            var storedTour = dbContext.Tour.FirstOrDefault(t => t.Id == -5);
           storedTour.ShouldBeNull();
        }
        
     
        
        private static TourController CreateController(IServiceScope scope)
        {
            return new TourController(scope.ServiceProvider.GetRequiredService<ITourService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}

