﻿using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.Tours;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly ToursContext _dbContext;
        public OrderItemRepository(ToursContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Result<decimal> CalculateTotalPrice(long itemId)
        {
            try
            {
                var item = _dbContext.OrderItems
                    .FirstOrDefault(i => i.Id == itemId);

                if (item == null)
                {
                    return Result.Fail<decimal>("Stavka nije pronađena.");
                }

                return Result.Ok(item.Price);
            }
            catch (Exception ex)
            {
                return Result.Fail<decimal>($"Greška prilikom računanja cene stavke: {ex.Message}");
            }

        }

        public Result<List<OrderItemDto>> GetAll(long cartId)
        {
            try
            {
                var orderItems = _dbContext.OrderItems.ToList();

                var orderItemDtos = orderItems.Select(item => new OrderItemDto
                {
                    Id = item.Id,
                    TourName = item.TourName,
                    Price = item.Price,
                    TourId = item.TourId,
                    CartId = item.CartId
                }).ToList();

                var result = orderItemDtos.Where(item => item.CartId == cartId).ToList();

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail<List<OrderItemDto>>($"Greška prilikom dohvatanja stavki: {ex.Message}");
            }
        }

    }
}
