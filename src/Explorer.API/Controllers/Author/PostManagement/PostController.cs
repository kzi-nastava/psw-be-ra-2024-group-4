﻿using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.Core.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;

namespace Explorer.API.Controllers.Author.PostManagement
{
    [Authorize(Policy = "authorPolicy")]

    //[Authorize(Policy = "authorPolicy")]

    [Route("api/postmanagement/post")]
    public class PostController : BaseApiController
    {
        private readonly IPostService _postService;
        private readonly ICommentService _commentService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public PostController(IPostService postService,ICommentService commentService,IWebHostEnvironment webHostEnvironment)
        {
            _postService = postService;
            _commentService = commentService;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet("comments")]
        public ActionResult<PagedResult<CommentDto>> GetAll([FromQuery] int id, [FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _commentService.GetPaged(id, page, pageSize);
            return CreateResponse(result);
        }
        [HttpGet]
        public ActionResult<PagedResult<PostDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result=_postService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }
        [HttpPost]
        public ActionResult<PostDto> Create([FromBody] PostDto postDto) 
        {
            if (!string.IsNullOrEmpty(postDto.ImageBase64))
            {
                var imageData = Convert.FromBase64String(postDto.ImageBase64.Split(',')[1]);
                var fileName = Guid.NewGuid() + ".png"; // ili format prema potrebi
                var folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "blogs");

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                var filePath = Path.Combine(folderPath, fileName);
                System.IO.File.WriteAllBytes(filePath, imageData);
                postDto.ImageUrl = $"images/blogs/{fileName}";
            }
            var result=_postService.Create(postDto);
            return CreateResponse(result);  
        }
        [HttpPut("{id:int}")]
        public ActionResult<PostDto> Update([FromBody] PostDto postDto)
        {
            var result = _postService.Update(postDto);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _postService.Delete(id);
            return CreateResponse(result);
        }
    }
}
