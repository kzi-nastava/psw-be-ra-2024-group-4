using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Public
{
    public interface ICouponService
    {
        Result<CouponDto> Create(CouponDto couponDto);
        Result Delete(int couponId);
        Result<CouponDto> Update(CouponDto couponDto);
        Result<PagedResult<CouponDto>> GetAll(int authorId,int page,int pageSize);

        Result<CouponDto> Get(string promoCode);


        Result<CouponDto> GetByTourId(int id);
    }
}
