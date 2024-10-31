using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Explorer.Blog.Core.Domain
{
    public class Rating : ValueObject
    {
        public int UserId { get; private set; }
        public int Value {  get; private set; }
        public DateTime CreatedAt { get; private set; }
        
        [JsonConstructor]
        public Rating(int userId,int value,DateTime createdAt)
        {
            UserId = userId;
            Value = value;
            CreatedAt = createdAt;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return UserId;
            yield return Value;
            yield return CreatedAt;
        }
    }
}
