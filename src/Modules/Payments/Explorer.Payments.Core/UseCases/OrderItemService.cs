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
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.UseCases
{
    public class OrderItemService : CrudService<OrderItemDto, OrderItem>, IOrderItemService
    {
        IOrderItemRepository _orderItemRepository {  get; set; }
        public OrderItemService(ICrudRepository<OrderItem> crudRepository, IMapper mapper, IOrderItemRepository orderItemRepository) : base(crudRepository, mapper)
        {
            _orderItemRepository = orderItemRepository;
        }
        public Result<decimal> CalculateTotalPrice(long itemId)
        {
            try { 
            var result = _orderItemRepository.CalculateTotalPrice(itemId);

                if (result == -1)
                    return Result.Fail<decimal>($"Predmet nije pronadjen");

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail<decimal>($"Greška prilikom računanja cene stavke: {ex.Message}");
            }
        }

        public Result<List<OrderItemDto>> GetAll(long cartId)
        {
            try { 
            var orderItems = _orderItemRepository.GetAll(cartId);

             var result = orderItems.Select(item => new OrderItemDto
                {
                    Id = item.Id,
                    TourName = item.TourName,
                    Price = item.Price,
                    TourId = item.TourId,
                    CartId = item.CartId,
                    IsBundle = item.IsBundle,
                }).ToList();

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail<List<OrderItemDto>>($"Greška prilikom dohvatanja stavki: {ex.Message}");
            }

        }
    }
}
