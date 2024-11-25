using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.Domain
{
    public class OrderItem : Entity
    {
        public string TourName { get; set; }
        public decimal Price { get; set; }
        public long TourId { get; set; }
        public long CartId { get; set; }
      
        public OrderItem()
        {

        }
        public OrderItem(string tourName, decimal price, long tourId, long cartId)
        {
            
            TourName = tourName;
            Price = price;
            TourId = tourId;
            CartId = cartId;
            
        }

        public void ApplyDiscount(double discountPercentage)
        {
            if (discountPercentage <= 0 || discountPercentage > 100)
            {
                throw new ArgumentException("Discount percentage must be greater than 0 and less than or equal to 100.");
            }

            Price -= Price * (decimal)(discountPercentage / 100);
            if (Price < 0)
            {
                Price = 0;
            }
        }
    }

}
