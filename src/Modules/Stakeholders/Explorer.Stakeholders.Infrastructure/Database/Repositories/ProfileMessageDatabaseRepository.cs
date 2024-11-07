using Explorer.BuildingBlocks.Core.Domain;
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

        public ProfileMessage GetByClubId(long clubId)
        {
            return _dbContext.ProfileMessages.Where(t => t.ClubId == clubId)
                .Include(t => t.Resource)
                .FirstOrDefault();
        }
        public ProfileMessage GetByUserId(long userId)
        {
            return _dbContext.ProfileMessages.Where(t => t.UserId == userId)
                .Include(t => t.Resource)
                .FirstOrDefault();
        }
        public ProfileMessage Update(ProfileMessage aggregateRoot)
        {
            _dbContext.Entry(aggregateRoot).State = EntityState.Modified;
            _dbContext.SaveChanges();
            return aggregateRoot;
        }
        public void Delete(ProfileMessage aggregateRoot)
        {
            _dbContext.Entry(aggregateRoot.Resource).State = EntityState.Deleted;
            _dbContext.Entry(aggregateRoot).State = EntityState.Deleted;
            _dbContext.Remove(aggregateRoot);
            _dbContext.SaveChanges();
        }
    }
}
