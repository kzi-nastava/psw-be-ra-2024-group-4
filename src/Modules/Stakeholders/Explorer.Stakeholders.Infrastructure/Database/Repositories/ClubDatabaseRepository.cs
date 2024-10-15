using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories
{
    public class ClubDatabaseRepository:IClubRepository
    {

        private readonly StakeholdersContext _dbContext;
        public ClubDatabaseRepository(StakeholdersContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Club GetById(long clubId)
        {
            return _dbContext.Clubs.FirstOrDefault(c => c.Id == clubId);
        }

       
    }
}
