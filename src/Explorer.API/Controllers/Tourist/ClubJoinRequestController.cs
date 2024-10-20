using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/clubJoinRequest")]
    public class ClubJoinRequestController : BaseApiController
    {
        private readonly IClubJoinRequestService _clubJoinRequestService;

        public ClubJoinRequestController(IClubJoinRequestService clubJoinRequestService)
        {
            _clubJoinRequestService = clubJoinRequestService;
        }

        [HttpGet]
        public ActionResult<PagedResult<ClubJoinRequestDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _clubJoinRequestService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }
        [HttpGet("{clubId:int}")]
        public ActionResult<PagedResult<ClubJoinRequestDto>> GetClubMembers([FromQuery] int page, [FromQuery] int pageSize, int clubId)
        {
            var result = _clubJoinRequestService.GetRequestsForClubMembers(clubId);
            return CreateResponse(result);
        }
        [HttpPost]
        public ActionResult<ClubJoinRequestDto> Create([FromBody] ClubJoinRequestDto dto)
        {
            var result = _clubJoinRequestService.Create(dto);
            return CreateResponse(result);
        }
        [HttpPut("{id:int}")]
        public ActionResult<ClubJoinRequestDto> Update([FromBody] ClubJoinRequestDto dto)
        {
            var result = _clubJoinRequestService.Update(dto);
            return CreateResponse(result);
        }
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _clubJoinRequestService.Delete(id);
            return CreateResponse(result);
        }
        [HttpGet("{clubId:int}/{userId:int}")]
        public ActionResult UserRequestExists(int clubId, int userId)
        {
            //vraca 200 ako je korisnik vec poslao zahtev za taj klub, 404 ako nije
            bool requestExists = _clubJoinRequestService.UserRequestExists(clubId, userId);
            if (requestExists)
            {
                return Ok(true);  
            }
            else
            {
                return Ok(false);
            }
        }
    }
}
