using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dotnet_webapi.Models.Entity.Config
{
	public class FlightEntityTypeConfiguration : IEntityTypeConfiguration<Flight>
	{
		public void Configure(EntityTypeBuilder<Flight> builder)
		{
            builder.HasKey(e => e.FlightId).HasName("flight_pkey");

            builder.ToTable("flight", "postgres_air");

            builder.HasIndex(e => e.DepartureAirport, "flight_departure_airport");

            builder.HasIndex(e => e.ScheduledDeparture, "flight_scheduled_departure");

            builder.HasIndex(e => e.UpdateTs, "flight_update_ts");

            builder.Property(e => e.FlightId).HasColumnName("flight_id");
            builder.Property(e => e.ActualArrival).HasColumnName("actual_arrival");
            builder.Property(e => e.ActualDeparture).HasColumnName("actual_departure");
            builder.Property(e => e.AircraftCode)
                .IsRequired()
                .HasMaxLength(3)
                .IsFixedLength()
                .HasColumnName("aircraft_code");
            builder.Property(e => e.ArrivalAirport)
                .IsRequired()
                .HasMaxLength(3)
                .IsFixedLength()
                .HasColumnName("arrival_airport");
            builder.Property(e => e.DepartureAirport)
                .IsRequired()
                .HasMaxLength(3)
                .IsFixedLength()
                .HasColumnName("departure_airport");
            builder.Property(e => e.FlightNo)
                .IsRequired()
                .HasColumnName("flight_no");
            builder.Property(e => e.ScheduledArrival).HasColumnName("scheduled_arrival");
            builder.Property(e => e.ScheduledDeparture).HasColumnName("scheduled_departure");
            builder.Property(e => e.Status)
                .IsRequired()
                .HasColumnName("status");
            builder.Property(e => e.UpdateTs).HasColumnName("update_ts");

            builder.HasOne(d => d.AircraftCodeNavigation).WithMany(p => p.Flights)
                .HasForeignKey(d => d.AircraftCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("aircraft_code_fk");

            builder.HasOne(d => d.DepartureAirportNavigation).WithMany(p => p.Flights)
                .HasForeignKey(d => d.DepartureAirport)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("departure_airport_fk");
		}
	}
}