using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Stakeholders.Core.Domain.ProfileMessaging
{
    public class ProfileMessage : Entity
    {
        public enum MessageType { FOLLOWERS, CLUB }
        public long UserId { get; set; }
        public string Message { get; set; }
        public MessageType Type { get; set; }
        //public Resource Attachment { get; set; } 


        public ProfileMessage()
        {

        }
    }
}
