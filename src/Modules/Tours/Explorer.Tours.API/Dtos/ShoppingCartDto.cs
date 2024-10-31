using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class ShoppingCartDto
    {
        public long Id { get; set; }    
        public List<OrderItemDto> Items { get; set; } 
        public List<TourPurchaseTokenDto> TourPurchaseTokens { get; set; }
        public decimal TotalPrice { get; set; }
        public ShoppingCartDto()
        {
            Items = new List<OrderItemDto>();
            TourPurchaseTokens = new List<TourPurchaseTokenDto>();  
            CalculateTotalPrice();
        }
        public ShoppingCartDto(List<OrderItemDto> items, List<TourPurchaseTokenDto>tokens)
        {
            Items = items;
            TourPurchaseTokens = tokens;
            CalculateTotalPrice();
        }
        public void CalculateTotalPrice()
        {
            TotalPrice = Items.Sum(item => item.Price);
        }
    }
}
