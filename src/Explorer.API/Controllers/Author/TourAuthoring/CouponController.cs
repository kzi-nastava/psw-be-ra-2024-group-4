using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author.TourAuthoring
{
    [Authorize(Policy = "authorPolicy")]
    [Route("api/author/coupon")]
    public class CouponController : BaseApiController
    {
        private readonly ICouponService _couponService;

        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }
        [HttpPost]
        public ActionResult<CouponDto> Create([FromBody] CouponDto couponDto)
        {
            var result = _couponService.Create(couponDto);
            return CreateResponse(result);
        }
        [HttpPut("{id:int}")]
        public ActionResult<CouponDto> Update([FromBody] CouponDto couponDto)
        {
            var result = _couponService.Update(couponDto);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _couponService.Delete(id);
            return CreateResponse(result);
        }
        [HttpGet("{authorId:int}")]
        public ActionResult<PagedResult<CouponDto>> GetAll(int authorId,[FromQuery] int page, [FromQuery] int pageSize)
        {
            var result=_couponService.GetAll(authorId, page, pageSize);
            return CreateResponse(result);
        }

        [HttpGet("tour/{tourId:int}")]
        public ActionResult<CouponDto> GetByTourId(int tourId)
        {
            var result = _couponService.GetByTourId(tourId);
            return CreateResponse(result);
        }
    }
}
