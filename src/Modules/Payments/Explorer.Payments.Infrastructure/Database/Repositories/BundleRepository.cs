using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Infrastructure.Database.Repositories
{
    public class BundleRepository : IBundleRepository
    {
        private readonly PaymentsContext _dbContext;
        public BundleRepository(PaymentsContext dbContext)
        {
            _dbContext = dbContext;
        }

        public PagedResult<Bundle> GetByAuthorId(long authorId)
        {
            var task = _dbContext.Bundles.Where(b => b.AuthorId == authorId).GetPaged(0, 0);
            task.Wait();
            return task.Result;
        }
    }
}
