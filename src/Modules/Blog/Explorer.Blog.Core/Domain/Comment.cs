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
        public string Username { get; private set; }

         public Comment(string text, DateTime createdAt, DateTime updatedAt, long userId, long postId, string username)
         {
            Text = text;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            UserId = userId;
            PostId = postId;
            Username = username;
            Validate();
         }
    private void Validate() 
      {

        if (string.IsNullOrWhiteSpace(Text)) throw new ArgumentException("Invalid Text.");
        if (CreatedAt >= DateTime.UtcNow) throw new ArgumentException("Invalid CreatedAt date");
        if (UpdatedAt == default(DateTime)) throw new ArgumentException("Invalid UpdatedAt date");
        if (UserId == 0) throw new ArgumentException("Invalid UserId.");
        if (PostId == 0) throw new ArgumentException("Invalid PostId.");
        if (string.IsNullOrWhiteSpace(Username)) throw new ArgumentException("Invalid Username.");
       }

    public void Update(long userId,string newText, string newUsername, DateTime newUpdatedAt,string username)
    {
        if (UserId == 0) throw new ArgumentException("Invalid UserId.");
        if (string.IsNullOrWhiteSpace(newText))
            throw new ArgumentException("Text cannot be empty.");
        if (string.IsNullOrWhiteSpace(newUsername))
            throw new ArgumentException("Username cannot be empty.");
        if (newUpdatedAt == default)
            throw new ArgumentException("Invalid UpdatedAt date.");
        UserId= userId;
        Text = newText;
        Username = newUsername;
        UpdatedAt = newUpdatedAt;
        Username=username;
    }

}

