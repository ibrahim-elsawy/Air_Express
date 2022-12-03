using System;
using System.Collections.Generic;
using Dotnet_webapi.Models.Entity;
using Dotnet_webapi.Models.Entity.Config;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Dotnet_webapi.Models;

// public partial class PostgresContext : DbContext
public partial class PostgresContext : IdentityDbContext
{

    private readonly IConfiguration Configuration;
    public PostgresContext()
    {
    }

    public PostgresContext(DbContextOptions<PostgresContext> options, IConfiguration configuration)
        : base(options)
    {
        Configuration = configuration;
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Aircraft> Aircraft { get; set; }

    public virtual DbSet<Airport> Airports { get; set; }

    public virtual DbSet<BoardingPass> BoardingPasses { get; set; }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<BookingLeg> BookingLegs { get; set; }

    public virtual DbSet<Flight> Flights { get; set; }

    public virtual DbSet<FrequentFlyer> FrequentFlyers { get; set; }

    public virtual DbSet<Passenger> Passengers { get; set; }

    public virtual DbSet<Phone> Phones { get; set; }
    public virtual DbSet<RefreshToken> RefreshToken { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(Configuration.GetConnectionString("PostgresDB"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

		new AccountEntityTypeConfiguration().Configure(modelBuilder.Entity<Account>());

		new AircraftEntityTypeConfiguration().Configure(modelBuilder.Entity<Aircraft>());

		new AirportEntityTypeConfiguration().Configure(modelBuilder.Entity<Airport>());

		new BoardingPassEntityTypeConfiguration().Configure(modelBuilder.Entity<BoardingPass>());


		new BookingEntityTypeConfiguration().Configure(modelBuilder.Entity<Booking>());


		new BookingLegEntityTypeConfiguration().Configure(modelBuilder.Entity<BookingLeg>());

		new FlightEntityTypeConfiguration().Configure(modelBuilder.Entity<Flight>());


		new FrequentFlyerEntityTypeConfiguration().Configure(modelBuilder.Entity<FrequentFlyer>());

        new PassengerEntityTypeConfiguration().Configure(modelBuilder.Entity<Passenger>());

        new PhoneEntityTypeConfiguration().Configure(modelBuilder.Entity<Phone>());


        modelBuilder.HasSequence("boarding_pass_pass_id_seq", "postgres_air");
        modelBuilder.HasSequence("booking_number", "postgres_air");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
