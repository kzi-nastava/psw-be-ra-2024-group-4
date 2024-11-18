using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Public;

public interface IOrderItemService
{
    Result<OrderItemDto>Create(OrderItemDto orderItemDto);
    Result Delete(int itemId);
    Result<List<OrderItemDto>> GetAll(long cartId);
    Result<decimal> CalculateTotalPrice(long itemId);
    Result<OrderItemDto> Get(int itemId);
}
