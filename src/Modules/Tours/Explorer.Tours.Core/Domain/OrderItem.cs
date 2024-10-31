using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain
{
    public class OrderItem : Entity
    {
        public long Id { get; set; }
        public string TourName { get; set; }
        public decimal Price { get; set; }
        public long TourId { get; set; }
        public long CartId { get; set; }
        public OrderItem()
        {

        }
        public OrderItem(long id, string tourName, decimal price, long tourId, long cartId)
        {
            Id = id;
            TourName = tourName;
            Price = price;
            TourId = tourId;
            CartId = cartId;
        }
    }

}
