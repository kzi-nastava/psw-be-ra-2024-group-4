using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Payments.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Explorer.Payments.Infrastructure.Database
{
    public class PaymentsContext : DbContext
    {
        //TODO: definicija DbSet za svaki entitet
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<TourPurchaseToken> PurchaseTokens { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<Sales> Sales { get; set; }

        public DbSet<Bundle> Bundles { get; set; }

        public DbSet<PaymentRecord> PaymentRecords { get; set; }
        public PaymentsContext(DbContextOptions<PaymentsContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("payments");
            ConfigureShoppingCart(modelBuilder);
        }

        private static void ConfigureShoppingCart(ModelBuilder modelBuilder)
        {
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
}
