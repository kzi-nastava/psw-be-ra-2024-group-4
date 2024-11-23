using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.Domain
{
    public class Sales:Entity
    {
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public double DiscountPercentage {  get; private set; }
        public List<int> TourIds { get; private set; }

        public int AuthorId {  get; private set; }

        public Sales(DateTime startDate,DateTime endDate,double discountPercentage,int authorId,List<int> tourIds)
        {
            StartDate = startDate;
            EndDate = endDate;
            DiscountPercentage = discountPercentage;
            AuthorId = authorId;
            TourIds = tourIds;
            Validate();
        }

        private void Validate()
        {
            if (StartDate==default) throw new ArgumentException("Invalid Start date");
            if (EndDate == default) throw new ArgumentException("Invalid End date");
            if (EndDate <= StartDate) throw new ArgumentException("End date must be after Start date");
            if ((EndDate - StartDate).TotalDays > 14) throw new ArgumentException("Sales duration cannot exceed two weeks");
            if (AuthorId == 0) throw new ArgumentException("Invalid Author");
            if (DiscountPercentage <= 0) throw new ArgumentException("Invalid discout percentage");
        }
    }
}
