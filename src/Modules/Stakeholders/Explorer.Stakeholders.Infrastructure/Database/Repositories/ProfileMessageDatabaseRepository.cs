using Explorer.BuildingBlocks.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Modules.Core.Domain;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.ProfileMessages;
using Microsoft.AspNetCore.Http;
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

        public ProfileMessageDatabaseRepository(StakeholdersContext dbContext) 
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
            var messages = _dbContext.ProfileMessages.Where(t => t.ClubId == clubId)
                .ToList();
            var pagedResult = new PagedResult<ProfileMessage>(messages, messages.Count);
            return pagedResult;
        }
        public PagedResult<ProfileMessage> GetByUserId(long userId)
        {
            var messages = _dbContext.ProfileMessages.Where(t => t.UserId == userId)
                .ToList();
            var pagedResult = new PagedResult<ProfileMessage>(messages, messages.Count);
            return pagedResult;
        }
        public ProfileMessage Update(ProfileMessage aggregateRoot)
        {
            _dbContext.Entry(aggregateRoot).State = EntityState.Modified;
            _dbContext.SaveChanges();
            return aggregateRoot;
        }
        public bool Delete(long aggregateRootId)
        {
            ProfileMessage aggregateRoot = _dbContext.ProfileMessages.Find(aggregateRootId);
            if (aggregateRoot == null) return false;
            _dbContext.Entry(aggregateRoot).State = EntityState.Deleted;
            _dbContext.Remove(aggregateRoot);
            _dbContext.SaveChanges();
            return true;
        }
    }
}
