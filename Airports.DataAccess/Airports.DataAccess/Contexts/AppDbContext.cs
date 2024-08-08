﻿using Airports.DataAccess.Models.DbModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Airports.DataAccess.Contexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Country> Countries { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<Airport> Airports { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Country>()
            .HasIndex(p => p.Iata)
            .IsUnique();

        modelBuilder.Entity<City>()
            .HasIndex(p => p.Iata)
            .IsUnique();

        modelBuilder.Entity<City>()
            .HasOne(p => p.Country)
            .WithMany(t => t.Cities)
            .HasForeignKey(p => p.CountryId);

        modelBuilder.Entity<Airport>()
            .HasIndex(p => p.Iata)
            .IsUnique();

        modelBuilder.Entity<Airport>()
            .HasOne(p => p.City)
            .WithMany(t => t.Airports)
            .HasForeignKey(p => p.CityId);
    }
}