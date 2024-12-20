using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
        [Route("api/clubMember")]
    public class ClubMemberController: BaseApiController
    {
            private readonly IClubMemberService _clubMemberService;
            public ClubMemberController(IClubMemberService clubMemberService)
            {
                _clubMemberService = clubMemberService;
            }

            [HttpPost]
            public ActionResult<ClubDto> Create([FromBody] ClubMemberDto clubMember)
            {
                
                var result = _clubMemberService.Create(clubMember);
                return CreateResponse(result);
            }
            [HttpPut("{id:int}")]
            public ActionResult<ClubDto> Update([FromBody] ClubMemberDto clubMember)
            {
                
                var result = _clubMemberService.Update(clubMember);
                return CreateResponse(result);
            }
           

            [HttpGet("{id:long}")]
            public ActionResult<ClubDto> GetByUserId(long id)
            {
                var result = _clubMemberService.GetByUserId(id);
                return CreateResponse(result);
            }

        }
}
