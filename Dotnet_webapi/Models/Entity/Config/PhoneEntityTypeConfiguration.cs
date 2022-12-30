using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dotnet_webapi.Models.Entity.Config
{
	public class PhoneEntityTypeConfiguration : IEntityTypeConfiguration<Phone>
	{
		public void Configure(EntityTypeBuilder<Phone> builder)
		{
            builder.HasKey(e => e.PhoneId).HasName("phone_pkey");

            builder.ToTable("phone", "postgres_air");

            builder.Property(e => e.PhoneId).HasColumnName("phone_id");
            builder.Property(e => e.AccountId).HasColumnName("account_id");
            builder.Property(e => e.Phone1).HasColumnName("phone");
            builder.Property(e => e.PhoneType).HasColumnName("phone_type");
            builder.Property(e => e.PrimaryPhone).HasColumnName("primary_phone");
            builder.Property(e => e.UpdateTs).HasColumnName("update_ts");

            builder.HasOne(d => d.Account).WithMany(p => p.Phones)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("phone_account_id_fk");
		}
	}
}