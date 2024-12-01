using Explorer.Payments.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.Domain.RepositoryInterfaces
{
    public interface IShoppingCartRepository
    {
        List<ShoppingCart> GetAll(long userId);
        decimal CalculateTotalPrice(long cartId);
        ShoppingCart Create(ShoppingCart entity);
        ShoppingCart Update(ShoppingCart aggregateRoot);
        void Delete(int id);
        ShoppingCart Get(int id);
       
    }
}
