using Explorer.Tours.Core.Domain;

namespace Explorer.Stakeholders.API.Dtos;

public class AccountRegistrationDto
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string ImageUrl { get;  set; }
    public string Biography { get;  set; }
    public string Moto { get;  set; }
    public List<Equipment> Equipment { get;  set; }
}