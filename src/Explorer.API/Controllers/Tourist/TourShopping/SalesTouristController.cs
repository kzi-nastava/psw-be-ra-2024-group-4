using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.TourShopping
{

    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/sales")]
    public class SalesTouristController : BaseApiController
    {
        private readonly ISalesService _salesService;

        public SalesTouristController(ISalesService salesService)
        {
            _salesService = salesService;
        }

        [HttpGet]
        public ActionResult<List<SalesDto>> GetAll()
        {

            var result = _salesService.GetAll();
            return CreateResponse(result);
        }

    }
}
