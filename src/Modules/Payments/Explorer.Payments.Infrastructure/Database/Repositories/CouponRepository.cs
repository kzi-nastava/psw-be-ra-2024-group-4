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
    public class CouponRepository:ICouponRepository
    {
        private readonly PaymentsContext _dbContext;
        public CouponRepository(PaymentsContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Coupon Get(string promoCode)
        {
            throw new NotImplementedException(); //dodati
        }

        public Coupon Get(int tourId)
        {
            var coupon = _dbContext.Coupons.FirstOrDefault(c => c.TourId == tourId);
            if (coupon == null) throw new Exception("Coupon for this tour does not exits");
            return coupon;
        }

        public PagedResult<Coupon> GetAll(int authorId, int page, int pageSize)
        {
            var task=_dbContext.Coupons.Where(c=>c.AuthorId == authorId && (c.ExpirationDate==null || c.ExpirationDate>DateTime.UtcNow)).GetPaged(pageSize, page);
            task.Wait();
            return task.Result;
        }
    }
}
