using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class OrderItemDto
    {
        public long Id { get; set; }              
        public string TourName { get; set; }    
        public decimal Price { get; set; }       
        public long TourId { get; set; }      
        public long CardId { get; set; }

    }
}
