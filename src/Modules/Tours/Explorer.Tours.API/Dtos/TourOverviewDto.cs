using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class TourOverviewDto
    {
        public long TourId { get; set; }
        public string TourName { get; set; }
        public string TourDescription { get; set; }
        public string TourDifficulty { get; set; }

        public decimal Price { get; set; }
        public List<string> Tags { get; set; }
        public KeyPointDto FirstKeyPoint { get; set; }
        public List<TourReviewDto> Reviews { get; set; }
        public float AverageRating { get; set; }
    }
}
