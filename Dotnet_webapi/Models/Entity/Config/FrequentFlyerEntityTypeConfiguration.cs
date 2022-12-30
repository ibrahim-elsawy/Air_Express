using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dotnet_webapi.Models.Entity.Config
{
	public class FrequentFlyerEntityTypeConfiguration : IEntityTypeConfiguration<FrequentFlyer>
	{
		public void Configure(EntityTypeBuilder<FrequentFlyer> builder)
		{
            builder.HasKey(e => e.FrequentFlyerId).HasName("frequent_flyer_pkey");

            builder.ToTable("frequent_flyer", "postgres_air");

            builder.Property(e => e.FrequentFlyerId).HasColumnName("frequent_flyer_id");
            builder.Property(e => e.AwardPoints).HasColumnName("award_points");
            builder.Property(e => e.CardNum)
                .IsRequired()
                .HasColumnName("card_num");
            builder.Property(e => e.Email)
                .IsRequired()
                .HasColumnName("email");
            builder.Property(e => e.FirstName)
                .IsRequired()
                .HasColumnName("first_name");
            builder.Property(e => e.LastName)
                .IsRequired()
                .HasColumnName("last_name");
            builder.Property(e => e.Level).HasColumnName("level");
            builder.Property(e => e.Phone)
                .IsRequired()
                .HasColumnName("phone");
            builder.Property(e => e.Title)
                .IsRequired()
                .HasColumnName("title");
            builder.Property(e => e.UpdateTs).HasColumnName("update_ts");
		}
	}
}