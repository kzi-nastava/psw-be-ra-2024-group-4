using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Stakeholders.Core.Domain.ProfileMessaging
{
    public class Resource : ValueObject
    {
        public enum ResourceType { TOUR, BLOG }
        public ResourceType Type { get; set; }
        public long EntityId { get; set; }

        public Resource(ResourceType type, long entityId) 
        {
            Type = type;
            EntityId = entityId;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }
    }
}
