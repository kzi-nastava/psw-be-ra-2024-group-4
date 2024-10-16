using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Explorer.Stakeholders.Core.Domain
{
    public class Problem: Entity
    {
        public long UserId { get; init; }
        public long TourId { get; init; }
        //public Category Catgory { get; set; }
        public string Category { get; init; }
        public string Description { get; init; }
        public int Priority { get; set; }
        public DateTime Time { get; set; }

        public Problem(long userId, long tourId, string category, string description, int priority, DateTime time)
        {
            UserId = userId;
            TourId = tourId;
            Category = category;
            Description = description;
            Priority = priority;
            Time = time;
            Validate();
        }

        public void Validate()
        {
            if (UserId == 0) throw new ArgumentException("Invalid UserId");
            if (TourId == 0) throw new ArgumentException("Invalid TourId");
            if (Priority == 0) throw new ArgumentException("Invalid Priority");
            if (string.IsNullOrWhiteSpace(Category)) throw new ArgumentException("Invalid Category");
            if (string.IsNullOrWhiteSpace(Description)) throw new ArgumentException("Invalid Description");
        }
    }
}
