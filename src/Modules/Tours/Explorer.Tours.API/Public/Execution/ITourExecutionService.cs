using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.TourExecutionDtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public.Execution
{
    public interface ITourExecutionService
    {
        Result<TourExecutionDto> Create(TourExecutionDto execution);

        Result<TourExecutionDto> CompleteTourExecution(long id);

        Result<TourExecutionDto> AbandonTourExecution(long id);
        Result<TourExecutionDto> GetByTourAndTouristId(long touristId, long tourId);
    }
}
