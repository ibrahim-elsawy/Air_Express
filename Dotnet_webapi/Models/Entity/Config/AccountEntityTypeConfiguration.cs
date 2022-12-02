using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dotnet_webapi.Models.Entity.Config
{
	public class AccountEntityTypeConfiguration : IEntityTypeConfiguration<Account>
	{
		public void Configure(EntityTypeBuilder<Account> builder)
		{
			builder.HasKey(e => e.AccountId).HasName("account_pkey");

			builder.ToTable("account", "postgres_air");

			builder.HasIndex(e => e.LastName, "account_last_name");

			builder.Property(e => e.AccountId).HasColumnName("account_id");

			builder.Property(e => e.FirstName)
				.IsRequired()
				.HasColumnName("first_name");

			builder.Property(e => e.FrequentFlyerId).HasColumnName("frequent_flyer_id");

			builder.Property(e => e.LastName)
				.IsRequired()
				.HasColumnName("last_name");

			builder.Property(e => e.Login)
				.IsRequired()
				.HasColumnName("login");

			builder.Property(e => e.UpdateTs).HasColumnName("update_ts");

			builder.HasOne(d => d.FrequentFlyer).WithMany(p => p.Accounts)
				.HasForeignKey(d => d.FrequentFlyerId)
				.HasConstraintName("frequent_flyer_id_fk");


		}
	}
}