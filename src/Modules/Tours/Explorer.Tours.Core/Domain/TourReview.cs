using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Modules.Core.Domain
{
    public class TourReview : Entity
    {
        public int IdTour { get; private set; }
        public int IdTourist { get; private set; }
        public int Rating { get; private set; }
        public string Comment { get; private set; }
        public DateTime DateTour { get; private set; }
        public DateTime DateComment { get; private set; }
        public List<string> Images { get; private set; }

        public TourReview(int idTour, int idTourist, int rating, string comment, DateTime dateTour, DateTime dateComment, List<string> images)
        {
            IdTour = idTour;
            IdTourist = idTourist;
            Rating = rating;
            Comment = comment;
            DateTour = dateTour;
            DateComment = dateComment;
            Images = images;
        }
    }
}
