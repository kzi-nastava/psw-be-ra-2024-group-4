using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.UseCases;

public class TourPurchaseTokenService : CrudService<TourPurchaseTokenDto, TourPurchaseToken>, ITourPurchaseTokenService
{
    ITourPurchaseTokenRepository _purchaseTokenRepository {  get; set; }

    public TourPurchaseTokenService(ICrudRepository<TourPurchaseToken> repository, IMapper mapper, ITourPurchaseTokenRepository purchaseTokenRepository) : base(repository, mapper)
    {
        _purchaseTokenRepository = purchaseTokenRepository;
    }

    public Result<List<TourPurchaseTokenDto>> GetAll(long cartId)
    {
        try
        {
            var purchaseTokens = _purchaseTokenRepository.GetAll(cartId);

            var result = purchaseTokens.Select(token => new TourPurchaseTokenDto
            {
                Id = token.Id,
                UserId = token.UserId,
                CartId = token.CartId,
                TourId = token.TourId,

            }).ToList();

         

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
            var purchaseTokens = _purchaseTokenRepository.GetByUser(userid);

            var result = purchaseTokens.Select(token => new TourPurchaseTokenDto
            {
                Id = token.Id,
                UserId = token.UserId,
                CartId = token.CartId,
                TourId = token.TourId

            }).ToList();


            return Result.Ok(result);
        }
        catch (Exception ex)
        {
            return Result.Fail<List<TourPurchaseTokenDto>>($"Greška prilikom dohvatanja stavki: {ex.Message}");
        }
    }
}
