using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.Domain.RepositoryInterfaces
{
    public interface ISalesRepository
    {
        IEnumerable<Sales> GetActiveSales();
        List<Sales> GetAll(long userId);
    }
}
