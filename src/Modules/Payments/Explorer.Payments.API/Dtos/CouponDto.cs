using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Dtos
{
    public class CouponDto
    {
        public long Id { get; set; }
        public double DiscountPercentage {  get; set; }
        public string? PromoCode { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public int? TourId { get; set; }
        public long AuthorId { get; set; }
    }
}
