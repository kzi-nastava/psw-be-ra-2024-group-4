﻿using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.API.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Explorer.Stakeholders.Core.UseCases;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Tours.Core.Domain;
using Microsoft.AspNetCore.Hosting;
using Explorer.Tours.Core.Domain.Tours;
using System.Security.Cryptography;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/club")]
    public class ClubController : BaseApiController
    {
        private readonly IClubService _clubService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IImageService _imageService;
        public ClubController(IClubService clubService, IWebHostEnvironment webHostEnvironment, IImageService imageService)
        {
            _clubService = clubService;
            _webHostEnvironment = webHostEnvironment;
            _imageService = imageService;
        }

        [HttpGet]
        public ActionResult<PagedResult<ClubDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _clubService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<ClubDto> Create([FromBody] ClubDto club)
        {
            if (!string.IsNullOrEmpty(club.ImageBase64))
            {
                var imageData = Convert.FromBase64String(club.ImageBase64.Split(',')[1]);
                var folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "clubs");


                club.Image = _imageService.SaveImage(folderPath, imageData, "clubs");
            }
            var result = _clubService.Create(club);
            return CreateResponse(result);
        }
        [HttpPut("{id:int}")]
        public ActionResult<ClubDto> Update([FromBody] ClubDto club)
        {
            if (!string.IsNullOrEmpty(club.ImageBase64))
            {
                // Brisanje stare slike ako postoji
                if (!string.IsNullOrEmpty(club.Image))
                {
                    var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, club.Image);
                    _imageService.DeleteOldImage(oldImagePath);
                }


                // Konvertovanje slike iz base64 formata
                var imageData = Convert.FromBase64String(club.ImageBase64.Split(',')[1]);

                var folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "clubs");

                club.Image = _imageService.SaveImage(folderPath, imageData, "clubs");
            }
            var result = _clubService.Update(club);
            return CreateResponse(result);
        }
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _clubService.Delete(id);
            return CreateResponse(result);
        }

        [HttpDelete("member/{memberId:long}/{clubId:int}/{userId:int}")]
        public ActionResult DeleteMember(long memberId, int clubId, int userId)
        {
            var result = _clubService.DeleteMember(memberId,clubId, userId);
            return CreateResponse(result);
        }

        /* [HttpGet("{id:int}/userids")]
         public ActionResult<List<long>> GetUserIds(int id)
         {
             var result = _clubService.GetUserIds(id);
             return CreateResponse(result);
         }*/

        [HttpGet("active-users/{clubId:int}")]
        public ActionResult<List<User>> GetActiveUsersInClub(int clubId)
        {
            var result = _clubService.GetActiveUsersInClub(clubId);
            return CreateResponse(result);
        }

        [HttpGet("{clubId:int}/eligible-users")]
        public ActionResult<List<UserDto>> GetEligibleUsersForClub(int clubId)
        {
            var result = _clubService.GetEligibleUsersForClub(clubId);
            return CreateResponse(result);
        }
        [HttpGet("{clubId:int}/out-club-users")]
        public ActionResult<List<UserDto>> GetUsersOutClub(int clubId)
        {
            var result = _clubService.GetUsersOutClub(clubId);
            return CreateResponse(result);
        }
        [HttpGet("{id:long}")]
        public ActionResult<ClubDto> GetById(long id)
        {
            var result = _clubService.GetById(id);
            return CreateResponse(result);
        }


        //dodaje membera u klub
        [HttpGet("member/{memberId:long}/{clubId:int}/{userId:int}")]
        public ActionResult AddMember(long memberId, int clubId, int userId)
        {
            var result = _clubService.AddMember(memberId, clubId, userId);
            return CreateResponse(result);
        }
    }
}
