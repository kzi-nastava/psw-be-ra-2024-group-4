using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounter.Infrastructure.Database
{
    public class EncounterContext : DbContext
    {
        public EncounterContext(DbContextOptions<EncounterContext> options) : base(options) { }

        
        public DbSet<Core.Domain.Encounter> Encounters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.HasDefaultSchema("encounter");

            base.OnModelCreating(modelBuilder);
        }
    }
}
