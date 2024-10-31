using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public.TourShopping
{
    public interface IOrderItemService
    {
        Result<OrderItemDto>Create(OrderItemDto orderItemDto);
        Result Delete(int itemId);
        Result<List<OrderItemDto>> GetAll(long itemId);
        Result<decimal> CalculateTotalPrice(long itemId);
        Result<OrderItemDto> Get(int itemId);
    }
}
