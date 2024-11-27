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
        public bool IsBundle { get; set; }
        public OrderItem()
        {

        }
        public OrderItem(string tourName, decimal price, long tourId, long cartId, bool isBundle)
        {
            
            TourName = tourName;
            Price = price;
            TourId = tourId;
            CartId = cartId;
            IsBundle = isBundle;
        }
    }

}
