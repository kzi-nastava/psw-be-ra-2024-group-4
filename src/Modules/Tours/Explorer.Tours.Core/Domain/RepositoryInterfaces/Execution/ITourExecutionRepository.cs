using Explorer.Tours.API.Dtos.TourExecutionDtos;
using Explorer.Tours.Core.Domain.TourExecutions;
using Explorer.Tours.Core.Domain.Tours;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces.Execution
{
    public interface ITourExecutionRepository
    {
        public new TourExecution? Get(long id);
        TourExecution Create(TourExecution execution);

        TourExecution Update(TourExecution execution);

        public void Delete(long id);

        public bool KeyPointExists(long keyPointId);

        TourExecution? GetByTourAndTourist(long touristId, long tourId);
        public ICollection<KeyPoint> GetKeyPointsByTourId(long tourId);
        public TourExecution? GetActiveTourByTourist(long touristId);
        bool CheckIfCompleted(long userId, long tourId);
    }
}
