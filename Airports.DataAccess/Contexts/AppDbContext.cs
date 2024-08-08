using Airports.DataAccess.Models.DbModels;
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

    public DbSet<Location> Locations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Country>()
            .HasIndex(p => p.Iata)
            .IsUnique();

        modelBuilder.Entity<Country>(entity =>
        {
            entity.Property<string>(e => e.Iata)
              .HasColumnType("nvarchar(25)");
            entity.Property<string?>(e => e.Name)
              .HasColumnType("nvarchar(100)");
        });

        modelBuilder.Entity<City>()
            .HasIndex(p => p.Iata)
            .IsUnique();

        modelBuilder.Entity<City>()
            .HasOne(p => p.Country)
            .WithMany(t => t.Cities)
            .HasForeignKey(p => p.CountryId);

        modelBuilder.Entity<City>(entity =>
        {
            entity.Property<string>(e => e.Iata)
              .HasColumnType("nvarchar(25)");
            entity.Property<string?>(e => e.Name)
              .HasColumnType("nvarchar(100)");
        });

        modelBuilder.Entity<Airport>()
            .HasIndex(p => p.Iata)
            .IsUnique();

        modelBuilder.Entity<Airport>()
            .HasOne(p => p.City)
            .WithMany(t => t.Airports)
            .HasForeignKey(p => p.CityId);

        modelBuilder.Entity<Airport>()
            .HasOne(p => p.Location)
            .WithMany(t => t.Airports)
            .HasForeignKey(p => p.LocationId);

        modelBuilder.Entity<Airport>(entity =>
        {
            entity.Property<string?>(e => e.Icao)
              .HasColumnType("nvarchar(25)");
            entity.Property<string>(e => e.Iata)
              .HasColumnType("nvarchar(25)");
            entity.Property<string?>(e => e.Name)
              .HasColumnType("nvarchar(100)");
        });

        modelBuilder.Entity<Location>()
            .HasIndex(p => p.Longitude);

        modelBuilder.Entity<Location>()
            .HasIndex(p => p.Lattitude);

        modelBuilder.Entity<Location>(entity =>
        {
            entity.Property<decimal>(e => e.Longitude)
              .HasColumnType("decimal(9, 6)");
            entity.Property<decimal>(e => e.Lattitude)
             .HasColumnType("decimal(8, 6)");
        });
    }
}