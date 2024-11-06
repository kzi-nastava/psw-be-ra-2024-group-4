using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.TourExecutionDtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public.Execution
{
    public interface ITourExecutionService
    {
        Result<TourExecutionDto> Create(TourExecutionDto execution);

        Result<TourExecutionDto> CompleteTourExecution(long id);

        Result<TourExecutionDto> AbandonTourExecution(long id);

        public Result<TourExecutionDto> CompleteKeyPoint(long executionId, long keyPointId);
        public void UpdateLastActivity(long executionId);

        Result<TourExecutionDto> GetByTourAndTouristId(long touristId, long tourId);
        public ICollection<KeyPointDto> GetKeyPointsForTour(long tourId);
        public Result<TourExecutionDto> GetActiveTourByTouristId(long touristId);

    }
}
