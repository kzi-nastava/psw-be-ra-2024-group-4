using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Tours.Core.Domain.RepositoryInterfaces.Execution;
using Explorer.Tours.Core.Domain.TourExecutions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Infrastructure.Database.Repositories.Execution;

public class PositionSimulatorRepository : CrudDatabaseRepository<PositionSimulator, ToursContext>, IPositionSimulatorRepository
{

    private readonly ToursContext _dbContext;

    public PositionSimulatorRepository(ToursContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public PositionSimulator GetByTouristId(long touristId)
    {
        return _dbContext.Positions.Where(ps => ps.TouristId == touristId).FirstOrDefault(); }
}
