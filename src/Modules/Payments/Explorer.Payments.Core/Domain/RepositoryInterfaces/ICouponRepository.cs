using Explorer.BuildingBlocks.Core.UseCases;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.Domain.RepositoryInterfaces
{
    public interface ICouponRepository
    {
        Coupon Get(string promoCode);
        PagedResult<Coupon> GetAll(int authorId,int page, int pageSize);  
        Coupon Get(int tourId);

    }
}
