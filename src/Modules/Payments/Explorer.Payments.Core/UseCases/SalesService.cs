using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;

namespace Explorer.Payments.Core.UseCases
{
    public class SalesService : CrudService<SalesDto, Sales>, ISalesService
    {
        public SalesService(ICrudRepository<Sales> crudRepository, IMapper mapper) : base(crudRepository, mapper)
        {
        }
    }
    
}
