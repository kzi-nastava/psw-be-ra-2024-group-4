using Explorer.Payments.API.Dtos;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Infrastructure.Database.Repositories
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly PaymentsContext _dbContext;
        public OrderItemRepository(PaymentsContext dbContext)
        {
            _dbContext = dbContext;
        }

        public decimal CalculateTotalPrice(long itemId)
        {
          
                var item = _dbContext.OrderItems
                    .FirstOrDefault(i => i.Id == itemId);

                if (item == null)
                {
                    return -1;
                }

                return item.Price;
            
            

        }

        public List<OrderItem> GetAll(long cartId)
        {
            
                var orderItems = _dbContext.OrderItems.ToList();

             

                var result = orderItems.Where(item => item.CartId == cartId).ToList();

                return result;
                //return Result.Ok(result);
            
        }

    }
}
