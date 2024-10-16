using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers
{
   
        [Route("api/person")]
        public class PersonController : BaseApiController
        {
            private readonly IPersonService _personService;

            public PersonController(IPersonService personService)
            {
                _personService = personService;
            }

        [HttpGet]
        public ActionResult<PagedResult<ClubDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _personService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }
        /*[HttpPut("{id:int}")]
        public ActionResult<PersonUpdateDto> Update([FromBody] PersonUpdateDto person)
        {
            var result = _personService.Update(person);
            return CreateResponse(result);
        }


        [HttpGet("{id:int}")]
        public ActionResult Get(int id)
        {
            var result = _personService.Get(id);
            return CreateResponse(result);
        }*/
    }
    }


