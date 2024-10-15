using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories
{
    public class ClubInvitationDatabaseRepository : IClubInvitationRepository
    {
        private readonly StakeholdersContext _dbContext;

        public ClubInvitationDatabaseRepository(StakeholdersContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Club GetById(long clubInvitationId)
        {
            return _dbContext.Clubs.FirstOrDefault(c => c.Id == clubInvitationId);
        }
        public ClubInvitation Create(ClubInvitation clubInvitation)
        {
            _dbContext.ClubInvitations.Add(clubInvitation);
            _dbContext.SaveChanges();
            return clubInvitation;
        }

       

    }
}
