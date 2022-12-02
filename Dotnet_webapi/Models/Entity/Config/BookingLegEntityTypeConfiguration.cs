using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dotnet_webapi.Models.Entity.Config
{
	public class BookingLegEntityTypeConfiguration : IEntityTypeConfiguration<BookingLeg>
	{
		public void Configure(EntityTypeBuilder<BookingLeg> builder)
		{
            builder.HasKey(e => e.BookingLegId).HasName("booking_leg_pkey");

            builder.ToTable("booking_leg", "postgres_air");

            builder.HasIndex(e => e.BookingId, "booking_leg_booking_id");

            builder.HasIndex(e => e.UpdateTs, "booking_leg_update_ts");

            builder.Property(e => e.BookingLegId).HasColumnName("booking_leg_id");
            builder.Property(e => e.BookingId).HasColumnName("booking_id");
            builder.Property(e => e.FlightId).HasColumnName("flight_id");
            builder.Property(e => e.IsReturning).HasColumnName("is_returning");
            builder.Property(e => e.LegNum).HasColumnName("leg_num");
            builder.Property(e => e.UpdateTs).HasColumnName("update_ts");

            builder.HasOne(d => d.Booking).WithMany(p => p.BookingLegs)
                .HasForeignKey(d => d.BookingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("booking_id_fk");

            builder.HasOne(d => d.Flight).WithMany(p => p.BookingLegs)
                .HasForeignKey(d => d.FlightId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("flight_id_fk");


		}
	}
}