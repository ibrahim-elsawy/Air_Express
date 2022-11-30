using System;
using System.Collections.Generic;
using Dotnet_webapi.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace Dotnet_webapi.Models;

public partial class PostgresContext : DbContext
{
    public PostgresContext()
    {
    }

    public PostgresContext(DbContextOptions<PostgresContext> options)
        : base(options)
    {
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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=postgres;Username=postgres;Password=Sawy4507@");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("account_pkey");

            entity.ToTable("account", "postgres_air");

            entity.HasIndex(e => e.LastName, "account_last_name");

            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasColumnName("first_name");
            entity.Property(e => e.FrequentFlyerId).HasColumnName("frequent_flyer_id");
            entity.Property(e => e.LastName)
                .IsRequired()
                .HasColumnName("last_name");
            entity.Property(e => e.Login)
                .IsRequired()
                .HasColumnName("login");
            entity.Property(e => e.UpdateTs).HasColumnName("update_ts");

            entity.HasOne(d => d.FrequentFlyer).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.FrequentFlyerId)
                .HasConstraintName("frequent_flyer_id_fk");
        });

        modelBuilder.Entity<Aircraft>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("aircraft_pkey");

            entity.ToTable("aircraft", "postgres_air");

            entity.Property(e => e.Code).HasColumnName("code");
            entity.Property(e => e.Class).HasColumnName("class");
            entity.Property(e => e.Model).HasColumnName("model");
            entity.Property(e => e.Range).HasColumnName("range");
            entity.Property(e => e.Velocity).HasColumnName("velocity");
        });

        modelBuilder.Entity<Airport>(entity =>
        {
            entity.HasKey(e => e.AirportCode).HasName("airport_pkey");

            entity.ToTable("airport", "postgres_air");

            entity.Property(e => e.AirportCode)
                .HasMaxLength(3)
                .IsFixedLength()
                .HasColumnName("airport_code");
            entity.Property(e => e.AirportName)
                .IsRequired()
                .HasColumnName("airport_name");
            entity.Property(e => e.AirportTz)
                .IsRequired()
                .HasColumnName("airport_tz");
            entity.Property(e => e.City)
                .IsRequired()
                .HasColumnName("city");
            entity.Property(e => e.Continent).HasColumnName("continent");
            entity.Property(e => e.Intnl).HasColumnName("intnl");
            entity.Property(e => e.IsoCountry).HasColumnName("iso_country");
            entity.Property(e => e.IsoRegion).HasColumnName("iso_region");
            entity.Property(e => e.UpdateTs).HasColumnName("update_ts");
        });

        modelBuilder.Entity<BoardingPass>(entity =>
        {
            entity.HasKey(e => e.PassId).HasName("boarding_pass_pkey");

            entity.ToTable("boarding_pass", "postgres_air");

            entity.Property(e => e.PassId).HasColumnName("pass_id");
            entity.Property(e => e.BoardingTime).HasColumnName("boarding_time");
            entity.Property(e => e.BookingLegId).HasColumnName("booking_leg_id");
            entity.Property(e => e.PassengerId).HasColumnName("passenger_id");
            entity.Property(e => e.Precheck).HasColumnName("precheck");
            entity.Property(e => e.Seat).HasColumnName("seat");
            entity.Property(e => e.UpdateTs).HasColumnName("update_ts");

            entity.HasOne(d => d.BookingLeg).WithMany(p => p.BoardingPasses)
                .HasForeignKey(d => d.BookingLegId)
                .HasConstraintName("booking_leg_id_fk");

            entity.HasOne(d => d.Passenger).WithMany(p => p.BoardingPasses)
                .HasForeignKey(d => d.PassengerId)
                .HasConstraintName("passenger_id_fk");
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("booking_pkey");

            entity.ToTable("booking", "postgres_air");

            entity.HasIndex(e => e.BookingRef, "booking_booking_ref_key").IsUnique();

            entity.Property(e => e.BookingId)
                .ValueGeneratedNever()
                .HasColumnName("booking_id");
            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.BookingName).HasColumnName("booking_name");
            entity.Property(e => e.BookingRef)
                .IsRequired()
                .HasColumnName("booking_ref");
            entity.Property(e => e.Email)
                .IsRequired()
                .HasColumnName("email");
            entity.Property(e => e.Phone)
                .IsRequired()
                .HasColumnName("phone");
            entity.Property(e => e.Price)
                .HasPrecision(7, 2)
                .HasColumnName("price");
            entity.Property(e => e.UpdateTs).HasColumnName("update_ts");

            entity.HasOne(d => d.Account).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("booking_account_id_fk");
        });

        modelBuilder.Entity<BookingLeg>(entity =>
        {
            entity.HasKey(e => e.BookingLegId).HasName("booking_leg_pkey");

            entity.ToTable("booking_leg", "postgres_air");

            entity.HasIndex(e => e.BookingId, "booking_leg_booking_id");

            entity.HasIndex(e => e.UpdateTs, "booking_leg_update_ts");

            entity.Property(e => e.BookingLegId).HasColumnName("booking_leg_id");
            entity.Property(e => e.BookingId).HasColumnName("booking_id");
            entity.Property(e => e.FlightId).HasColumnName("flight_id");
            entity.Property(e => e.IsReturning).HasColumnName("is_returning");
            entity.Property(e => e.LegNum).HasColumnName("leg_num");
            entity.Property(e => e.UpdateTs).HasColumnName("update_ts");

            entity.HasOne(d => d.Booking).WithMany(p => p.BookingLegs)
                .HasForeignKey(d => d.BookingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("booking_id_fk");

            entity.HasOne(d => d.Flight).WithMany(p => p.BookingLegs)
                .HasForeignKey(d => d.FlightId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("flight_id_fk");
        });

        modelBuilder.Entity<Flight>(entity =>
        {
            entity.HasKey(e => e.FlightId).HasName("flight_pkey");

            entity.ToTable("flight", "postgres_air");

            entity.HasIndex(e => e.DepartureAirport, "flight_departure_airport");

            entity.HasIndex(e => e.ScheduledDeparture, "flight_scheduled_departure");

            entity.HasIndex(e => e.UpdateTs, "flight_update_ts");

            entity.Property(e => e.FlightId).HasColumnName("flight_id");
            entity.Property(e => e.ActualArrival).HasColumnName("actual_arrival");
            entity.Property(e => e.ActualDeparture).HasColumnName("actual_departure");
            entity.Property(e => e.AircraftCode)
                .IsRequired()
                .HasMaxLength(3)
                .IsFixedLength()
                .HasColumnName("aircraft_code");
            entity.Property(e => e.ArrivalAirport)
                .IsRequired()
                .HasMaxLength(3)
                .IsFixedLength()
                .HasColumnName("arrival_airport");
            entity.Property(e => e.DepartureAirport)
                .IsRequired()
                .HasMaxLength(3)
                .IsFixedLength()
                .HasColumnName("departure_airport");
            entity.Property(e => e.FlightNo)
                .IsRequired()
                .HasColumnName("flight_no");
            entity.Property(e => e.ScheduledArrival).HasColumnName("scheduled_arrival");
            entity.Property(e => e.ScheduledDeparture).HasColumnName("scheduled_departure");
            entity.Property(e => e.Status)
                .IsRequired()
                .HasColumnName("status");
            entity.Property(e => e.UpdateTs).HasColumnName("update_ts");

            entity.HasOne(d => d.AircraftCodeNavigation).WithMany(p => p.Flights)
                .HasForeignKey(d => d.AircraftCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("aircraft_code_fk");

            entity.HasOne(d => d.DepartureAirportNavigation).WithMany(p => p.Flights)
                .HasForeignKey(d => d.DepartureAirport)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("departure_airport_fk");
        });

        modelBuilder.Entity<FrequentFlyer>(entity =>
        {
            entity.HasKey(e => e.FrequentFlyerId).HasName("frequent_flyer_pkey");

            entity.ToTable("frequent_flyer", "postgres_air");

            entity.Property(e => e.FrequentFlyerId).HasColumnName("frequent_flyer_id");
            entity.Property(e => e.AwardPoints).HasColumnName("award_points");
            entity.Property(e => e.CardNum)
                .IsRequired()
                .HasColumnName("card_num");
            entity.Property(e => e.Email)
                .IsRequired()
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .IsRequired()
                .HasColumnName("last_name");
            entity.Property(e => e.Level).HasColumnName("level");
            entity.Property(e => e.Phone)
                .IsRequired()
                .HasColumnName("phone");
            entity.Property(e => e.Title)
                .IsRequired()
                .HasColumnName("title");
            entity.Property(e => e.UpdateTs).HasColumnName("update_ts");
        });

        modelBuilder.Entity<Passenger>(entity =>
        {
            entity.HasKey(e => e.PassengerId).HasName("passenger_pkey");

            entity.ToTable("passenger", "postgres_air");

            entity.Property(e => e.PassengerId).HasColumnName("passenger_id");
            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.BookingId).HasColumnName("booking_id");
            entity.Property(e => e.BookingRef).HasColumnName("booking_ref");
            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .IsRequired()
                .HasColumnName("last_name");
            entity.Property(e => e.PassengerNo).HasColumnName("passenger_no");
            entity.Property(e => e.UpdateTs).HasColumnName("update_ts");

            entity.HasOne(d => d.Account).WithMany(p => p.Passengers)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("pass_frequent_flyer_id_fk");

            entity.HasOne(d => d.Booking).WithMany(p => p.Passengers)
                .HasForeignKey(d => d.BookingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("pass_booking_id_fk");
        });

        modelBuilder.Entity<Phone>(entity =>
        {
            entity.HasKey(e => e.PhoneId).HasName("phone_pkey");

            entity.ToTable("phone", "postgres_air");

            entity.Property(e => e.PhoneId).HasColumnName("phone_id");
            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.Phone1).HasColumnName("phone");
            entity.Property(e => e.PhoneType).HasColumnName("phone_type");
            entity.Property(e => e.PrimaryPhone).HasColumnName("primary_phone");
            entity.Property(e => e.UpdateTs).HasColumnName("update_ts");

            entity.HasOne(d => d.Account).WithMany(p => p.Phones)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("phone_account_id_fk");
        });
        modelBuilder.HasSequence("boarding_pass_pass_id_seq", "postgres_air");
        modelBuilder.HasSequence("booking_number", "postgres_air");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
