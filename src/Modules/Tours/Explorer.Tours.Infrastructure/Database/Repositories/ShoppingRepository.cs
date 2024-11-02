using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.Tours;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class ShoppingRepository : IShoppingCartRepository
    {
        private readonly ToursContext _dbContext;
        private readonly DbSet<ShoppingCartDto> _dbSet;
        public ShoppingRepository(ToursContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<ShoppingCartDto>();
        }
        public ShoppingCartDto Get(int id)
        {
            return _dbSet
                .Include(cart => cart.Items) 
                .Include(cart => cart.PurchaseTokens)
                .FirstOrDefault(cart => cart.Id == id);
        }
        public ShoppingCartDto Create(ShoppingCartDto entity)
        {
            _dbSet.Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }
        public ShoppingCartDto Update(ShoppingCartDto aggregateRoot)
        {
            _dbContext.Entry(aggregateRoot).State = EntityState.Modified;
            _dbContext.SaveChanges();
            return aggregateRoot;
        }

        public Result Delete(int id)
        {
            var entity = Get(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                _dbContext.SaveChanges();
                return Result.Ok(); 
            }
            return Result.Fail("Entitet nije pronađen.");
        }
        public Result<List<ShoppingCartDto>> GetAll(long userId)
        {
            try
            {
                var shoppingCarts = _dbContext.ShoppingCarts
                    .Include(cart => cart.Items)
                    .Include(cart => cart.PurchaseTokens)
                    .ToList();

                
                
                var shoppingCartDtos = shoppingCarts.Select(cart => new ShoppingCartDto
                {
                    Id = cart.Id,
                    UserId = cart.UserId,
                    Items = cart.Items.Select(item => new OrderItemDto
                    {
                        Id = item.Id,
                        TourName = item.TourName,
                        Price = item.Price,
                        TourId = item.TourId,
                        CartId = item.CartId,
                    }).ToList(),
                   PurchaseTokens = cart.PurchaseTokens.Select(token => new TourPurchaseTokenDto
                    {
                        Id = token.Id,
                        UserId = token.UserId,
                        TourId = token.TourId,
                        PurchaseDate = token.PurchaseDate,
                        CartId = token.CartId,
                        OrderId = token.OrderId

                    }).ToList(),
                   TotalPrice = cart.Items.Sum(item => item.Price)

            }).ToList();

                var result = shoppingCartDtos.Where(cart => cart.UserId == userId).ToList();
                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail<List<ShoppingCartDto>>($"Greška prilikom dohvatanja korpi: {ex.Message}");
            }
        }

        public Result<decimal> CalculateTotalPrice(long cartId)
        {
            try
            {
                var shoppingCart = _dbContext.ShoppingCarts
                    .Include(cart => cart.Items)
                    .Include(cart => cart.PurchaseTokens)
                    .FirstOrDefault(cart => cart.Id == cartId);

                if (shoppingCart == null)
                {
                    return Result.Fail<decimal>("Korpa nije pronađena.");
                }
                var totalPrice = shoppingCart.Items.Sum(item => item.Price);
                return Result.Ok(totalPrice);
            }
            catch (Exception ex)
            {
                return Result.Fail<decimal>($"Greška prilikom računanja ukupne cene: {ex.Message}");
            }
        }
    }
}
