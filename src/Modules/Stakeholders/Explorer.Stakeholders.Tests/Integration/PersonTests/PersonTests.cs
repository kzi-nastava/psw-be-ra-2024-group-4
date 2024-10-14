
using Explorer.API.Controllers.Person;
using Explorer.Stakeholders.API.Dtos; 
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Infrastructure.Database;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

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
            Id = 5,
            UserId = 5,
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

    //[Fact]
    //public void GetById_Returns_CorrectPersonFromDatabase()
    //{
    //    // Arrange
    //    using var scope = Factory.Services.CreateScope();
    //    var controller = CreateController(scope);
    //    var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();

    //    var existingPersonId = 5;

    //    // Act - pozivamo Get metodu
    //    var result = controller.Get(existingPersonId); // Očekujemo Result<PersonUpdateDto>

    //    // Assert
    //    result.ShouldNotBeNull();
    //    result.IsSuccess.ShouldBeTrue(); // Proverava da li je operacija bila uspešna

    //    var personDto = result.Value; // Dobijanje DTO objekta iz rezultata
    //    personDto.ShouldNotBeNull();
    //    personDto.Id.ShouldBe(existingPersonId);

    //    var storedEntity = dbContext.People.FirstOrDefault(i => i.Id == existingPersonId);
    //    storedEntity.ShouldNotBeNull();
    //    personDto.Name.ShouldBe(storedEntity.Name);
    //    personDto.Surname.ShouldBe(storedEntity.Surname);
    //    personDto.Email.ShouldBe(storedEntity.Email);
    //    personDto.ProfilePicture.ShouldBe(storedEntity.ProfilePicture);
    //    personDto.Biography.ShouldBe(storedEntity.Biography);
    //    personDto.Motto.ShouldBe(storedEntity.Motto);
    //}


    private static PersonController CreateController(IServiceScope scope)
    {
        return new PersonController(scope.ServiceProvider.GetRequiredService<IPersonService>())
        {
            ControllerContext = BuildContext("-1")
        };
    }
}
