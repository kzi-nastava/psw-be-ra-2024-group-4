using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.Domain;

namespace Explorer.Stakeholders.API.Dtos
{
    public class PersonDto
    {
        public int Id { get; set; }
        public long UserId { get; set; }
        public string Name { get; init; }
        public string Surname { get; init; }
        public string Email { get; init; }
        public string ImageUrl { get; set; }
        public string Biography { get; set; }
        public string Motto { get; set; }
        public List<int> Equipment { get; set; }
        public string ImageBase64 { get; set; }
    }
}
