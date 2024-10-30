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
        public long TourId { get;  set; }
        public long TouristId { get;  set; }

        public long LocationId { get;  set; } //ili entitet nisam sigurna

        public DateTime? LastActivity { get; set; }
        public TourExecutionStatus Status { get;  set; }

        public List<CompletedKeyPointDto> CompletedKeys { get; set; } //ovde se nalazi endTime za svaku kt

        public TourExecutionDto() { }

        public TourExecutionDto(long id, long tourId, long touristId, long locationId, DateTime? lastActivity, TourExecutionStatus status, List<CompletedKeyPointDto> completedKeys)
        {
            Id = id;
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
