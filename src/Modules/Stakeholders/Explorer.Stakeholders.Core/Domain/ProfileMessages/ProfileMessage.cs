using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Stakeholders.Core.Domain.ProfileMessages
{
    public class ProfileMessage : Entity
    {
        public enum MessageType { FOLLOWERS, CLUB }
        public long UserId { get; set; }
        public string Message { get; set; }
        public MessageType Type { get; set; }
        public Resource Resource { get; set; } 
        public long ClubId { get; set; }

        public ProfileMessage(long userId, string message, MessageType type, Resource resource, long clubId)
        {
            UserId = userId;
            Message = message.Length > 0 && message.Length <= 280 ? message : throw new ArgumentException("Invalid Message");
            Type = type;
            Resource = resource;
            ClubId = clubId;
        }


    }
}
