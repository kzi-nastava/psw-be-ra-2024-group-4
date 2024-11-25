using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.Domain
{
    public class PaymentRecord : Entity
    {
        public long BundleId { get; private set; }
        public long TouristId { get; private set; }

        public decimal Price { get; private set; }

        public DateTime Date { get; private set; }

        public PaymentRecord() { }

        public PaymentRecord(long bundleId, long  touristId, decimal price, DateTime date)
        {
            BundleId = bundleId;
            TouristId = touristId;
            Price = price;
            Date = date;
        }
    }
}
