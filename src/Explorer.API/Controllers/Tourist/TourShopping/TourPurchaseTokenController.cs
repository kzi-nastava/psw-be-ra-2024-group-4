using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.TourShopping
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/shopping/token/")]
    public class TourPurchaseTokenController : BaseApiController
    {
        private readonly ITourPurchaseTokenService _purchaseTokenService;

        public TourPurchaseTokenController(ITourPurchaseTokenService purchaseTokenService)
        {
            _purchaseTokenService = purchaseTokenService;
        }

        [HttpPost]
        public ActionResult<TourPurchaseTokenDto> Create([FromBody] TourPurchaseTokenDto item)
        {
            var result = _purchaseTokenService.Create(item);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _purchaseTokenService.Delete(id);

            return CreateResponse(result);
        }

        [HttpGet("getAllFromCart/{id:long}")]
        public ActionResult<List<TourPurchaseTokenDto>> GetAll(long id)
        {
            var result = _purchaseTokenService.GetAll(id);
            return CreateResponse(result);
        }

        [HttpGet("getAllFromUser/{id:long}")]
        public ActionResult<List<TourPurchaseTokenDto>> GetByUser(long id)
        {
            var result = _purchaseTokenService.GetByUser(id);
            return CreateResponse(result);
        }




    }
}
