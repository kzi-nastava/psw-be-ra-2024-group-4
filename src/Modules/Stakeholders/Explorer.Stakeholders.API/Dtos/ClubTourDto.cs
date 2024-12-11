using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos
{
    public class ClubTourDto
    {
        public int ClubId;
        public int TourId;
        public DateTime Date;
        public int Discount;
        public List<int> TouristIds;
    }
}
