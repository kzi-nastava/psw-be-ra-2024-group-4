using Explorer.API.Controllers.Administrator;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.TourAuthoring.KeypointAddition;
using Explorer.Tours.API.Public.TourAuthoring.ObjectAddition;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Stakeholders.Tests.Integration
{
    [Collection("Sequential")]
    public class RequestedPublicTests : BaseStakeholdersIntegrationTest
    {
        public RequestedPublicTests(StakeholdersTestFactory factory) : base(factory) { }

        [Fact]
        public void GetRequestedPublicObject()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            var result = ((ObjectResult)controller.GetRequestedPublicObject().Result)?.Value as List<ObjectDTO>;
            
            result.ShouldNotBeNull();
            result[0].Id.ShouldBe(-3);
            result[0].Name.ShouldBe("WC Kalemegdan");
        }

        [Fact]
        public void GetRequestedPublicKeyPoint()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            var result = ((ObjectResult)controller.GetRequestedPublicKeyPoint().Result)?.Value as List<KeyPointDto>;

            result.ShouldNotBeNull();
            result[0].Id.ShouldBe(-3);
            result[0].Name.ShouldBe("Sistine Chapel, Vatican City");
        }

        [Fact]
        public void UpdateObject()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            var updatedObject = new ObjectDTO
            {
                Id = -3,
                Name = "WC",
                Description = "Opis",
                Image = "slika.jpg",
                Category = (ObjectCategory)1,
                Longitude = 11,
                Latitude = -15,
                UserId = 1003,
                PublicStatus = (PublicStatus)2,
                
            };

            var result = ((ObjectResult)controller.UpdateObject(updatedObject).Result)?.Value as ObjectDTO;

            result.ShouldNotBeNull();
            result.Id.ShouldBe(-3);
            result.PublicStatus.ShouldBe((PublicStatus)2);

            var storedObject = dbContext.Objects.FirstOrDefault(o => o.Name == updatedObject.Name);
            storedObject.ShouldNotBeNull();
            storedObject.Description.ShouldBe(updatedObject.Description);
        }

        [Fact]
        public void UpdateKeyPoint()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            var updatedKeyPoint = new KeyPointDto
            {
                Id = -3,
                Name = "Sistine Chapel, Vatican City",
                Longitude = 11,
                Latitude = 11,
                Description = "opis",
                Image = "Slika1.jpg",
                UserId = 1003,
                TourId = -1,
                ImageBase64 = "image.jpg",
                PublicStatus = (PublicStatus)2
            };

            var result = ((ObjectResult)controller.UpdateKeyPoint(updatedKeyPoint).Result)?.Value as KeyPointDto;

            result.ShouldNotBeNull();
            result.Id.ShouldBe(-3);
            result.PublicStatus.ShouldBe((PublicStatus)2);

            var storedObject = dbContext.KeyPoints.FirstOrDefault(o => o.Name == updatedKeyPoint.Name);
            storedObject.ShouldNotBeNull();
            storedObject.Description.ShouldBe(updatedKeyPoint.Description);
        }

        private static RequestedPublicController CreateController(IServiceScope scope)
        {
            return new RequestedPublicController(scope.ServiceProvider.GetRequiredService<IObjectService>(), scope.ServiceProvider.GetRequiredService<IKeyPointService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
