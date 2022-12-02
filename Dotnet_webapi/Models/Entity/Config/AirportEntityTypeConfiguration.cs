using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dotnet_webapi.Models.Entity.Config
{
	public class AirportEntityTypeConfiguration : IEntityTypeConfiguration<Airport>
	{
		public void Configure(EntityTypeBuilder<Airport> builder)
		{
            builder.HasKey(e => e.AirportCode).HasName("airport_pkey");

            builder.ToTable("airport", "postgres_air");

            builder.Property(e => e.AirportCode)
                .HasMaxLength(3)
                .IsFixedLength()
                .HasColumnName("airport_code");
            builder.Property(e => e.AirportName)
                .IsRequired()
                .HasColumnName("airport_name");
            builder.Property(e => e.AirportTz)
                .IsRequired()
                .HasColumnName("airport_tz");
            builder.Property(e => e.City)
                .IsRequired()
                .HasColumnName("city");
            builder.Property(e => e.Continent).HasColumnName("continent");
            builder.Property(e => e.Intnl).HasColumnName("intnl");
            builder.Property(e => e.IsoCountry).HasColumnName("iso_country");
            builder.Property(e => e.IsoRegion).HasColumnName("iso_region");
            builder.Property(e => e.UpdateTs).HasColumnName("update_ts");


		}
	}
}