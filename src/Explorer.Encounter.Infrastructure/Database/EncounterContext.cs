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

            // Configure owned types for Encounter
            modelBuilder.Entity<Core.Domain.Encounter>(builder =>
            {
                builder.Property(e => e.Instances)
                    .HasColumnType("jsonb");

                builder.OwnsOne(e => e.HiddenLocationDetails, hidden =>
                {
                    hidden.Property(h => h.ImageUrl)
                          .IsRequired(false); // Optional property
                    hidden.Property(h => h.ActivationRadius)
                          .IsRequired(); // Required property
                });

                builder.OwnsOne(e => e.SocialDetails, social =>
                {
                    social.Property(s => s.RequiredParticipants)
                          .IsRequired();
                    social.Property(s => s.Radius)
                          .IsRequired();
                });

                builder.OwnsOne(e => e.MiscDetails, misc =>
                {
                    misc.Property(m => m.ActionDescription)
                          .IsRequired(false); // Optional property
                });
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
