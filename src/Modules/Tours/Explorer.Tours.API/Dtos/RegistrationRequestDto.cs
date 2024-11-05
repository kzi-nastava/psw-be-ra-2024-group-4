
namespace Explorer.Tours.API.Dtos
{
    public enum Status { Pending, Approved, Declined }
    public class RegistrationRequestDto
    {
        public long Id { get; set; }
        public long UserId {  get; set; }
        public long ObjectId { get; set; }
        public long KeyPointId { get; set; }
        public Status Status { get; set; }

        public RegistrationRequestDto() { }
    }
}
