using Explorer.Blog.API.Dtos;
using Explorer.BuildingBlocks.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace Explorer.Blog.Core.Domain.Posts
{
    public class Post : Entity
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public string ImageUrl { get; private set; }
        public BlogStatus Status { get; private set; }
        public long UserId { get; private set; }
        public List<Comment> Comments { get; private set; }
        public List<Rating> Ratings { get; private set; }


        public Post(string title, string description, DateTime createdAt, string imageUrl, BlogStatus status, long userId)
        {
            Title = title;
            Description = description;
            CreatedAt = createdAt;
            ImageUrl = imageUrl;
            Status = status;
            UserId = userId;
            Comments = new List<Comment>();
            Ratings= new List<Rating>();
            Validate();
        }
        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(Title)) throw new ArgumentException("Invalid Title");
            if (string.IsNullOrWhiteSpace(Description)) throw new ArgumentException("Invalid Description");
            if (CreatedAt == default) throw new ArgumentException("Invalid CreatedAt date");
            if (!Enum.IsDefined(typeof(BlogStatus), Status)) throw new ArgumentException("Invalid Status");
            if (UserId == 0) throw new ArgumentException("Invalid UserId");
        }

        public void AddComment(Comment comment)
        {
            if (Comments.Any(c => c.Id == comment.Id))
                throw new ArgumentException("Comment already exists.");
            Comments.Add(comment);
        }

        public void DeleteComment(long commentId)
        {
            var comment = Comments.FirstOrDefault(c => c.Id == commentId);
            if (comment == null)
                throw new KeyNotFoundException("Comment not found.");
            Comments.Remove(comment);
        }
      
        public List<Comment> GetAll()
        {
            return Comments.ToList();
        }
   

    }
    public enum BlogStatus
    {
        Draft,
        Published,
        Closed
    }

   
}