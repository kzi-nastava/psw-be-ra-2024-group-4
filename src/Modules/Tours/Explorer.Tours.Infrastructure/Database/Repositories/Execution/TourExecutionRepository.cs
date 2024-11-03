using Explorer.Tours.Core.Domain.TourExecutions;
using Explorer.Tours.Core.Domain.RepositoryInterfaces.Execution;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;
using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Tours.Core.Domain.Tours;

namespace Explorer.Tours.Infrastructure.Database.Repositories.Execution
{
    public class TourExecutionRepository : ITourExecutionRepository
    {
        private readonly ToursContext _dbContext;
        private readonly DbSet<TourExecution> _dbSet;

        public TourExecutionRepository(ToursContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TourExecution>();
        }

        public new TourExecution? Get(long id) //ovde nisam uspela kao na tutoru puca mi al nadam se da radi bez toga...
        {
            return _dbContext.TourExecution
        .Where(t => t.Id == id)
        .FirstOrDefault();

        }
        public TourExecution Update(TourExecution aggregateRoot)
        {
            _dbContext.Entry(aggregateRoot).State = EntityState.Modified;
            _dbContext.SaveChanges();
            return aggregateRoot;
        }

        public TourExecution Create(TourExecution execution)
        {
            _dbContext.TourExecution.Add(execution);
            _dbContext.SaveChanges();
            return execution;
        }

        public void Delete(long id)
        {
            var execution = _dbSet
                .Include(t => t.CompletedKeys) 
                .FirstOrDefault(t => t.Id == id);

            if (execution != null)
            {
                _dbSet.Remove(execution);
                _dbContext.SaveChanges();
            }
        }

        public TourExecution StartExecution(TourExecution execution)
        {
            Create(execution);
            if (execution != null)
            {
                execution.StartTourExecution();
                Update(execution);
                return execution;
            }
            return null;

        }

        public TourExecution CompleteExecution(long executionId)
        {
            var execution = Get(executionId);
            if (execution != null)
            {
                execution.CompleteTourExecution();
                Update(execution);
                return execution;
            }
            return null;

        }

        public TourExecution AbandonExecution(long executionId)
        {
            var execution = Get(executionId);
            if (execution != null)
            {
                execution.AbandonTourExecution();
                Update(execution);
                return execution;
            }
            return null;

        }

        public bool KeyPointExists(long keyPointId)
        {
            return _dbContext.KeyPoints.Any(kp => kp.Id == keyPointId);
        }
    }
}
