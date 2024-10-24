using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Tours.Core.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/person/tourist")]
    public class TouristController :BaseApiController
    {
        private readonly IPersonService _personService;

        public TouristController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet("{id:int}")]
        public ActionResult Get(int id)
        {
            var result = _personService.Get(id);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult Update([FromBody]PersonDto personDto)
        {
            var result = _personService.Update(personDto);
            return CreateResponse(result);
        }
    }
}
