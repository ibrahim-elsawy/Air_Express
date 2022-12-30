using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dotnet_webapi.Models.Entity.Config
{
	public class BookingEntityTypeConfiguration : IEntityTypeConfiguration<Booking>
	{
		public void Configure(EntityTypeBuilder<Booking> builder)
		{
            builder.HasKey(e => e.BookingId).HasName("booking_pkey");

            builder.ToTable("booking", "postgres_air");

            builder.HasIndex(e => e.BookingRef, "booking_booking_ref_key").IsUnique();

            builder.Property(e => e.BookingId)
                .ValueGeneratedNever()
                .HasColumnName("booking_id");
            builder.Property(e => e.AccountId).HasColumnName("account_id");
            builder.Property(e => e.BookingName).HasColumnName("booking_name");
            builder.Property(e => e.BookingRef)
                .IsRequired()
                .HasColumnName("booking_ref");
            builder.Property(e => e.Email)
                .IsRequired()
                .HasColumnName("email");
            builder.Property(e => e.Phone)
                .IsRequired()
                .HasColumnName("phone");
            builder.Property(e => e.Price)
                .HasPrecision(7, 2)
                .HasColumnName("price");
            builder.Property(e => e.UpdateTs).HasColumnName("update_ts");

            builder.HasOne(d => d.Account).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("booking_account_id_fk");
		}
	}
}