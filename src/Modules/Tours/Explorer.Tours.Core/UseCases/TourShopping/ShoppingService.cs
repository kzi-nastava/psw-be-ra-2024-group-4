using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Modules.Core.Domain;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.TourShopping;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.Tours;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases.TourShopping
{
    public class ShoppingService : CrudService<ShoppingCartDto, ShoppingCart>, IShoppingCartService
    {
        IShoppingCartRepository _shoppingCartRepository { get; set; }
        public ShoppingService(ICrudRepository<ShoppingCart> repository, IMapper mapper, IShoppingCartRepository shoppingCartRepository) : base(repository, mapper) 
        {
            _shoppingCartRepository = shoppingCartRepository;
        }

        public Result<ShoppingCartDto> Create(ShoppingCartDto orderItemDto)
        {

            return _shoppingCartRepository.Create(orderItemDto);
        }
        public Result<ShoppingCartDto> Update(ShoppingCartDto orderItemDto)
        {
            return _shoppingCartRepository.Update(orderItemDto);
        }
        public Result Delete(int itemId)
        {
            return _shoppingCartRepository.Delete(itemId);
        }
        public Result<List<ShoppingCartDto>> GetAll()
        {
            return _shoppingCartRepository.GetAll();
        }
        public Result<decimal> CalculateTotalPrice(long cartId)
        {
            return _shoppingCartRepository.CalculateTotalPrice(cartId);
        }
        public Result<ShoppingCartDto> Get(int cardId)
        {
            return _shoppingCartRepository.Get(cardId);
        }
    }
}
