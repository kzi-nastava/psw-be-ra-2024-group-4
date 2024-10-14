using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.Core.Domain
{
    public class Post : Entity
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public string ImageUrl {  get; private set; }
        public Status Status { get; private set; } 
        public long UserId { get; private set; }
        

        public Post(string title, string description, DateTime createdAt, string imageUrl, Status status, long userId)
        {
            Title = title;
            Description = description;
            CreatedAt = createdAt;
            ImageUrl = imageUrl;
            Status = status;
            UserId = userId;
            Validate();
        }
        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(Title))throw new ArgumentException("Invalid Title");
            if (string.IsNullOrWhiteSpace(Description)) throw new ArgumentException("Invalid Description");
            if (CreatedAt == default(DateTime))throw new ArgumentException("Invalid CreatedAt date");
            if (string.IsNullOrWhiteSpace(ImageUrl))throw new ArgumentException("Invalid ImageUrl");
            if (!Enum.IsDefined(typeof(Status), Status)) throw new ArgumentException("Invalid Status");
            if (UserId == 0) throw new ArgumentException("Invalid UserId");
        }

    }
    public enum Status
    {
        Draft,
        Published,
        Closed
    }
}