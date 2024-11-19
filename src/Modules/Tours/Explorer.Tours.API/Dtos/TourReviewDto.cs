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
        public string Image { get; set; }
        public string ImageBase64 { get; set; }

        public double PercentageCompleted {  get; set; }

        public TourReviewDto()
        {
        }

        public TourReviewDto(long id, long idTour, long idTourist, int rating, string comment, DateTime dateTour, DateTime dateComment, string image, double percentageCompleted, string imageBase64)
        {
            Id = id;
            IdTour = idTour;
            IdTourist = idTourist;
            Rating = rating;
            Comment = comment;
            DateTour = dateTour;
            DateComment = dateComment;
            Image = image;
            ImageBase64 = imageBase64;
            PercentageCompleted = percentageCompleted;
        }
    }
}
