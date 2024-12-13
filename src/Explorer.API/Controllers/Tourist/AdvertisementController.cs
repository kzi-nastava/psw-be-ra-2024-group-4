using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Route("api/tourist/advertisement")]
    public class AdvertisementController : BaseApiController
    {
        private readonly IAdvertisementService _advertisementService;

        public AdvertisementController(IAdvertisementService advertisementService)
        {
            _advertisementService = advertisementService;
        }


    }
}
