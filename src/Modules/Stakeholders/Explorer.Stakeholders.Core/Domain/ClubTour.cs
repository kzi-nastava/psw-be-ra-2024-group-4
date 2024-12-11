using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain
{
    public class ClubTour:Entity
    {

        public int ClubId;
        public int TourId;
        public DateTime Date;
        public int Discount;
        public List<int> TouristIds;

        public ClubTour() { }

        public ClubTour(int clubId, int tourId, DateTime date, int discount)
        {
            ClubId = clubId;
            TourId = tourId;
            Date = date;
            Discount = discount;
        }

        private void Validate()
        {
            if (ClubId < 0) throw new ArgumentException("Invalid club");
            if (TourId < 0) throw new ArgumentException("Invalid tour");
            if (Discount <= 0) throw new ArgumentException("Invalid discount");

        }

    }

}

