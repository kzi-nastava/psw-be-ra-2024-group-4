using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Dtos
{
    public class ShoppingCartDto
    {
        public long Id { get; set; }  
        
        public long UserId { get; set; }
        public List<OrderItemDto> Items { get; set; } 
        public List<TourPurchaseTokenDto> PurchaseTokens { get; set; }
        public decimal TotalPrice { get; set; }
        public ShoppingCartDto()
        {
            Items = new List<OrderItemDto>();
            PurchaseTokens = new List<TourPurchaseTokenDto>();  
            CalculateTotalPrice();
        }
       /* public ShoppingCartDto(long id, long userId, List<OrderItemDto> items, List<TourPurchaseTokenDto>tokens)
        {
            Id = id;
            UserId = userId;
            Items = items;
            TourPurchaseTokens = tokens;
            CalculateTotalPrice();
        }*/
        public void CalculateTotalPrice()
        {
            TotalPrice = Items.Sum(item => item.Price);
        }
    }
}
