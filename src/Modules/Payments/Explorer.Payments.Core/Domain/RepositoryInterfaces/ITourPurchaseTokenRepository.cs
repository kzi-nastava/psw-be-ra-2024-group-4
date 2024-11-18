using Explorer.Payments.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.Domain.RepositoryInterfaces;

public interface ITourPurchaseTokenRepository
{

    List<TourPurchaseToken> GetAll(long cartId);
    List<TourPurchaseToken> GetByUser(long userid);
}
