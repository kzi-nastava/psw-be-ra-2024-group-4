﻿using Explorer.API.Controllers.Tourist;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Infrastructure.Database;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.Infrastructure.Database;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Stakeholders.Tests.Integration.Tourist
{
    [Collection("Sequential")]
    public class TouristCommandTests : BaseStakeholdersIntegrationTest
    {
        public TouristCommandTests(StakeholdersTestFactory factory) : base(factory) { }



        [Fact]
        public void GetPersonById()
        {
            //Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            // Act
            var actionResult = controller.Get(-11);
            var objectResult = actionResult as ObjectResult;
            var result = objectResult?.Value as PersonDto;
            // Assert - Response
            result.ShouldNotBeNull();
            result.UserId.ShouldBe(-11);
            result.Name.ShouldBe("Ana");
            result.Surname.ShouldBe("Anić");
            result.Email.ShouldBe("autor1@gmail.com");
            result.ImageUrl.ShouldBe("https1");
            result.Biography.ShouldBe("KaoJa");
            result.Moto.ShouldBe("Samo Jako Bro"); 
            result.Equipment.ShouldBe((new[] { 1, 2 }).ToArray());
        }

        [Fact]
        public void Updates()
        {
            //Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
            var updatedEntity = new PersonDto
            {
                Id = -11,
                UserId = -11,
                Name = "Uros",
                Surname = "S",
                Email = "uros@gmailo.com",
                ImageUrl = "https1",
                Biography = "KaoJa",
                Moto = "Preko mora i okeana",
                Equipment = new() { 1,2 ,3},
            };
            var actionResult = controller.Update(updatedEntity);
            var objectResult = actionResult as ObjectResult;
            var result = objectResult?.Value as PersonDto;

            //Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(-11);
            result.Name.ShouldBe(updatedEntity.Name);
            result.Equipment.ShouldBe(new List<int>() { 1, 2, 3 }.ToArray());
        }
        private static TouristController CreateController(IServiceScope scope)
        {
            return new TouristController(scope.ServiceProvider.GetRequiredService<IPersonService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
