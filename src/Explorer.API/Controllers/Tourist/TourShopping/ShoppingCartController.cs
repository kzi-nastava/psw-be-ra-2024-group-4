using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.TourShopping
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/shopping/")]
    public class ShoppingCartController : BaseApiController
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly ICouponService _couponService;

        public ShoppingCartController(IShoppingCartService shoppingCartService, ICouponService couponService)
        {
            _shoppingCartService = shoppingCartService;
            _couponService = couponService;
        }

        [HttpPost]
        public ActionResult<ShoppingCartDto> Create([FromBody] ShoppingCartDto shoppingCart)
        {
            var result = _shoppingCartService.Create(shoppingCart);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _shoppingCartService.Delete(id);

            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<ShoppingCartDto> Update([FromBody] ShoppingCartDto shoppingCart)
        {
            var result = _shoppingCartService.Update(shoppingCart);
            return CreateResponse(result);
        }


        [HttpGet("getByUser/{userid:int}")]
        public ActionResult<List<ShoppingCartDto>> GetAll(int userid)
        {
            var result = _shoppingCartService.GetAll(userid);
            return CreateResponse(result);
        }

        [HttpGet("getPrice/{id:int}")]
        public ActionResult<decimal> CalculateTotalPrice(long id)
        {
            var result = _shoppingCartService.CalculateTotalPrice(id);
            return CreateResponse(result);
        }

        [HttpGet("getbyid/{id:int}")]
        public ActionResult<ShoppingCartDto> Get(int id)
        {
            var result = _shoppingCartService.Get(id);
            return CreateResponse(result);
        }

        [HttpGet("coupon/{promoCode}")]
        public ActionResult<CouponDto> GetCouponByPromoCode(string promoCode)
        {
            var result = _couponService.Get(promoCode);
            if (result.IsFailed) 
            {
                return BadRequest(result.Errors);
            }
            return Ok(result.Value); 
        }
     


    }
}
