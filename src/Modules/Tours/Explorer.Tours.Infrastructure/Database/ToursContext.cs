using Explorer.Modules.Core.Domain;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.TourExecutions;
using Explorer.Tours.Core.Domain.Tours;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Text.Json;

namespace Explorer.Tours.Infrastructure.Database;

public class ToursContext : DbContext
{
    public DbSet<Equipment> Equipment { get; set; }
    public DbSet<TourReview> TourReview { get; set; }
    public DbSet<TourPreference> TourPreferences { get; set; }

    public DbSet<KeyPoint> KeyPoints { get; set; }
    public DbSet<Tour> Tour { get; set; }
    public DbSet<ShoppingCart> ShoppingCarts { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Explorer.Tours.Core.Domain.Object> Objects { get; set; }

    public DbSet<TourPurchaseToken> PurchaseTokens { get; set; }

    public DbSet<TourExecution> TourExecution { get; set; }
    public DbSet<PositionSimulator> Positions { get; set; }

    public ToursContext(DbContextOptions<ToursContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("tours");

        // Configure TourExecution as JSON
        modelBuilder.Entity<TourExecution>().Property(item => item.CompletedKeys).HasColumnType("jsonb");
        ConfigureTourExecution(modelBuilder);

        // Add a ValueComparer for the Durations property to fix the warning
        modelBuilder.Entity<Tour>()
            .Property(t => t.Durations)
            .HasConversion(
                v => JsonSerializer.Serialize(v, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }),
                v => JsonSerializer.Deserialize<List<TourDuration>>(v, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase })
            )
            .HasColumnType("jsonb")
            .Metadata.SetValueComparer(
                new ValueComparer<List<TourDuration>>(
                    (c1, c2) => c1.SequenceEqual(c2),   // Comparison logic
                    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())), // Hash code computation
                    c => c.ToList()) // Clone logic for tracking changes
            );

        modelBuilder.Entity<PositionSimulator>()
            .HasIndex(ps => ps.TouristId)
            .IsUnique();

        modelBuilder.Entity<Tour>()
          .HasMany(t => t.KeyPoints)
          .WithOne()
          .HasForeignKey(kp => kp.TourId);

        ConfigureTour(modelBuilder);
    }




    private static void ConfigureTourExecution(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TourExecution>()
            .HasOne<Tour>()
            .WithMany()
            .HasForeignKey(s => s.TourId);

        modelBuilder.Entity<TourExecution>()
            .HasOne<PositionSimulator>()
            .WithMany()
            .HasForeignKey(s => s.LocationId);

    }
    private static void ConfigureTour(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tour>()
           .HasMany(t => t.KeyPoints)
           .WithOne()
           .HasForeignKey(kp => kp.TourId);
        modelBuilder.Entity<ShoppingCart>()
            .HasMany(sc => sc.Items)
            .WithOne()
            .HasForeignKey(sc => sc.CartId);

        modelBuilder.Entity<ShoppingCart>()
            .HasMany(sc => sc.PurchaseTokens)
            .WithOne()
            .HasForeignKey(sc => sc.CartId);

      
    }

}