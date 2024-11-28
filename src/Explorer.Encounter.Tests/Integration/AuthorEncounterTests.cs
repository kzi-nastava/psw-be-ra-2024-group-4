using Explorer.API.Controllers.Encounter;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounter.API.Dtos.Explorer.Encounters.API.Dtos;
using Explorer.Encounter.API.Public;
using Explorer.Encounter.Infrastructure.Database;
using Explorer.Encounters.Tests;
using FluentResults;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Encounter.Tests.Integration;

[Collection("Sequential")]
public class AuthorEncounterTests : BaseEncountersIntegrationTest
{
    public AuthorEncounterTests(EncountersTestFactory factory) : base(factory)
    {
    }

    [Fact]
    public void CreateEncounter()
    {
        using var scope = Factory.Services.CreateScope();
        var controller = CreateEncounterController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<EncounterContext>();

        EncounterDto encounter = new EncounterDto()
        {
            Id = -4,
            Title = "NewEncounter",
            Description = "Description of Encounter",
            Latitude = 45.234234,
            Longitude = 20.00034,
            XP = 100,
            Status = 0,
            Type = 0,
            SocialData = new API.Dtos.SocialDataDto()
            {
                Radius = 100,
                RequiredParticipants = 5
            },
            HiddenLocationData = null,
            MiscData = null,
            Instances = null,
            IsRequired = true,
        };

        // Act
        var result = ((ObjectResult)controller.Create(encounter).Result)?.Value as EncounterDto;

        // Assert - Response
        result.ShouldNotBeNull();
        result.Id.ShouldBe(-4);
        result.Title.ShouldBe("NewEncounter");

        var storedEntity = dbContext.Encounters.FirstOrDefault(e => e.Title == "NewEncounter");
        storedEntity.ShouldNotBeNull();
    }

    [Fact]
    public void GetByLatLong()
    {
        using var scope = Factory.Services.CreateScope();
        var controller = CreateEncounterController(scope);

        var result = ((ObjectResult)controller.GetByLatLong(45.0, 10.0).Result)?.Value as EncounterDto;

        result.ShouldNotBeNull();
        result.Id.ShouldBe(-3);
    }

    private static EncounterController CreateEncounterController(IServiceScope scope)
    {
        return new EncounterController(scope.ServiceProvider.GetRequiredService<IEncounterService>(), scope.ServiceProvider.GetRequiredService<IImageService>(), scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>())
        {
            ControllerContext = BuildContext("-1")
        };
    }
}

