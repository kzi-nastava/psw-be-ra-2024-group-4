using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.Core.UseCases;
using FluentResults;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers;

[Route("api/users")]
public class AuthenticationController : BaseApiController
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IImageService _imageService;
    private readonly IUserService _userService;

    public AuthenticationController(IAuthenticationService authenticationService,IImageService imageService, IWebHostEnvironment webHostEnvironment, IUserService userService)
    {
        _authenticationService = authenticationService;
        _imageService = imageService;
        _webHostEnvironment = webHostEnvironment;
        _userService = userService;
    }

    [HttpPost]
    public ActionResult<AuthenticationTokensDto> RegisterTourist([FromBody] AccountRegistrationDto account)
    {
        if (!string.IsNullOrEmpty(account.ImageBase64))
        {
            // Konvertovanje base64 stringa u niz bajtova (posle uklanjanja prefiksa tipa podataka ako je potrebno)
            var imageData = Convert.FromBase64String(account.ImageBase64.Split(',')[1]);

            // Definišite putanju foldera za čuvanje slike
            var folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "person");

            // Sačuvajte sliku koristeći pomoćnu servisnu metodu
            account.ProfilePicture = _imageService.SaveImage(folderPath, imageData, "person");
        }

        var result = _authenticationService.RegisterTourist(account);
        return CreateResponse(result);
    }

    [HttpPost("login")]
    public ActionResult<AuthenticationTokensDto> Login([FromBody] CredentialsDto credentials)
    {
        var result = _authenticationService.Login(credentials);

        if (result.IsFailed)
        {
            return BadRequest("Invalid username or password.");
        }
        return Ok(result.Value);
    }


    [HttpGet("check-username/{username}")]
    public ActionResult<bool> CheckUsername(string username)
    {
        var result = _userService.ExistsByUsername(username);
        if (result.IsSuccess)
        {
            return Ok(result.Value); 
        }
        return BadRequest(result.Errors);
    }
 
}