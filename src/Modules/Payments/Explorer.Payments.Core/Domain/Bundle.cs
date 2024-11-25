using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.Domain
{
    public class Bundle : Entity
    {
        public string Name { get; private set; }
        public decimal Price { get; private set; }

        public BundleStatus Status { get; private set; }

        public List<long> TourIds { get; private set; }
        public long AuthorId { get; private set; }

        public Bundle() { }

        public Bundle(string name, decimal price, BundleStatus status, List<long> tourIds, long authorId)
        {
           
            Name = name;
            Price = price;
            Status = status;
            TourIds = tourIds;
            AuthorId = authorId;
        }
        public enum BundleStatus
        {
            DRAFT,
            PUBLISHED,
            ARCHIVED
        }
    }
}
