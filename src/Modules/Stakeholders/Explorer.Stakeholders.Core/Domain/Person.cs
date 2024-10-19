using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Tours.Core.Domain;
using System.Net.Mail;

namespace Explorer.Stakeholders.Core.Domain;

public class Person : Entity
{
    public long UserId { get; init; }
    public string Name { get; init; }
    public string Surname { get; init; }
    public string Email { get; init; }
    public string ImageUrl {  get; private set; }
    public string Biography { get; private set; }
    public string Moto { get; private set; }
    public List<int> Equipment {  get; private set; }


    public Person(long userId, string name, string surname, string email, string imageUrl, string biography, string moto)
    {
        UserId = userId;
        Name = name;
        Surname = surname;
        Email = email;
        ImageUrl = imageUrl;
        Biography = biography;
        Moto = moto;
        //if(equipment == null)
        //{
        //    Equipment = new List<Equipment>();
        //}
        //else
        //{
        //    Equipment = equipment;
        //}
        Validate();
    }

    private void Validate()
    {
        if (UserId == 0) throw new ArgumentException("Invalid UserId");
        if (string.IsNullOrWhiteSpace(Name)) throw new ArgumentException("Invalid Name");
        if (string.IsNullOrWhiteSpace(Surname)) throw new ArgumentException("Invalid Surname");
        if (!MailAddress.TryCreate(Email, out _)) throw new ArgumentException("Invalid Email");
    }
}