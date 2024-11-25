using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;

using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;

using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.UseCases
{
    public class ShoppingService : CrudService<ShoppingCartDto, ShoppingCart>, IShoppingCartService
    {
        IShoppingCartRepository _shoppingCartRepository { get; set; }
        ICouponRepository _couponRepository { get; set; }
        
        public ShoppingService(ICrudRepository<ShoppingCart> repository, IMapper mapper, IShoppingCartRepository shoppingCartRepository, ICouponRepository couponRepository) : base(repository, mapper) 
        {
            _shoppingCartRepository = shoppingCartRepository;
            _couponRepository = couponRepository;
          
        }

        public Result<List<ShoppingCartDto>> GetAll(long userId)
        {

            try { 
            var shoppingCarts =  _shoppingCartRepository.GetAll(userId);

            var result = shoppingCarts.Select(cart => new ShoppingCartDto
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
                    CartId = token.CartId

                }).ToList(),
                TotalPrice = cart.Items.Sum(item => item.Price)

            }).ToList();

            

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
                var result = _shoppingCartRepository.CalculateTotalPrice(cartId);

                if(result == -1)
                    return Result.Fail<decimal>($"Korpa nije pronadjena");
                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail<decimal>($"Greška prilikom računanja ukupne cene: {ex.Message}");
            }
        }

       


    }


}

