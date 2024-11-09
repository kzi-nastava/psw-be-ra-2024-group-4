﻿using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.TourShopping;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.TourShopping
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/shopping/")]
    public class ShoppingCartController : BaseApiController
    {
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
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


    }
}
