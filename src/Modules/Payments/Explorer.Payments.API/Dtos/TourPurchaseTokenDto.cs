﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Dtos
{
    public class TourPurchaseTokenDto
    {
        public long Id { get; set; }
        public long UserId { get; set; }

        public long CartId { get; set; }
        public long TourId { get; set; }
        public decimal Price { get; set; }
        public DateTime PurchaseDate { get; set; }

      //  public long OrderId { get; set; }
    }
}
