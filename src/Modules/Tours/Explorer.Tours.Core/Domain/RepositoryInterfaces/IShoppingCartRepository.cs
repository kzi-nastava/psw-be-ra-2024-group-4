using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces
{
    public interface IShoppingCartRepository
    {
        Result<List<ShoppingCartDto>> GetAll();
        Result<decimal> CalculateTotalPrice(long cartId);
        ShoppingCartDto Create(ShoppingCartDto entity);
        ShoppingCartDto Update(ShoppingCartDto aggregateRoot);
        Result Delete(int id);
        ShoppingCartDto Get(int id);
    }
}
