using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.TourShopping
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/shopping/item/")]
    public class OrderItemController : BaseApiController
    {
        private readonly IOrderItemService _orderItemService;
        
        public OrderItemController(IOrderItemService orderItemService)
        {
            _orderItemService = orderItemService;
        }

        [HttpPost]
        public ActionResult<OrderItemDto> Create([FromBody] OrderItemDto item)
        {
            var result = _orderItemService.Create(item);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _orderItemService.Delete(id);

            return CreateResponse(result);
        }

       

        [HttpGet("getbyid/{id:int}")]
        public ActionResult<OrderItemDto> Get(int id)
        {
            var result = _orderItemService.Get(id);
            return CreateResponse(result);
        }

        [HttpGet("getPrice/{id:long}")]
        public ActionResult<decimal> CalculateTotalPrice(long itemId)
        {
            var result = _orderItemService.CalculateTotalPrice(itemId);
            return CreateResponse(result);
        }

        [HttpGet("getAllFromCart/{id:long}")]
        public ActionResult<List<OrderItemDto>> GetAll(long id)
        {
            var result =  _orderItemService.GetAll(id);
            return CreateResponse(result);
        }


    }
}
