﻿using Explorer.Encounter.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounter.Infrastructure.Database.Repositories
{
    public class EncounterRepository : IEncounterRepository
    {
        private readonly EncounterContext _context;
        private readonly DbSet<Core.Domain.Encounter> _dbSet;

        public EncounterRepository(EncounterContext context)
        {
            _context = context;
            _dbSet = context.Set<Core.Domain.Encounter>();
        }

        public async Task<Core.Domain.Encounter?> GetByIdAsync(Guid id)
        {
            return await _context.Encounters.FindAsync(id);
        }

        public async Task AddAsync(Core.Domain.Encounter challenge)
        {
            await _context.Encounters.AddAsync(challenge);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Core.Domain.Encounter challenge)
        {
            _context.Encounters.Update(challenge);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Core.Domain.Encounter>> GetActiveChallengesAsync()
        {
            return await _context.Encounters
                .Where(c => c.Status == Core.Domain.EncounterStatus.Active)
                .ToListAsync();
        }

        public Core.Domain.Encounter GetById(long id)
        {
            var entity = _dbSet.First(x => x.Id == id);
            if (entity == null) throw new KeyNotFoundException("Not found: " + id);
            return entity;
        }

        public async Task<List<Core.Domain.Encounter>> GetPendingEncounters()
        {
            return _context.Encounters.Where(c => c.RequestStatus == Core.Domain.RequestStatus.Pending).ToList();
        }

    }
}
