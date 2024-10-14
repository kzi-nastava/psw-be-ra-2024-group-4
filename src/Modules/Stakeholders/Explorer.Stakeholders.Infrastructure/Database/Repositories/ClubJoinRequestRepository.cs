using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories
{
    public class ClubJoinRequestRepository:IClubJoinRequestRepository
    {
        private readonly StakeholdersContext _dbContext;

        public ClubJoinRequestRepository(StakeholdersContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<ClubJoinRequest> GetAll()
        {
            return _dbContext.ClubJoinRequests.ToList();
        }
    }
}
