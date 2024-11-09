﻿using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces
{
    public interface IOrderItemRepository
    {
        Result<List<OrderItemDto>> GetAll(long cartId);
        Result<decimal> CalculateTotalPrice(long itemId);

      
    }
}
