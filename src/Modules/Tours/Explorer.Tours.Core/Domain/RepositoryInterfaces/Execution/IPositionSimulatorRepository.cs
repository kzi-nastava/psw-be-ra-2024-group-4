using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.Core.Domain.TourExecutions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces.Execution;

public interface IPositionSimulatorRepository : ICrudRepository<PositionSimulator>
{
    PositionSimulator GetByTouristId(long touristId);
}
