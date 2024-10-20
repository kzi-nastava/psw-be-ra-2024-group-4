using Explorer.API.Controllers.Administrator.Administration;
using Explorer.API.Controllers.Author;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                EquipmentIds = new List<long>(),
                KeyPointIds = new List<long>()
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
            var equipmentId = -3;

            // Act
            var result = (OkResult)controller.AddEquipmentToTour(tourId, equipmentId);

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
        public void Add_equipment_to_tour_fails_invalid_tourId()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            var tourId = -999; 
            var equipmentId = -3; 

            // Act
            var result = controller.AddEquipmentToTour(tourId, equipmentId);

            // Assert
            var objectResult = result.ShouldBeOfType<ObjectResult>();
            objectResult.StatusCode.ShouldBe(404);
        }

        [Fact]
        public void Add_equipment_to_tour_fails_invalid_equipmentId()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            var tourId = -1; 
            var equipmentId = -999; 

            // Act
            var result = controller.AddEquipmentToTour(tourId, equipmentId);

            // Assert
            var objectResult = result.ShouldBeOfType<ObjectResult>();
            objectResult.StatusCode.ShouldBe(404); 
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


        private static TourController CreateController(IServiceScope scope)
        {
            return new TourController(scope.ServiceProvider.GetRequiredService<ITourService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
