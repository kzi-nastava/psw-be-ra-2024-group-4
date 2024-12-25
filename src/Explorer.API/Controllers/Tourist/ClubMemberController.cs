using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
        [Route("api/clubMember")]
    public class ClubMemberController: BaseApiController
    {
        private readonly IClubMemberService _clubMemberService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IImageService _imageService;
        public ClubMemberController(IClubMemberService clubMemberService, IWebHostEnvironment webHostEnvironment, IImageService imageService)
        {
            _clubMemberService = clubMemberService;
            _webHostEnvironment = webHostEnvironment;
            _imageService = imageService;
        }

            [HttpPost]
            public ActionResult<ClubMemberDto> Create([FromBody] ClubMemberDto clubMember)
            {
                if (!string.IsNullOrEmpty(clubMember.ImageBase64))
                {
                    var imageData = Convert.FromBase64String(clubMember.ImageBase64.Split(',')[1]);
                    var folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "clubMembers");


                    clubMember.QuizImage = _imageService.SaveImage(folderPath, imageData, "clubMembers");
                    clubMember.CurrentImage = clubMember.QuizImage;
                }

            var result = _clubMemberService.Create(clubMember);
            return CreateResponse(result);
            }
            [HttpPut("{id:int}")]
            public ActionResult<ClubMemberDto> Update([FromBody] ClubMemberDto clubMember)
            {
                
                var result = _clubMemberService.Update(clubMember);
                return CreateResponse(result);
            }
           

            [HttpGet("{id:long}")]
            public ActionResult<ClubMemberDto> GetByUserId(long id)
            {
                var result = _clubMemberService.GetByUserId(id);
                return CreateResponse(result);
            }   

        }
}
