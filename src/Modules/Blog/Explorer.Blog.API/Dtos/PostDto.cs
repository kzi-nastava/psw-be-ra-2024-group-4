using System;

namespace Explorer.Blog.API.Dtos
{
    public class PostDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ImageUrl { get; set; }
        public long UserId { get; set; }
        public Status Status { get; set; }
    }

    public enum Status
    {
        Draft,
        Published,
        Closed
    }
}
