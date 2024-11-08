using Microsoft.AspNetCore.Mvc;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.API.Dtos;
using Microsoft.AspNetCore.Authorization;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.Core.Domain.ProfileMessages;
using Explorer.Stakeholders.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Explorer.Stakeholders.Core.UseCases;
using Explorer.Tours.API.Dtos;

namespace Explorer.API.Controllers.Author
{
    [Authorize(Policy = "authorPolicy")]
    [Route("api/author/profileMessage")]
    public class AuthorProfileMessageController : BaseApiController
    {
        private readonly IProfileMessageService profileMessageService;
        public AuthorProfileMessageController(IProfileMessageService service)
        {
            profileMessageService = service;
        }

        [HttpGet("byUser/{userId:long}")]
        public ActionResult<PagedResult<ProfileMessageDto>> GetByUserId(long userId)
        {
            var result = profileMessageService.GetByUserId(userId);
            return CreateResponse(result);
        }

        [HttpGet("byClub/{clubId:long}")]
        public ActionResult<PagedResult<ProfileMessageDto>> GetByClubId(long clubId)
        {
            var result = profileMessageService.GetByClubId(clubId);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<ProfileMessageDto> Create([FromBody] ProfileMessageDto message)
        {
            var result = profileMessageService.Create(message);
            return CreateResponse(result);
        }

        [HttpPut]
        public ActionResult<ProfileMessageDto> Update([FromBody] ProfileMessageDto message)
        {
            var result = profileMessageService.Update(message);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = profileMessageService.Delete(id);
            return CreateResponse(result);
        }
    }
}
