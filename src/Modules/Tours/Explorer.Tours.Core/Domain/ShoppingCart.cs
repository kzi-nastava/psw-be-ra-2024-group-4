using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain
{
    public class ShoppingCart : Entity
    {
        public List<OrderItem> Items;
        public List<TourPurchaseToken> PurchaseTokens;
        public ShoppingCart()
        {
            Items = new List<OrderItem>();
            PurchaseTokens = new List<TourPurchaseToken>();
        }
    }
}
