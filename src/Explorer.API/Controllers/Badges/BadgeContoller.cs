using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounter.API.Dtos.Explorer.Encounters.API.Dtos;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Badges;
using FluentResults;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Badges
{
    [Route("api/badges")]
    public class BadgeContoller : BaseApiController
    {
        public readonly IBadgeService _badgeService;

        public BadgeContoller(IBadgeService badgeService) { 
            _badgeService = badgeService;
        }

        [HttpGet]
        public ActionResult<PagedResult<BadgeDto>> GetAll()
        {
            var ret = _badgeService.getAll();
            return CreateResponse(ret);
        }

        [HttpGet("getNotRead")]
        public ActionResult<PagedResult<BadgeDto>> GetAllNotRead()
        {
            var ret = _badgeService.getAllNotRead();
            return CreateResponse(ret);
        }

        [HttpGet("getById/{userId:long}")]
        public ActionResult<PagedResult<BadgeDto>> GetAllByUserId(long userId)
        {
            var ret = _badgeService.getAllById(userId);
            return CreateResponse(ret);
        }

        [HttpGet("getNotRead/{userId:long}")]
        public ActionResult<PagedResult<BadgeDto>> GetAllNotReadByUserId(long userId)
        {
            var ret = _badgeService.getAllNotReadById(userId);
            return CreateResponse(ret);
        }

        [HttpPost("read/{badgeId:long}")]
        public ActionResult<BadgeDto> Read(int badgeId)
        {
            var result = _badgeService.readBadge(badgeId);
            if (result == null)
            {
                return NotFound($"Badge with ID {badgeId} not found.");
            }
            return CreateResponse(result);
        }
    }
}
