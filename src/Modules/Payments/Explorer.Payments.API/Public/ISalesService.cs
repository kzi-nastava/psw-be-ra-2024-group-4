using Explorer.Payments.API.Dtos;
using Explorer.Tours.API.Dtos;

using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Public
{
    public interface ISalesService
    {
        Result<SalesDto> Create(SalesDto salesDto);
        Result Delete(int salesId);
        Result<SalesDto> Update(SalesDto salesDto);
        public Result<List<TourOverviewDto>> GetDiscountedTours(List<TourOverviewDto> allTours);
        Result<List<SalesDto>> GetAll(long userId);




    }
}
