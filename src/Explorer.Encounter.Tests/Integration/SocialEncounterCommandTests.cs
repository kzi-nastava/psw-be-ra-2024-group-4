using Explorer.API.Controllers.Author;
using Explorer.API.Controllers.Tourist;
using Explorer.Encounter.API.Dtos;
using Explorer.Encounter.API.Public;
using Explorer.Encounter.Infrastructure.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System.Security.Claims;
using Explorer.Encounter.API.Dtos.Explorer.Encounters.API.Dtos;
using Microsoft.Extensions.Logging;

namespace Explorer.Encounters.Tests.Integration.SocialEncounter;

[Collection("Sequential")]
public class SocialEncounterCommandTests : BaseEncountersIntegrationTest
{
    public SocialEncounterCommandTests(EncountersTestFactory factory) : base(factory)
    {
    }

    [Fact]
    public void Activate_social_encounter_success()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateEncounterController(scope);

        var contextUser = new ClaimsIdentity(new Claim[] { new Claim("id", "-21") }, "test");
        var context = new DefaultHttpContext()
        {
            User = new ClaimsPrincipal(contextUser)
        };
        controller.ControllerContext = new ControllerContext
        {
            HttpContext = context
        };

        var touristPositionDto = new TouristPositionCreateDto
        {
            TouristId = -21,  
            Longitude = 45.45, 
            Latitude = 45.45   
        };

        // Act
        var result = (ObjectResult)controller.Activate(touristPositionDto, -1).Result;

        // Assert - Response
        result.ShouldNotBeNull();             
        result.StatusCode.ShouldBe(200);   
        result.Value.ShouldNotBeNull();      
    }


    [Fact]
    public void Unsuccessfully_activate_social_encounter()
    {
        using var scope = Factory.Services.CreateScope();
        var controller = CreateEncounterController(scope);
        var contextUser = new ClaimsIdentity(new Claim[] { new Claim("id", "-21") }, "test");

        var context = new DefaultHttpContext()
        {
            User = new ClaimsPrincipal(contextUser)
        };

        controller.ControllerContext = new ControllerContext
        {
            HttpContext = context
        };
        var dbContext = scope.ServiceProvider.GetRequiredService<EncounterContext>();
        var touristPositionDto = new TouristPositionCreateDto
        {
            TouristId = -1,
            Longitude = 46.55,
            Latitude = 45.45
        };
        // Act
        var result = (ObjectResult)controller.Activate(touristPositionDto, -1).Result;

        // Assert - Response
        result.StatusCode.ShouldBe(500);
    }

    [Fact]
    public void Complete_social_encounter_success()
    {
        using var scope = Factory.Services.CreateScope();
        var controller = CreateEncounterController(scope);

        var contextUser = new ClaimsIdentity(new Claim[] { new Claim("id", "-21") }, "test");
        var context = new DefaultHttpContext()
        {
            User = new ClaimsPrincipal(contextUser)
        };
        controller.ControllerContext = new ControllerContext
        {
            HttpContext = context
        };

        // Act
        var result = (ObjectResult)controller.Complete(-2).Result;

        // Assert - Response
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(200);
    }


    [Fact]
    public void Unsuccessfully_complete_social_encounter()
    {
        using var scope = Factory.Services.CreateScope();
        var controller = CreateEncounterController(scope);
        var contextUser = new ClaimsIdentity(new Claim[] { new Claim("id", "-21") }, "test");

        var context = new DefaultHttpContext()
        {
            User = new ClaimsPrincipal(contextUser)
        };

        controller.ControllerContext = new ControllerContext
        {
            HttpContext = context
        };
        var dbContext = scope.ServiceProvider.GetRequiredService<EncounterContext>();
        var touristPositionDto = new TouristPositionCreateDto
        {
            TouristId = -1,
            Longitude = 45.45,
            Latitude = 45.45
        };
        // Act
        var result = (ObjectResult)controller.Complete(-3).Result;

        // Assert - Response
        result.StatusCode.ShouldBe(400);
    }

    private static Explorer.API.Controllers.Encounter.EncounterController CreateEncounterController(IServiceScope scope)
    {
        return new Explorer.API.Controllers.Encounter.EncounterController(scope.ServiceProvider.GetRequiredService<IEncounterService>())
        {
            ControllerContext = BuildContext("-1")
        };
    }

}