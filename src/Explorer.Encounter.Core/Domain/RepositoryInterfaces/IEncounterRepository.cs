using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounter.Core.Domain.RepositoryInterfaces
{
    public interface IEncounterRepository
    {
        Task<Encounter?> GetByIdAsync(Guid id);
        Task AddAsync(Encounter encounter);
        Task UpdateAsync(Encounter encounter);
        Task<List<Encounter>> GetActiveChallengesAsync();
        public Encounter GetById(long id);
        Task<List<Encounter>> GetPendingEncounters();

    }
}
