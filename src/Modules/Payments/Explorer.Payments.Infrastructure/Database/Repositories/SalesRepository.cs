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
        public List<Sales> GetAll(long userId)
        {

            return _dbContext.Sales
                  .Where(s => s.AuthorId == userId)
                  .ToList();

        }
    }
}
