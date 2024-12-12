using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.TourShopping
{

    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/sales")]
    public class SalesController : BaseApiController
    {
        private readonly ISalesService _salesService;

        public SalesController(ISalesService salesService)
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
