﻿using AutoMapper;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.Core.Domain;

namespace Explorer.Payments.Core.Mappers;

public class PaymentsProfile : Profile
{
    public PaymentsProfile()
    {
        CreateMap<OrderItemDto, OrderItem>().ReverseMap();
        CreateMap<ShoppingCartDto, ShoppingCart>().ReverseMap();
        CreateMap<TourPurchaseTokenDto, TourPurchaseToken>().ReverseMap();
        CreateMap<BundleDto, Bundle>().ReverseMap();
        CreateMap<PaymentRecordDto, PaymentRecord>().ReverseMap();

        CreateMap<CouponDto, Coupon>().ReverseMap();    
        CreateMap<SalesDto, Sales>().ReverseMap();  
    }
}

