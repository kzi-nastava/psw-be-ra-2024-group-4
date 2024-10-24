using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shouldly;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Cryptography.X509Certificates;
using Explorer.API.Controllers.Tourist;
using Explorer.Tours.API.Public.Administration;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;

namespace Explorer.Tours.Tests.Integration.Administration {
    [Collection("Sequential")]
    public class TourPreferencesQueryTest : BaseToursIntegrationTest {
        
        public TourPreferencesQueryTest(ToursTestFactory factory) : base(factory) { }

        [Fact]
        public async void Retrieves_all() {
            //Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            //Act
            //var result = ((ObjectResult)controller.GetAllPreferences().Result)?.Value as OkObjectResult;
            var actionResult = await controller.GetAllPreferences();
            actionResult.ShouldNotBeNull();
            var okResult = actionResult.Result as  OkObjectResult;
            var result = okResult?.Value as List<TourPreferenceDto>;

            //Assert
            result.ShouldNotBeNull();
            result.Count.ShouldBe(2);

        }

        private static TourPreferenceController CreateController(IServiceScope scope) {
            return new TourPreferenceController(scope.ServiceProvider.GetRequiredService<ITourPreferenceService>()) {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
