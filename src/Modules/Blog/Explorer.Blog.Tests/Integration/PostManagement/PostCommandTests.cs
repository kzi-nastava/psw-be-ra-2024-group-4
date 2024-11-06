using Explorer.API.Controllers.Author.PostManagement;
using Explorer.API.Controllers.Tourist.BlogFeedback;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.Blog.Infrastructure.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using Shouldly;

namespace Explorer.Blog.Tests.Integration.PostManagement
{
    [Collection("Sequential")]
    public class PostCommandTests:BaseBlogIntegrationTest
    {
        public PostCommandTests(BlogTestFactory factory) : base(factory) { }

        [Fact]
        public void Creates()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<BlogContext>();
            var newEntity = new PostDto
            {
                Title = "Tips for Budget Travel",
                Description = "How to travel the world on a budget.",
                CreatedAt = DateTime.UtcNow,
                ImageUrl = "https://example.com/images/budget-travel.jpg",
                Status = BlogStatus.Draft,
                UserId = 3,
                RatingSum = 0
            };

            // Act
            var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as PostDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(0);
            result.Title.ShouldBe(newEntity.Title);
            result.Description.ShouldBe(newEntity.Description);
            result.CreatedAt.ShouldNotBe(default);
            result.Status.ShouldBe(newEntity.Status);
            result.UserId.ShouldBe(newEntity.UserId);
            result.RatingSum.ShouldBe(newEntity.RatingSum);


            // Assert - Database
            var storedEntity = dbContext.Posts.FirstOrDefault(i => i.Title == newEntity.Title); //?
            storedEntity.ShouldNotBeNull();
            storedEntity.Id.ShouldBe(result.Id);
        }

        [Fact]
        public void Create_fails_invalid_data()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedEntity = new PostDto
            {
                Title = "Test"
            };

