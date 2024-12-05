using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.Domain
{
    public class ShoppingCart : Entity
    {
        public long UserId { get; private set; }
        public List<OrderItem> Items { get; private set; }
        public List<TourPurchaseToken> PurchaseTokens { get; private set; }
        public ShoppingCart()
        {
            Items = new List<OrderItem>();
            PurchaseTokens = new List<TourPurchaseToken>();
        }

       


    }
}
