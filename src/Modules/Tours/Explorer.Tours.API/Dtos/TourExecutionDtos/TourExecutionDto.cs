using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos.TourExecutionDtos
{
    public class TourExecutionDto
    {
        public long Id { get; set; }
        public long TourId { get; private set; }
        public long TouristId { get; private set; }

        public long LocationId { get; private set; } //ili entitet nisam sigurna

        public DateTime? LastActivity { get; private set; }
        public TourExecutionStatus Status { get; private set; }

        public List<CompletedKeyPointDto> CompletedKeys { get; private set; } //ovde se nalazi endTime za svaku kt

        public TourExecutionDto(long tourId, long touristId, long locationId, DateTime? lastActivity, TourExecutionStatus status, List<CompletedKeyPointDto> completedKeys)
        {
            TourId = tourId;
            TouristId = touristId;
            LocationId = locationId;
            LastActivity = lastActivity;
            Status = status;
            CompletedKeys = completedKeys;
        }
    }

    public enum TourExecutionStatus
    {
        Active,
        Completed,
        Abandoned
    }
}
