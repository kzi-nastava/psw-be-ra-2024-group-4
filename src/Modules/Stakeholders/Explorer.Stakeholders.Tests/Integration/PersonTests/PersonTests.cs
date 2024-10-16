
using Explorer.API.Controllers.Person;
using Explorer.Stakeholders.API.Dtos; 
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Infrastructure.Database;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System.Xml.Linq;

namespace Explorer.Stakeholders.Tests.Integration.PersonTests;

[Collection("Sequential")]
public class PersonTests : BaseStakeholdersIntegrationTest
{
    public PersonTests(StakeholdersTestFactory factory) : base(factory) { }

    [Fact]
    public void Updates()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
        var updatedEntity = new PersonUpdateDto
        {
            Id = -11,
            UserId = -11,
            Name = "string",
            Surname = "string",
            Email = "string@gmail.com",
            ProfilePicture = "string",
            Biography = "string",
            Motto = "string"
        };

        // Act
        var result = ((ObjectResult)controller.Update(updatedEntity).Result)?.Value as PersonUpdateDto;

        // Assert - Response
        result.ShouldNotBeNull();
        result.Id.ShouldBe(updatedEntity.Id);
        result.UserId.ShouldBe(updatedEntity.UserId);
        result.Name.ShouldBe(updatedEntity.Name);
        result.Surname.ShouldBe(updatedEntity.Surname);
        result.Email.ShouldBe(updatedEntity.Email);
        result.ProfilePicture.ShouldBe(updatedEntity.ProfilePicture);
        result.Biography.ShouldBe(updatedEntity.Biography);
        result.Motto.ShouldBe(updatedEntity.Motto);

        // Assert - Database
        var storedEntity = dbContext.People.FirstOrDefault(i => i.Email == updatedEntity.Email);
        storedEntity.Id.ShouldBe(updatedEntity.Id);
        storedEntity.ShouldNotBeNull();
        storedEntity.Name.ShouldBe(updatedEntity.Name);
        storedEntity.Surname.ShouldBe(updatedEntity.Surname);
        storedEntity.Biography.ShouldBe(updatedEntity.Biography);

        var oldEntity = dbContext.People.FirstOrDefault(i => i.Email == "string@gmail.com");
        oldEntity.ShouldBeNull();
    }

    [Fact]
    public void GetPersonById()
    {
        //Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);

        // Act
        //var result = ((ObjectResult)controller.Get(-11).Result)?.Value as PersonUpdateDto;

        var actionResult = controller.Get(-11);
        // 1. Poziv metode i pristupanje rezultatu
        var objectResult = actionResult as ObjectResult;               // 2. Kastovanje u ObjectResult
        var result = objectResult?.Value as PersonUpdateDto;  // 3. Pristup vrednosti i kastovanje u PersonUpdateDto


        // Assert
        result.ShouldNotBeNull();
        result.UserId.ShouldBe(-11);
        result.Name.ShouldBe("string");
        result.Surname.ShouldBe("string");
        result.Email.ShouldBe("string@gmailcom");
        result.ProfilePicture.ShouldBe("string");
        result.Biography.ShouldBe("string");
        result.Motto.ShouldBe("string");
    }


    private static PersonController CreateController(IServiceScope scope)
    {
        return new PersonController(scope.ServiceProvider.GetRequiredService<IPersonService>())
        {
            ControllerContext = BuildContext("-1")
        };
    }
}
