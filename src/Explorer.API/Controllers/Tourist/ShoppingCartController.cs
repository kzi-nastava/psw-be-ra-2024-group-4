using Explorer.Stakeholders.API.Public;
using Explorer.Tours.API.Public.TourShopping;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/shopping")]
    public class ShoppingCartController: BaseApiController
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IShoppingCartService _shoppingCartService;
        public ShoppingCartController(IShoppingCartService shoppingCartService, IWebHostEnvironment webHostEnvironment) 
        {

            _shoppingCartService = shoppingCartService;
            _webHostEnvironment = webHostEnvironment;
        }

    }
}
