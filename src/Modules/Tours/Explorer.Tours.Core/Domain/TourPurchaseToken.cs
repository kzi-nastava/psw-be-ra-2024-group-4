using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain
{
    public class TourPurchaseToken : Entity
    {
        public long CartId { get; private set; }
        public long UserId { get; private set; }
        public long TourId { get; private set; }

        public long OrderId { get; private set; }
        public DateTime PurchaseDate { get; }

        public TourPurchaseToken() { }
        public TourPurchaseToken(long cartId, long userId, long tourId, DateTime purchaseDate, long orderId)
        {
            CartId = cartId;
            UserId = userId;
            TourId = tourId;
            PurchaseDate = purchaseDate;
            OrderId = orderId;
        }

        
    }
}
