using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dotnet_webapi.Models.Entity.Config
{
	public class PassengerEntityTypeConfiguration : IEntityTypeConfiguration<Passenger>
	{
		public void Configure(EntityTypeBuilder<Passenger> builder)
		{
            builder.HasKey(e => e.PassengerId).HasName("passenger_pkey");

            builder.ToTable("passenger", "postgres_air");

            builder.Property(e => e.PassengerId).HasColumnName("passenger_id");
            builder.Property(e => e.AccountId).HasColumnName("account_id");
            builder.Property(e => e.Age).HasColumnName("age");
            builder.Property(e => e.BookingId).HasColumnName("booking_id");
            builder.Property(e => e.BookingRef).HasColumnName("booking_ref");
            builder.Property(e => e.FirstName)
                .IsRequired()
                .HasColumnName("first_name");
            builder.Property(e => e.LastName)
                .IsRequired()
                .HasColumnName("last_name");
            builder.Property(e => e.PassengerNo).HasColumnName("passenger_no");
            builder.Property(e => e.UpdateTs).HasColumnName("update_ts");

            builder.HasOne(d => d.Account).WithMany(p => p.Passengers)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("pass_frequent_flyer_id_fk");

            builder.HasOne(d => d.Booking).WithMany(p => p.Passengers)
                .HasForeignKey(d => d.BookingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("pass_booking_id_fk");
		}
	}
}