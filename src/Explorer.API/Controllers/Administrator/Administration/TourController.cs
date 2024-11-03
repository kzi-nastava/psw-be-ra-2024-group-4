using Explorer.Tours.API.Public.TourAuthoring;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Administrator.Administration
{

    [Authorize(Policy = "administratorPolicy")]
    [Route("api/admin/tour")]
    public class TourController :BaseApiController
    {
        private readonly ITourService _tourService;

        public TourController(ITourService tourService)
        {
            _tourService = tourService;
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTour(int id)
        {
            var result = _tourService.DeleteTour(id);

            if (result.IsSuccess)
            {
                return NoContent(); // Vraća 204 status kod ako je brisanje uspešno
            }
            else
            {
                return BadRequest(result.Errors); // Vraća 400 status kod ako ima grešaka
            }
        }
    }
}
