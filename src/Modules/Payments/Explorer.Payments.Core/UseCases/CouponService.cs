using AutoMapper;
using Explorer.BuildingBlocks.Core.Domain;
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
    public class CouponService: CrudService<CouponDto,Coupon>, ICouponService
    {
        private readonly ICouponRepository _couponRepository;
        public CouponService(ICrudRepository<Coupon> crudRepository,IMapper mapper,ICouponRepository couponRepository):base(crudRepository,mapper) {
            _couponRepository = couponRepository;
        }

        public new Result<CouponDto> Create(CouponDto coupon) 
        {
            try
            {
                coupon.PromoCode = Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper();
                var result = CrudRepository.Create(MapToDomain(coupon));
                return MapToDto(result);
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

        public Result<CouponDto> Get(string promoCode)
        {
            if (string.IsNullOrWhiteSpace(promoCode))
            {
                return Result.Fail("Promo code cannot be null or empty.");
            }
            var coupon = _couponRepository.Get(promoCode);
            if (coupon == null)
            {
                return Result.Fail("Coupon with the provided promo code does not exist.");
            }
            return MapToDto(coupon);
        }

        public Result<PagedResult<CouponDto>> GetAll(int authorId, int page, int pageSize)
        {
           var result=_couponRepository.GetAll(authorId, page, pageSize);
           return MapToDto(result);
        }

        public Result<CouponDto> GetByTourId(int id)
        {
           var result=_couponRepository.Get(id); 
           return MapToDto(result);
        }
    }


}
