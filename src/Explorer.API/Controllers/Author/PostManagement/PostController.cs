using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.Tours;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using System.Security.Cryptography;

namespace Explorer.API.Controllers.Author.PostManagement
{
    [Authorize(Policy = "authorPolicy")]
    [Route("api/postmanagement/post")]
    public class PostController : BaseApiController
    {
        private readonly IPostService _postService;
        private readonly ICommentService _commentService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IImageService _imageService;
        public PostController(IPostService postService,ICommentService commentService,IWebHostEnvironment webHostEnvironment, IImageService imageService)
        {
            _postService = postService;
            _commentService = commentService;
            _webHostEnvironment = webHostEnvironment;
            _imageService = imageService;
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
                var folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "blogs");


                postDto.ImageUrl = _imageService.SaveImage(folderPath, imageData, "blogs");
            }
            var result=_postService.Create(postDto);
            return CreateResponse(result);  
        }
        [HttpPut("{id:int}")]
        public ActionResult<PostDto> Update([FromBody] PostDto postDto)
        {
            if (!string.IsNullOrEmpty(postDto.ImageBase64))
            {
                // Brisanje stare slike ako postoji
                if (!string.IsNullOrEmpty(postDto.ImageUrl))
                {
                    var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, postDto.ImageUrl);
                    _imageService.DeleteOldImage(oldImagePath);
                }


                // Konvertovanje slike iz base64 formata
                var imageData = Convert.FromBase64String(postDto.ImageBase64.Split(',')[1]);

                var folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "blogs");

                postDto.ImageUrl = _imageService.SaveImage(folderPath, imageData, "blogs");
            }
                var result = _postService.Update(postDto);
                 return CreateResponse(result);
             }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _postService.Delete(id);
            return CreateResponse(result);
        }

        [HttpGet("{id:int}")]
        public ActionResult<PostDto> GetPostById(int id)
        {
            var result = _postService.Get(id);
            return CreateResponse(result);
        }
    }
}
