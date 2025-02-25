﻿using Explorer.Payments.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.Domain.RepositoryInterfaces
{
    public interface IOrderItemRepository
    {
        List<OrderItem> GetAll(long cartId);
        decimal CalculateTotalPrice(long itemId);

      
    }
}