            // Act
            var result = (ObjectResult)controller.Create(updatedEntity).Result;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(400);
        }

        [Fact]
        public void Updates()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<BlogContext>();
            var updatedEntity = new PostDto
            {
                Id=-1,
                Title = "Exploring the Hills",
                Description = "A journey through the ugly big mountains.",
                CreatedAt = DateTime.UtcNow,
                ImageUrl = "https://example.com/images/budget-travel.jpg",
                Status = BlogStatus.Draft,
                UserId = 5,
                RatingSum = 0,
                Ratings=new List<RatingDto>() 
                {
                    new RatingDto { CreatedAt=DateTime.UtcNow,UserId=1,Value=1 }
                }
            };

            // Act
            var result = ((ObjectResult)controller.Update(updatedEntity).Result)?.Value as PostDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldBe(-1);
            result.Title.ShouldBe(updatedEntity.Title);
            result.Description.ShouldBe(updatedEntity.Description);
            result.CreatedAt.ShouldNotBe(default);
            result.Status.ShouldBe(updatedEntity.Status);
            result.UserId.ShouldBe(updatedEntity.UserId);
            result.Ratings.ShouldNotBeNull();
            result.Ratings.Count.ShouldBe(updatedEntity.Ratings.Count);
            // Assert - Database
            var storedEntity = dbContext.Posts.FirstOrDefault(i => i.Title == "Exploring the Hills");
            storedEntity.ShouldNotBeNull();
            storedEntity.Title.ShouldBe(updatedEntity.Title);
            var oldEntity = dbContext.Posts.FirstOrDefault(i => i.Title == "Exploring the mountains");
            oldEntity.ShouldBeNull();
        }
        [Fact]
        public void Deletes()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<BlogContext>();

            // Act
            var result = (OkResult)controller.Delete(-3);

            // Assert - Response
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);

            // Assert - Database
            var storedCourse = dbContext.Posts.FirstOrDefault(i => i.Id == -3);
            storedCourse.ShouldBeNull();
        }
        [Theory]
        [InlineData(1, -1, 1, 200)]//znaci doda se rating u post sa id -1 od usera 1
        [InlineData(5, -1, -1, 200)]//znaci doda se rating u post sa id -1 od usera 2
        public void Add_rating_to_post(long userId, int postId, int value, int expectedResponseCode)
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateRatingController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<BlogContext>();
            var ratingDto = new RatingDto
            {
                UserId = userId,
                Value = value
            };
            var result = (ObjectResult)controller.AddRating(postId, ratingDto).Result;

            // Assert - Response
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(expectedResponseCode);

            // Assert - Database
            var storedEntity = dbContext.Posts.FirstOrDefault(t => t.Id == postId);
            var rating = storedEntity.Ratings.FirstOrDefault(t => t.UserId == userId);
            rating.ShouldNotBeNull();

        }
        [Theory]
        [InlineData(2, -1, 1, 200)] // ažurira se rating za post sa ID -1 od korisnika 2 sa vrednošću 1
        [InlineData(1, -1, -1, 200)] // ažurira se rating za post sa ID -1 od korisnika 1 sa vrednošću -1
        public void Update_rating_from_post(long userId, int postId, int value, int expectedResponseCode)
        {

            using var scope = Factory.Services.CreateScope();
            var controller = CreateRatingController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<BlogContext>();
            var ratingDto = new RatingDto
            {
                UserId = userId,
                Value = value
            };

            // Act
            var result = controller.UpdateRating(postId, ratingDto);

            // Assert - Response
            result.Result.ShouldNotBeNull();
            result.Result.ShouldBeOfType<OkObjectResult>();
            var okResult = result.Result as OkObjectResult;
            okResult.StatusCode.ShouldBe(expectedResponseCode);

            // Assert - Database
            var storedEntity = dbContext.Posts.FirstOrDefault(t => t.Id == postId);
            storedEntity.ShouldNotBeNull();
            var rating = storedEntity.Ratings.FirstOrDefault(t => t.UserId == userId);
            rating.ShouldNotBeNull(); 
            rating.Value.ShouldBe(value);
        }



        [Fact]
        public void Add_comment_to_post_success()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var commentController = CreateCommentController(scope); 
            var dbContext = scope.ServiceProvider.GetRequiredService<BlogContext>();

            var postId = -1;
            var newComment = new CommentDto
            {
                Text = "Ovo je novi test komentar.",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                UserId = -21, 
                PostId = postId,
                Username = "Testic"
            };

            // Act
            var actionResult = commentController.AddCommentToPost(postId, newComment).Result as OkObjectResult;

            // Assert - Response
            actionResult.ShouldNotBeNull();
            actionResult.StatusCode.ShouldBe(200);

            // Assert - Database
            var storedPost = dbContext.Posts
                .Include(p => p.Comments)
                .FirstOrDefault(p => p.Id == postId);
            storedPost.ShouldNotBeNull();

            var storedComment = storedPost.Comments.FirstOrDefault(c => c.Text == newComment.Text);
            storedComment.ShouldNotBeNull();
            storedComment.Text.ShouldBe(newComment.Text);
            storedComment.UserId.ShouldBe(newComment.UserId);
            storedComment.Username.ShouldBe(newComment.Username);
        }

        [Fact]
        public void Update_comment_success()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateCommentController(scope); 
            var dbContext = scope.ServiceProvider.GetRequiredService<BlogContext>();

            var commentId = -1; 
            var updatedComment = new CommentDto
            {
                Id = commentId,
                Text = "Ovo je ažuriran komentar.",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                UserId = -21,  
                PostId = -1, 
                Username = "TesticUpdated"
            };

            // Act
            var result = (ObjectResult)controller.Update(updatedComment).Result;

            // Assert - Response
            var updatedCommentResponse = result.Value as CommentDto;
            result.ShouldNotBeNull();
            updatedCommentResponse.ShouldNotBeNull(); 
            updatedCommentResponse.Id.ShouldBe(updatedComment.Id);
            updatedCommentResponse.Text.ShouldBe(updatedComment.Text);

            // Assert - Database
            var storedComment = dbContext.Comments.FirstOrDefault(c => c.Text == "Ovo je ažuriran komentar.");
            storedComment.ShouldNotBeNull();
            var oldEntity = dbContext.Comments.FirstOrDefault(i => i.Text == "This is the first comment.");
            oldEntity.ShouldBeNull();
        }

        [Fact]
        public void Update_comment_fails_invalid_commentId()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateCommentController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<BlogContext>();

            var invalidCommentId = -999; 
            var updatedComment = new CommentDto
            {
                Id = invalidCommentId,
                Text = "Ovo je ažuriran komentar.",
                UpdatedAt = DateTime.UtcNow,
                UserId = -21,
                PostId = -1,
                Username = "TesticUpdated"
            };

            // Act
            var result = controller.Update(updatedComment).Result;

            // Assert - Response
            var objectResult = Assert.IsType<ObjectResult>(result);
            objectResult.ShouldNotBeNull();
            objectResult.StatusCode.ShouldBe(404);
        }

        [Fact]
        public void Delete_comment_success()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateCommentController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<BlogContext>();

            var commentId = -1; 

            // Act
            var result = (OkResult)controller.Delete(commentId);

            // Assert - Response
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);

            // Assert - Database
            var deletedComment = dbContext.Comments.FirstOrDefault(c => c.Id == commentId);
            deletedComment.ShouldBeNull(); 
        }

        [Fact]
        public void Delete_comment_fails_invalid_commentId()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateCommentController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<BlogContext>();

            var invalidCommentId = -999; 

            // Act
            var result = controller.Delete(invalidCommentId);

            // Assert - Response
            var objectResult = Assert.IsType<ObjectResult>(result);
            objectResult.ShouldNotBeNull();
            objectResult.StatusCode.ShouldBe(404);
        }


        private static PostController CreateController(IServiceScope scope)
        {
            return new PostController(scope.ServiceProvider.GetRequiredService<IPostService>(), scope.ServiceProvider.GetRequiredService<ICommentService>(),scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>())
            {
                ControllerContext = BuildContext("-1")
            };
        }

        private static CommentController CreateCommentController(IServiceScope scope)
        {
            return new CommentController(scope.ServiceProvider.GetRequiredService<ICommentService>(),scope.ServiceProvider.GetRequiredService<IPostService>())
            { ControllerContext = BuildContext("-1") };
        }
        private static RatingController CreateRatingController(IServiceScope scope)
        {
            return new RatingController(scope.ServiceProvider.GetRequiredService<IPostService>())
            { ControllerContext = BuildContext("-1") };
        }
    }
}
