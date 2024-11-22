using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;

using FluentResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Infrastructure.Database.Repositories
{
    public class ShoppingRepository : IShoppingCartRepository
    {
        private readonly PaymentsContext _dbContext;
        private readonly DbSet<ShoppingCart> _dbSet;
        public ShoppingRepository(PaymentsContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<ShoppingCart>();
        }
        public ShoppingCart Get(int id)
        {
            return _dbSet
                .Include(cart => cart.Items) 
                .Include(cart => cart.PurchaseTokens)
                .FirstOrDefault(cart => cart.Id == id);
        }
        public ShoppingCart Create(ShoppingCart entity)
        {
            _dbSet.Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }
        public ShoppingCart Update(ShoppingCart aggregateRoot)
        {
            _dbContext.Entry(aggregateRoot).State = EntityState.Modified;
            _dbContext.SaveChanges();
            return aggregateRoot;
        }

        public void Delete(int id)
        {
            var entity = Get(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                _dbContext.SaveChanges();
               // return Result.Ok(); 
            }
           // return Result.Fail("Entitet nije pronađen.");
        }
        public List<ShoppingCart> GetAll(long userId)
        {
            
                var shoppingCarts = _dbContext.ShoppingCarts
                    .Include(cart => cart.Items)
                    .Include(cart => cart.PurchaseTokens)
                    .ToList();
            var result = shoppingCarts.Where(cart => cart.UserId == userId).ToList();

            return result;

                
                
            
           
        }

        public decimal CalculateTotalPrice(long cartId)
        {
           
                var shoppingCart = _dbContext.ShoppingCarts
                    .Include(cart => cart.Items)
                    .Include(cart => cart.PurchaseTokens)
                    .FirstOrDefault(cart => cart.Id == cartId);

                if (shoppingCart == null)
                {
                    return -1;
                }
                var totalPrice = shoppingCart.Items.Sum(item => item.Price);
                return totalPrice;
            
        }
    }
}
