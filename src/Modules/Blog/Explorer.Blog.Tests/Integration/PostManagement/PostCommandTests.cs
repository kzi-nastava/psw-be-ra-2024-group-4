﻿using Explorer.API.Controllers.Author.PostManagement;
using Explorer.API.Controllers.Tourist.BlogFeedback;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.Blog.Infrastructure.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
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
    }
}
