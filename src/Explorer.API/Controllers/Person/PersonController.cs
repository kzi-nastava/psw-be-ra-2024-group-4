using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Tours.API.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Person
{
    [Authorize(Policy = "AuthorPolicy")]
    [Authorize(Policy = "TouristPolicy")]
    [Route("api/person")]
    public class PersonController : BaseApiController
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpPut("{id:int}")]
        public ActionResult<PersonUpdateDto> Update([FromBody] PersonUpdateDto person)
        {
            var result = _personService.Update(person);
            return CreateResponse(result);
        }
    }
}
