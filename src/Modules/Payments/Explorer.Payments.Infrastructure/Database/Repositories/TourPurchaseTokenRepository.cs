using Explorer.Payments.API.Dtos;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Explorer.Payments.Infrastructure.Database;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Infrastructure.Database.Repositories;

public class TourPurchaseTokenRepository : ITourPurchaseTokenRepository
{
    private readonly PaymentsContext _dbContext;
    private readonly DbSet<TourPurchaseToken> _dbSet;
    public TourPurchaseTokenRepository(PaymentsContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<TourPurchaseToken>();
    }

    public List<TourPurchaseToken> GetAll(long cartId)
    {
       
         var purchaseTokens = _dbContext.PurchaseTokens.ToList();
        var result = purchaseTokens.Where(token => token.CartId == cartId).ToList();
        return result;
    }

    public List<TourPurchaseToken> GetByUser(long userid)
    {
        
            var purchaseTokens = _dbContext.PurchaseTokens.ToList();
            var result = purchaseTokens.Where(token => token.UserId == userid).ToList();
            return result;
          
    }
}
