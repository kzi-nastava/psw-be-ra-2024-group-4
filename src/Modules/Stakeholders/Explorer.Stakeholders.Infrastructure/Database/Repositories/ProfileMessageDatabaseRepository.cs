using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.ProfileMessages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories
{
    public class ProfileMessageDatabaseRepository : IProfileMessageRepository
    {
        private readonly StakeholdersContext _dbContext;

        ProfileMessageDatabaseRepository(StakeholdersContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public ProfileMessage Create(ProfileMessage profileMessage)
        {
            _dbContext.ProfileMessages.Add(profileMessage);
            _dbContext.SaveChanges();
            return profileMessage;
        }

        public PagedResult<ProfileMessage> GetByClubId(long clubId)
        {
            throw new NotImplementedException();
        }

        public PagedResult<ProfileMessage> GetByUserId(long userId)
        {
            throw new NotImplementedException();
        }
    }
}
