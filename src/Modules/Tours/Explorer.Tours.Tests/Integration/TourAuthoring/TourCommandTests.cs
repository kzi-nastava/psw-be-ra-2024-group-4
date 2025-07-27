using Explorer.API.Controllers.Administrator.Administration;
using Explorer.API.Controllers.Author;
using Explorer.API.Controllers.Author.TourAuthoring;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.API.Public.TourAuthoring;
using Explorer.Tours.Infrastructure.Database;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourController = Explorer.API.Controllers.Author.TourAuthoring.TourController;

namespace Explorer.Tours.Tests
{
    [Collection("Sequential")]
    public class TourCommandTests : BaseToursIntegrationTest
    {
        public TourCommandTests(ToursTestFactory factory) : base(factory) { }

        [Fact]
        public void Creates()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            var newEntity = new TourDto
            {
                Id = -3,
                Name = "Obuća za grub teren",
                Description = "Patike sa tvrdim đonom i kramponima koje daju stabilnost na neravnom i rastresitom terenu.",
                Difficulty = "Medium",
                Tags = new List<TourTags> { TourTags.Adventure, TourTags.Wildlife },
                Status = TourStatus.Draft,
                Price = 100.00,
                UserId = 1,
                EquipmentIds = new List<long>()
             
            };

            // Act
            var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as TourDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(0);
            result.Name.ShouldBe(newEntity.Name);

            // Assert - Database
            var storedEntity = dbContext.Tour.FirstOrDefault(i => i.Name == newEntity.Name);
            storedEntity.ShouldNotBeNull();
            //storedEntity.Id.ShouldBe(result.Id);
        }

        [Fact]
        public void Create_fails_invalid_data()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedEntity = new TourDto
            {
                Name = "Obuća za grub teren",
            };

