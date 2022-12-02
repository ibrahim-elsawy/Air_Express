using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dotnet_webapi.Models.Entity.Config
{
	public class BoardingPassEntityTypeConfiguration : IEntityTypeConfiguration<BoardingPass>
	{
		public void Configure(EntityTypeBuilder<BoardingPass> builder)
		{
            builder.HasKey(e => e.PassId).HasName("boarding_pass_pkey");

            builder.ToTable("boarding_pass", "postgres_air");

            builder.Property(e => e.PassId).HasColumnName("pass_id");
            builder.Property(e => e.BoardingTime).HasColumnName("boarding_time");
            builder.Property(e => e.BookingLegId).HasColumnName("booking_leg_id");
            builder.Property(e => e.PassengerId).HasColumnName("passenger_id");
            builder.Property(e => e.Precheck).HasColumnName("precheck");
            builder.Property(e => e.Seat).HasColumnName("seat");
            builder.Property(e => e.UpdateTs).HasColumnName("update_ts");

            builder.HasOne(d => d.BookingLeg).WithMany(p => p.BoardingPasses)
                .HasForeignKey(d => d.BookingLegId)
                .HasConstraintName("booking_leg_id_fk");

            builder.HasOne(d => d.Passenger).WithMany(p => p.BoardingPasses)
                .HasForeignKey(d => d.PassengerId)
                .HasConstraintName("passenger_id_fk");
		}
	}
}