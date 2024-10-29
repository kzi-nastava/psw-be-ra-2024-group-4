using Explorer.Modules.Core.Domain;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.TourExecutions;
using Explorer.Tours.Core.Domain.Tours;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Tours.Infrastructure.Database;

public class ToursContext : DbContext
{
    public DbSet<Equipment> Equipment { get; set; }
    public DbSet<TourReview> TourReview { get; set; }
    public DbSet<TourPreference> TourPreferences { get; set; }

    public DbSet<KeyPoint> KeyPoints { get; set; }
    public DbSet<Tour> Tour { get; set; }

    public DbSet<TourExecution> TourExecution { get; set; }

    public DbSet<Explorer.Tours.Core.Domain.Object> Objects { get; set; }

    public DbSet<PositionSimulator> Positions { get; set; }
    public ToursContext(DbContextOptions<ToursContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("tours");


        modelBuilder.Entity<TourExecution>().Property(item => item.CompletedKeys).HasColumnType("jsonb"); //value object cuva kao json


        modelBuilder.Entity<PositionSimulator>()
          .HasIndex(ps => ps.TouristId)
          .IsUnique();

        modelBuilder.Entity<Tour>()
          .HasMany(t => t.KeyPoints)
          .WithOne()
          .HasForeignKey(kp => kp.TourId);
    }

   


}