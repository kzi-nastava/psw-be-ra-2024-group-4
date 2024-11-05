using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public.TourShopping
{
    public interface IShoppingCartService
    {
        Result<ShoppingCartDto> Create(ShoppingCartDto orderItemDto);
        Result<ShoppingCartDto> Update(ShoppingCartDto orderItemDto);
        Result Delete(int itemId);
        Result<List<ShoppingCartDto>> GetAll(long userId);
        Result<decimal> CalculateTotalPrice(long cartId);
        Result<ShoppingCartDto> Get(int cardId);
    }
}
