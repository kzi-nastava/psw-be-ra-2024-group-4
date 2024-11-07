using Explorer.BuildingBlocks.Core.Domain;
using System.Text.Json.Serialization;

namespace Explorer.Stakeholders.Core.Domain.ProfileMessages
{
    public class Resource : ValueObject
    {
        public enum ResourceType { TOUR, BLOG }
        public ResourceType Type { get; }
        public long EntityId { get; }

        [JsonConstructor]
        public Resource(ResourceType type, long entityId) 
        {
            Type = type;
            EntityId = entityId;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Type;
            yield return EntityId;
        }
    }
}
