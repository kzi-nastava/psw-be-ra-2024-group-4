using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author
{
    [Authorize(Policy = "authorPolicy")]
    [Route("api/author/person")]
    public class PersonController: BaseApiController
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


        [HttpGet("{id:int}")]
        public ActionResult Get(int id)
        {
            var result = _personService.Get(id);
            return CreateResponse(result);
        }
    }
}
