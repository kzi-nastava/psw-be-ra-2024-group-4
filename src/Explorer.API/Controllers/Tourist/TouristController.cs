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

        [HttpPut("{id:int}")]
        public ActionResult UpdatePersonEquipment(int id, List<int> equipmentIds)
        {
            var result = _personService.UpdatePersonEquipment(id, equipmentIds);
            return CreateResponse(result);
        }
    }
}
