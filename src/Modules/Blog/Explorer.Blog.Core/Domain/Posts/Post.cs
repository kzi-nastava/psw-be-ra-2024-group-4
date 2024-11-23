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
        public int RatingSum {  get; private set; }
        public List<Comment> Comments { get; private set; }
        public List<Rating> Ratings { get; private set; }


        public Post(string title, string description, DateTime createdAt, string imageUrl, BlogStatus status, long userId,int ratingSum)
        {
            Title = title;
            Description = description;
            CreatedAt = createdAt;
            ImageUrl = imageUrl;
            Status = status;
            UserId = userId;
            RatingSum = ratingSum;
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
        public void AddRating(int value, long userId)
        {
            Rating? ratingCheck= Ratings.FirstOrDefault(r => r.UserId == userId);
            if (ratingCheck != null) throw new ArgumentException("Rating from this user already exists");
            else Ratings.Add(new Rating(userId, value, DateTime.Now));
        }
        public void DeleteRating(long userId)
        {
            Rating? rating= Ratings.FirstOrDefault(rating => rating.UserId == userId);
            if (rating != null) Ratings.Remove(rating);
        }
        public void TotalRating()
        {
            RatingSum = Ratings.Sum(v => v.Value);
        }
        public void AddComment(Comment comment)
        {
            if (Comments.Any(c => c.Id == comment.Id))
                throw new ArgumentException("Comment already exists.");
            Comments.Add(comment);
        }

        public void DeleteComment(long commentId)
        {
            var comment = Comments.FirstOrDefault(c => c.Id == commentId) ?? throw new KeyNotFoundException("Comment not found.");
            Comments.Remove(comment);
        }

        public void UpdateComment(Comment updatedComment)
        {
            var comment = Comments.FirstOrDefault(c => c.Id == updatedComment.Id) ?? throw new KeyNotFoundException("Comment not found in the post.");
            comment.Update(updatedComment.UserId,updatedComment.Text, updatedComment.Username, updatedComment.UpdatedAt, updatedComment.Username);
        }

        public void UpdateStatus()
        {
            var commentCount= Comments.Count;
            if (RatingSum < -5) { Status = BlogStatus.Closed; }
            else if (RatingSum > 5 && commentCount > 10 && Status != BlogStatus.Active) { Status = BlogStatus.Active; } //treba 100 i 10
            else if(RatingSum >7 && commentCount>15) { Status = BlogStatus.Famous; } //treba 500 i 30
        }
        public void Publish()
        {
            Status = BlogStatus.Published;
        }
    }
    public enum BlogStatus
    {
        Draft,
        Published,
        Closed,
        Active,
        Famous
    }

   
}