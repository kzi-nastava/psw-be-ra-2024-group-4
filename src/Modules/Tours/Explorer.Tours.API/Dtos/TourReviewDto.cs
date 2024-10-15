using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class TourReviewDto
    {
        public int Id { get; set; }
        public int IdTour { get; set; }
        public int IdTourist { get; set; }
        public string? Comment { get; set; }
        public int Rating { get; set; }
        public DateTime? DateTour { get; set; }
        public DateTime? DateComment { get; set; }
        public List<string>? Images { get; set; }

        public TourReviewDto()
        {
            Images = new List<string>();
        }

        public TourReviewDto(int id, int idTour, int idTourist, int rating, string comment, DateTime dateTour, DateTime dateComment, List<string> images)
        {
            Id = id;
            IdTour = idTour;
            IdTourist = idTourist;
            Rating = rating;
            Comment = comment;
            DateTour = dateTour;
            DateComment = dateComment;
            Images = images ?? new List<string>();
        }
    }
}
