using Explorer.API.Controllers.Administrator.Administration;
using Explorer.API.Controllers.Tourist.Comments;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.Blog.Infrastructure.Database;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.Tests.Integration
{
    [Collection("Sequential")]
    public class CommentCommandTests : BaseBlogIntegrationTest
    {

        public CommentCommandTests(BlogTestFactory factory) : base(factory) { }

        [Fact]
        public void CreatesComment()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<BlogContext>();

            var newComment = new CommentDto
            {
                Text = "Ovo je test komentar.",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                UserId = 1,
                PostId = 1,
                Username= "Testic"
            };

            // Act
            var result = ((ObjectResult)controller.Create(newComment).Result)?.Value as CommentDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(0); // Proverava da li je komentar kreiran
            result.Text.ShouldBe(newComment.Text);
            result.UserId.ShouldBe(newComment.UserId);
            result.PostId.ShouldBe(newComment.PostId);
            result.CreatedAt.ShouldNotBe(default);
            result.UpdatedAt.ShouldNotBe(default);
            result.Username.ShouldBe(newComment.Username);
            // Assert - Database
            var storedComment = dbContext.Comments.FirstOrDefault(c => c.Text == newComment.Text);
            storedComment.ShouldNotBeNull();
            storedComment.Id.ShouldBe(result.Id);
           
        }

        [Fact]
        public void Create_fails_invalid_data()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedEntity = new CommentDto
            {
                Text = "Test"
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
            var updatedEntity = new CommentDto
            {
                Id = -1,
                Text = "Ovo je prvi.",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                UserId = 1,
                PostId = 1,
                Username= "Testici"

            };

            // Act
            var result = ((ObjectResult)controller.Update(updatedEntity).Result)?.Value as CommentDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldBe(-1);
            result.Text.ShouldBe(updatedEntity.Text);

            // Assert - Database
            var storedEntity = dbContext.Comments.FirstOrDefault(i => i.Text == "Ovo je prvi.");
            storedEntity.ShouldNotBeNull();
            var oldEntity = dbContext.Comments.FirstOrDefault(i => i.Text == "This is the first comment.");
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
            var result = (OkResult)controller.Delete(-2);

            // Assert - Response
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);

            // Assert - Database
            var storedCourse = dbContext.Comments.FirstOrDefault(i => i.Id == -2);
            storedCourse.ShouldBeNull();
        }

        [Fact]
        public void Delete_fails_invalid_id()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            // Act
            var result = (ObjectResult)controller.Delete(-10);

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(404);
        }
        private static CommentController CreateController(IServiceScope scope)
        {
            return new CommentController(scope.ServiceProvider.GetRequiredService<ICommentService>(), scope.ServiceProvider.GetRequiredService<IPostService>())

            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
