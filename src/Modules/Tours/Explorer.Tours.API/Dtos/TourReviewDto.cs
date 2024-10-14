using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class TourReviewDto
    {
        public int IdTour { get; set; }
        public int IdTourist { get; set; }
        public string Comment { get; set; }
        public DateTime DateTour { get; set; }
        public DateTime DateComment { get; set; }
        public List<string> Images { get; set; }

        TourReviewDto() 
        { 
        
        }
    }
}
