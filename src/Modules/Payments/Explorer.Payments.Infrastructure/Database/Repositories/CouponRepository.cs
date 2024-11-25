﻿using Explorer.BuildingBlocks.Core.UseCases;
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
            if (string.IsNullOrWhiteSpace(promoCode))
            {
                throw new ArgumentException("Promo code cannot be null or empty.");
            }

            // Koristi Entity Framework za dohvat kupona
            return _dbContext.Coupons.FirstOrDefault(c => c.PromoCode == promoCode);
        }

        public PagedResult<Coupon> GetAll(int authorId, int page, int pageSize)
        {
            var task=_dbContext.Coupons.Where(c=>c.AuthorId == authorId).GetPaged(pageSize, page);
            task.Wait();
            return task.Result;
        }
    }
}
