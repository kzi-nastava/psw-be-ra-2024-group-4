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
    public class BundleService : CrudService<BundleDto, Bundle>, IBundleService
    {
        IBundleRepository _bundleRepository { get; set; }
        

        public BundleService(ICrudRepository<Bundle> repository, IMapper mapper, IBundleRepository bundleRepository) : base(repository, mapper)
        {
            _bundleRepository = bundleRepository;
        }


        public Result<PagedResult<BundleDto>> GetByAuthorId(long authorId)
        {
            var result = _bundleRepository.GetByAuthorId(authorId);
            return MapToDto(result);
        }
    }
}
