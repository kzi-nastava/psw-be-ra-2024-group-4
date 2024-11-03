using Explorer.BuildingBlocks.Core.Domain;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.TourExecutions
{
    public class TourExecution : Entity
    {
        public long TourId { get; private set; }
        public long TouristId { get; private set; }

        public long LocationId { get; private set; } //ili entitet nisam sigurna

        public DateTime? LastActivity { get; private set; }
        public TourExecutionStatus Status { get; private set; }

        public List<CompletedKeyPoint> CompletedKeys { get; private set; } //ovde se nalazi endTime za svaku kt

        public TourExecution(long tourId, long touristId, long locationId, DateTime? lastActivity, TourExecutionStatus status, List<CompletedKeyPoint> completedKeys)
        {
            TourId = tourId;
            TouristId = touristId;
            LocationId = locationId;
            LastActivity = lastActivity;
            Status = status;
            if (!DateTime.TryParse(LastActivity.ToString(), out _)) throw new ArgumentException("Invalid date time format");

            CompletedKeys = completedKeys;
        }

        public void StartTourExecution()
        {
           LastActivity = DateTime.UtcNow;
            Status = TourExecutionStatus.Active;
        }

        public CompletedKeyPoint CompleteKeyPoint(long keyPointId)
        {
            if (Status != TourExecutionStatus.Active)
                throw new InvalidOperationException("Tour is not active");

            var completedKeyPoint = new CompletedKeyPoint(keyPointId, DateTime.UtcNow);

            CompletedKeys.Add(completedKeyPoint);

            return completedKeyPoint;
        }

        public void CompleteTourExecution()
        {
            if (Status != TourExecutionStatus.Active)
                throw new ArgumentException("Invalid end status.");

            Status = TourExecutionStatus.Completed;
            LastActivity = DateTime.UtcNow;
        }

        public void AbandonTourExecution()
        {
            if (Status != TourExecutionStatus.Active)
                throw new ArgumentException("Invalid end status.");

            Status = TourExecutionStatus.Abandoned;
            LastActivity = DateTime.UtcNow;
        }
    }

    public enum TourExecutionStatus
    {
        Active,
        Completed,
        Abandoned
    }
}
