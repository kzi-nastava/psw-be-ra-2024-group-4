﻿using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.ProfileMessages;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace Explorer.Stakeholders.Infrastructure.Database;

public class StakeholdersContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Person> People { get; set; }
    public DbSet<AppReview> AppReviews { get; set; }
    public DbSet<Problem> Problem { get; set; }

    public DbSet<Club> Clubs { get; set; }
    public DbSet<ClubInvitation> ClubInvitations { get; set; }
    public DbSet<ClubJoinRequest> ClubJoinRequests { get; set; }
    public DbSet<ProfileMessage> ProfileMessages { get; set; }


    public StakeholdersContext(DbContextOptions<StakeholdersContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("stakeholders");

        modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();
        modelBuilder.Entity<Problem>().HasIndex(p => p.Id).IsUnique();
        modelBuilder.Entity<ProfileMessage>().Property(item => item.Resource).HasColumnType("jsonb");

        ConfigureStakeholder(modelBuilder);
        ConfigureAppReview(modelBuilder);
    }

    private static void ConfigureStakeholder(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>()
            .HasOne<User>()
            .WithOne()
            .HasForeignKey<Person>(s => s.UserId);
    }

    private static void ConfigureAppReview(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AppReview>()
            .HasOne<User>() 
            .WithOne() 
            .HasForeignKey<AppReview>(ar => ar.UserId); 
    }
}