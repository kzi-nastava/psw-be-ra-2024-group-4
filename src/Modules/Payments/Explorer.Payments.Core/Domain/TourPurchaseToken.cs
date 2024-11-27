using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.Domain
{
    public class TourPurchaseToken : Entity
    {
        public long CartId { get; private set; }
        public long UserId { get; private set; }
        public long TourId { get; private set; }
        public decimal Price { get; private set; }
        public DateTime PurchaseDate { get; private set; }

        // public long OrderId { get; private set; }
        //public DateTime PurchaseDate { get; }

        public TourPurchaseToken() { }
        public TourPurchaseToken(long cartId, long userId, long tourId, decimal price)
        {
            CartId = cartId;
            UserId = userId;
            TourId = tourId;
            Price = price;
            PurchaseDate = DateTime.SpecifyKind(DateTime.MinValue, DateTimeKind.Utc);
            //  OrderId = orderId;
        }

        
    }
}
