using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Infrastructure.Database.Repositories;

public class TourPurchaseTokenRepository : ITourPurchaseTokenRepository
{
    private readonly ToursContext _dbContext;
    private readonly DbSet<TourPurchaseTokenDto> _dbSet;
    public TourPurchaseTokenRepository(ToursContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<TourPurchaseTokenDto>();
    }

    public Result<List<TourPurchaseTokenDto>> GetAll(long cartId)
    {
        try
        {
            var purchaseTokens = _dbContext.PurchaseTokens.ToList();

            var purchaseTokenDtos = purchaseTokens.Select(token => new TourPurchaseTokenDto
            {
                Id = token.Id,
                UserId = token.UserId,
                CartId = token.CartId,
                TourId = token.TourId,
       
            }).ToList();

            var result = purchaseTokenDtos.Where(token => token.CartId == cartId).ToList();

            return Result.Ok(result);
        }
        catch (Exception ex)
        {
            return Result.Fail<List<TourPurchaseTokenDto>>($"Greška prilikom dohvatanja stavki: {ex.Message}");
        }
    }

    public Result<List<TourPurchaseTokenDto>> GetByUser(long userid)
    {
        try
        {
            var purchaseTokens = _dbContext.PurchaseTokens.ToList();

            var purchaseTokenDtos = purchaseTokens.Select(token => new TourPurchaseTokenDto
            {
                Id = token.Id,
                UserId = token.UserId,
                CartId = token.CartId,
                TourId = token.TourId
       
            }).ToList();

            var result = purchaseTokenDtos.Where(token => token.UserId == userid).ToList();

            return Result.Ok(result);
        }
        catch (Exception ex)
        {
            return Result.Fail<List<TourPurchaseTokenDto>>($"Greška prilikom dohvatanja stavki: {ex.Message}");
        }
    }
}
