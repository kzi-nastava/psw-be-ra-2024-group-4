using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Infrastructure.Database.Repositories
{
    public class SalesRepository: ISalesRepository
    {
        private readonly PaymentsContext _dbContext;
        private readonly DbSet<Sales> _dbSet;

        public SalesRepository(PaymentsContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<Sales>();
        }

        public IEnumerable<Sales> GetActiveSales()
        {
            return _dbSet.Where(s => s.StartDate <= DateTime.Now && s.EndDate >= DateTime.Now).ToList();
        }

        public List<Sales> GetAllForUser(long userId)
        {

            return _dbContext.Sales
                  .Where(s => s.AuthorId == userId)
                  .ToList();

        }

        public PagedResult<Sales> GetPaged(int page, int pageSize)
        {
            if (page < 1) page = 1;

            var totalSalesCount = _dbSet.Count();

            var skip = (page - 1) * pageSize;

            var sales = _dbSet
                .Skip(skip)
                .Take(pageSize)
                .ToList();

            return new PagedResult<Sales>(sales, totalSalesCount);
        }
        public List<Sales> GetAll()
        {
            
            return _dbContext.Set<Sales>()
                .ToList();
        }
    }
}
