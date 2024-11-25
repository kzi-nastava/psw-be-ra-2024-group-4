using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.TourShopping
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/shopping/paymentRecord/")]
    public class PaymentRecordController : BaseApiController
    {
        private readonly IPaymentRecordService _paymentRecordService;

        public PaymentRecordController(IPaymentRecordService paymentRecordService)
        {
            _paymentRecordService = paymentRecordService;
        }

        [HttpPost]
        public ActionResult<PaymentRecordDto> Create([FromBody] PaymentRecordDto paymentRecord)
        {
            var result = _paymentRecordService.Create(paymentRecord);
            return CreateResponse(result);
        }

        [HttpGet]
        public ActionResult<List<PaymentRecordDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _paymentRecordService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }
    }
}
