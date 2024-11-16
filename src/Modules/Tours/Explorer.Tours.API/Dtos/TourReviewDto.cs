using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class TourReviewDto
    {
        public long Id { get; set; }
        public long IdTour { get; set; }
        public long IdTourist { get; set; }
        public string? Comment { get; set; }
        public int Rating { get; set; }
        public DateTime? DateTour { get; set; }
        public DateTime? DateComment { get; set; }
        public List<string>? Images { get; set; }

        public double PercentageCompleted {  get; set; }

        public TourReviewDto()
        {
            Images = new List<string>();
        }

        public TourReviewDto(long id, long idTour, long idTourist, int rating, string comment, DateTime dateTour, DateTime dateComment, List<string> images, double percentageCompleted)
        {
            Id = id;
            IdTour = idTour;
            IdTourist = idTourist;
            Rating = rating;
            Comment = comment;
            DateTour = dateTour;
            DateComment = dateComment;
            Images = images ?? new List<string>();
            PercentageCompleted = percentageCompleted;
        }
    }
}