            // Act
            var result = (ObjectResult)controller.Create(updatedEntity).Result;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(400);
        }

        [Fact]
        public void Add_equipment_to_tour_success()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            var tourId = -1;
            var equipmentIds = new List<long> { -3, -4 };

            // Act
            var result = (OkResult)controller.AddEquipmentToTour(tourId, equipmentIds);

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);

            // Assert-Database
            var equipmentIdss = dbContext.Tour
            .Where(t => t.Id == tourId)
            .ToList() 
            .SelectMany(t => t.EquipmentIds) 
            .ToList();


            var tour_eq = dbContext.Equipment
                .Where(e => equipmentIds.Contains(e.Id))
                .ToList();

            tour_eq.ShouldNotBeNull();
            tour_eq.Count().ShouldBe(1);
        }

        [Fact]
        public void Add_equipment_to_tour_fails_invalid_tourId()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            var tourId = -999; 
            var equipmentIds = new List<long> { -3, -4}; 

            // Act
            var result = controller.AddEquipmentToTour(tourId, equipmentIds);

            // Assert
            var objectResult = result.ShouldBeOfType<ObjectResult>();
            objectResult.StatusCode.ShouldBe(404);
        }

        [Fact]
        public void Add_equipment_to_tour_fails_invalid_equipmentIds() {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            // Invalid tour ID and equipment IDs
            var tourId = -2;
            var equipmentIds = new List<long> { -333, -444 };

            foreach (var equipmentId in equipmentIds) {
                dbContext.Equipment.Find(equipmentId).ShouldBeNull();
            }

            // Act
            var result = controller.AddEquipmentToTour(tourId, equipmentIds);

            // Assert
            var objectResult = result.ShouldBeOfType<OkResult>();
            objectResult.StatusCode.ShouldBe(200);
        }


        [Fact]
        public void Remove_equipment_from_tour_success()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            var tourId = -2;
            var equipmentId = -3; 

            // Act
            var result = (OkResult)controller.RemoveEquipmentFromTour(tourId, equipmentId);

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);

            // Assert-Database
            var equipmentIds = dbContext.Tour
                .Where(t => t.Id == tourId)
                .ToList()
                .SelectMany(t => t.EquipmentIds)
                .ToList();

            var tour_eq = dbContext.Equipment
                .Where(e => equipmentIds.Contains(e.Id))
                .ToList();

            tour_eq.ShouldNotBeNull();
            tour_eq.Count().ShouldBe(1); 
        }

        [Fact]
        public void Remove_equipment_from_tour_fails_invalid_tourId()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            var tourId = -999; 
            var equipmentId = -3; 

            // Act
            var result = controller.RemoveEquipmentFromTour(tourId, equipmentId);

            // Assert
            var objectResult = result.ShouldBeOfType<ObjectResult>();
            objectResult.StatusCode.ShouldBe(404); 
        }

        [Fact]
        public void Remove_equipment_from_tour_fails_invalid_equipmentId()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            var tourId = -1; 
            var equipmentId = -999; 

            // Act
            var result = controller.RemoveEquipmentFromTour(tourId, equipmentId);

            // Assert
            var objectResult = result.ShouldBeOfType<ObjectResult>();
            objectResult.StatusCode.ShouldBe(404); 
        }

        [Fact]
        public void Publish_succeeds()
        {
            // Arrange - Input data
            var tourId = -5;
            var expectedResponseCode = 200;
            var expectedStatus = TourStatus.Published;
            var authorId = 2;

            // Arrange - Controller and dbContext
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            // Act
            var result = (OkResult)controller.Publish(tourId).Result;

            // Assert - Response
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(expectedResponseCode);

            // Assert - Database
            var storedEntity = dbContext.Tour.FirstOrDefault(t => t.Id == tourId);
            storedEntity.ShouldNotBeNull();
            storedEntity.Status.ToString().ShouldBe(expectedStatus.ToString());
        }

        [Fact]

        public void Publish_fails_invalid_status()
        {
            // Arrange - Input data
            var tourId = -4;
            var expectedResponseCode = 400;
            var expectedStatus = TourStatus.Archived;
            var authorId = 1;

            // Arrange - Controller and dbContext
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            // Act
            var result = controller.Publish(tourId).Result;


            // Assert - Response
            var objectResult = Assert.IsType<ObjectResult>(result);
            objectResult.ShouldNotBeNull();
            objectResult.StatusCode.ShouldBe(expectedResponseCode);

            // Assert - Database
            var storedEntity = dbContext.Tour.FirstOrDefault(t => t.Id == tourId);
            storedEntity.ShouldNotBeNull();
            storedEntity.Status.ToString().ShouldBe(expectedStatus.ToString());
        }



        [Fact]
        public void Archive_succeeds()
        {
            // Arrange - Input data
            var tourId = -2;
            var expectedResponseCode = 200;
            var expectedStatus = TourStatus.Archived;
            var authorId = 2;

            // Arrange - Controller and dbContext
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            // Act
            var result = (OkResult)controller.Archive(tourId).Result;

            // Assert - Response
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(expectedResponseCode);

            // Assert - Database
            var storedEntity = dbContext.Tour.FirstOrDefault(t => t.Id == tourId);
            storedEntity.ShouldNotBeNull();
            storedEntity.Status.ToString().ShouldBe(expectedStatus.ToString());
        }

        [Fact]
        public void Archive_fails_invalid_status()
        {
            // Arrange - Input data
            var tourId = -1;
            var expectedResponseCode = 400;
            var expectedStatus = TourStatus.Draft;
            var authorId = 1;

            // Arrange - Controller and dbContext
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            // Act
            var result = controller.Archive(tourId).Result;


            // Assert - Response
            var objectResult = Assert.IsType<ObjectResult>(result);
            objectResult.ShouldNotBeNull();
            objectResult.StatusCode.ShouldBe(expectedResponseCode);

            // Assert - Database
            var storedEntity = dbContext.Tour.FirstOrDefault(t => t.Id == tourId);
            storedEntity.ShouldNotBeNull();
            storedEntity.Status.ToString().ShouldBe(expectedStatus.ToString());
        }

        [Fact]
        public void Archive_fails_wrong_author()
        {
            // Arrange - Input data
            var tourId = -1;
            var expectedStatus = TourStatus.Draft;
            var expectedResponseCode = 400;
            var authorId = 5;

            // Arrange - Controller and dbContext
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            // Act
            var result = controller.Archive(tourId).Result;

            // Assert - Response
            var objectResult = Assert.IsType<ObjectResult>(result);
            objectResult.ShouldNotBeNull();
            objectResult.StatusCode.ShouldBe(expectedResponseCode);

            // Assert - Database
            var storedEntity = dbContext.Tour.FirstOrDefault(t => t.Id == tourId);
            storedEntity.ShouldNotBeNull();
            storedEntity.Status.ToString().ShouldBe(expectedStatus.ToString());
        }

        [Fact]
        public void Reactivate_succeeds()
        {
            // Arrange - Input data
            var tourId = -4; 
            var expectedResponseCode = 200; 
            var expectedStatus = TourStatus.Published; 
            var authorId = 2; 

            // Arrange - Controller and dbContext
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            // Act
            var result = (OkResult)controller.Reactivate(tourId).Result;

            // Assert - Response
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(expectedResponseCode);

            // Assert - Database
            var storedEntity = dbContext.Tour.FirstOrDefault(t => t.Id == tourId);
            storedEntity.ShouldNotBeNull();
            storedEntity.Status.ToString().ShouldBe(expectedStatus.ToString());
        }


        [Fact]
        public void Update_Distance_succeeds()
        {
            // Arrange - Input data
            var tourId = -2;
            var expectedResponseCode = 200;
            var expectedLength = 200;
            var authorId = 2;

            // Arrange - Controller and dbContext
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            // Act
            var result = (OkResult)controller.UpdateDistance(tourId, expectedLength).Result;

            // Assert - Response
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(expectedResponseCode);

            // Assert - Database
            var storedEntity = dbContext.Tour.FirstOrDefault(t => t.Id == tourId);
            storedEntity.ShouldNotBeNull();
            storedEntity.LengthInKm.ToString().ShouldBe(expectedLength.ToString());
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
