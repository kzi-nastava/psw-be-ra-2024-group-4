using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.TourShopping;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases.TourShopping
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
            return _orderItemRepository.CalculateTotalPrice(itemId);
        }

        public Result<List<OrderItemDto>> GetAll(long itemId)
        {
            return _orderItemRepository.GetAll(itemId);

        }
    }
}
