
using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Tours.Core.Domain
{
    public enum Status { Pending, Approved, Declined }
    public class RegistrationRequest : Entity
    {
        public long UserId { get; private set; }
        public long ObjectId { get; private set; }
        public long KeyPointId {  get; private set; }
        public Status Status { get; private set; }

        public RegistrationRequest() { }
        public RegistrationRequest(long userId, long objectId, long keyPointId, Status status)
        {
            UserId = userId;
            ObjectId = objectId;
            KeyPointId = keyPointId;
            Status = status;
        }
    }
}
