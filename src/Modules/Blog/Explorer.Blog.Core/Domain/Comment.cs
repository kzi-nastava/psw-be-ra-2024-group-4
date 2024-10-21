using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace Explorer.Blog.Core.Domain;

    public class Comment: Entity
    {
        public string Text { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set;}
        public long UserId { get; private set; }
        public long PostId { get; private set; }

    public Comment(string text, DateTime createdAt, DateTime updatedAt, long userId, long postId)
    {
        Text = text;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        UserId = userId;
        PostId = postId;
        Validate();
    }
    private void Validate() {

        if (string.IsNullOrWhiteSpace(Text)) throw new ArgumentException("Invalid Text.");
        if (CreatedAt >= DateTime.UtcNow) throw new ArgumentException("Invalid CreatedAt date");
        if (UpdatedAt == default(DateTime)) throw new ArgumentException("Invalid UpdatedAt date");
        if (UserId == 0) throw new ArgumentException("Invalid UserId.");
        if (PostId == 0) throw new ArgumentException("Invalid PostId.");
    }
}

