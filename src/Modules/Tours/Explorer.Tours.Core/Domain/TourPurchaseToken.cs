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
    public class TourPurchaseToken : ValueObject
    {
        public long UserId { get; }
        public long TourId { get; }
        public DateTime PurchaseDate { get; }

        [JsonConstructor]
        public TourPurchaseToken(long userId, long tourId, DateTime purchaseDate)
        {
            UserId = userId;
            TourId = tourId;
            PurchaseDate = purchaseDate;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return UserId;
            yield return TourId;
            yield return PurchaseDate;
        }

    }
}
