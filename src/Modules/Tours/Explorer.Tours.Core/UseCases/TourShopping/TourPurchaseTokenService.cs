using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.TourShopping;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases.TourShopping;

public class TourPurchaseTokenService : CrudService<TourPurchaseTokenDto, TourPurchaseToken>, ITourPurchaseTokenService
{
    ITourPurchaseTokenRepository _purchaseTokenRepository {  get; set; }

    public TourPurchaseTokenService(ICrudRepository<TourPurchaseToken> repository, IMapper mapper, ITourPurchaseTokenRepository purchaseTokenRepository) : base(repository, mapper)
    {
        _purchaseTokenRepository = purchaseTokenRepository;
    }

    public Result<List<TourPurchaseTokenDto>> GetAll(long cartId)
    {
        return _purchaseTokenRepository.GetAll(cartId);
    }

    public Result<List<TourPurchaseTokenDto>> GetByUser(long userid)
    {
        return _purchaseTokenRepository.GetByUser(userid);
    }
}
