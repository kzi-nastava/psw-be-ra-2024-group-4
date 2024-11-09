using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces;

public interface ITourPurchaseTokenRepository
{

    Result<List<TourPurchaseTokenDto>> GetAll(long cartId);
    Result<List<TourPurchaseTokenDto>> GetByUser(long userid);
}
