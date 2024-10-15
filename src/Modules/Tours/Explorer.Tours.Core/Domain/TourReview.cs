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
        public string? Comment { get; private set; }
        public DateTime? DateTour { get; private set; }
        public DateTime? DateComment { get; private set; }
        public List<string>? Images { get; private set; }

        public TourReview(int idTour, int idTourist, int rating, string comment, DateTime? dateTour, DateTime? dateComment, List<string> images)
        {
            if(idTour == 0) throw new ArgumentException("idTour");
            IdTour = idTour;
            if (idTourist == 0) throw new ArgumentException("idTourist");
            IdTourist = idTourist;
            if (rating < 1 || rating > 5) throw new ArgumentException("Invalid Rating");
            Rating = rating;
            if (string.IsNullOrWhiteSpace(comment)) comment = "";
            Comment = comment;
            if (dateTour == null) throw new ArgumentNullException("Invalid Date");
            DateTour = dateTour;
            if (dateComment == null) throw new ArgumentNullException("Invalid Date");
            DateComment = dateComment;
            if(images == null) images = new List<string>();
            Images = images;
        }

        public TourReview() 
        {
        
        }
    }
}
