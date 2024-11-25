using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.Domain
{
    public class Coupon : Entity 
    {
        public string PromoCode { get; private set; }
        public double DiscountPercentage { get; private set; }
        public DateTime? ExpirationDate { get; private set; } //moze da bude neograniceno trajanje
        public int AuthorId {  get; private set; }
        public int? TourId {  get; private set; } //u zavisnosti da li se primenjuje na sve ture

        public Coupon(string promoCode,double discountPercentage, DateTime? expirationDate, int authorId, int? tourId)
        {
            PromoCode = promoCode;
            DiscountPercentage = discountPercentage;
            ExpirationDate = expirationDate;
            AuthorId = authorId;
            TourId = tourId;
            Validate();
        }

        private void Validate()
        {
            if (string.IsNullOrEmpty(PromoCode) && PromoCode.Length != 8) throw new ArgumentException("Invalid promo code");
            if (AuthorId == 0) throw new ArgumentException("Invalid Author");
            if (DiscountPercentage <= 0) throw new ArgumentException("Invalid discout percentage");
        }
    }
}
