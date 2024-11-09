using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public.TourShopping;

public interface ITourPurchaseTokenService
{
    Result<TourPurchaseTokenDto> Create(TourPurchaseTokenDto tokenDto);
    Result Delete(int id);

    Result<List<TourPurchaseTokenDto>> GetAll(long cartId);
    Result<List<TourPurchaseTokenDto>> GetByUser(long userid);
}
