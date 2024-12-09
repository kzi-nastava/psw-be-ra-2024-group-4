using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Public;

public interface IBundleService
{
    Result<BundleDto> Create(BundleDto bundleDto);

    Result<BundleDto> Update(BundleDto bundleDto);
    Result Delete(int bundleId);

    Result<BundleDto> Get(int bundleId);

    Result<PagedResult<BundleDto>> GetPaged(int page, int pageSize);
    Result<PagedResult<BundleDto>> GetByAuthorId(long authorId);
}
