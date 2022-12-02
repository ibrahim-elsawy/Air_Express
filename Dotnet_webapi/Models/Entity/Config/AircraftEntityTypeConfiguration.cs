using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dotnet_webapi.Models.Entity.Config
{
	public class AircraftEntityTypeConfiguration : IEntityTypeConfiguration<Aircraft>
	{
		public void Configure(EntityTypeBuilder<Aircraft> builder)
		{
			builder.HasKey(e => e.Code).HasName("aircraft_pkey");

			builder.ToTable("aircraft", "postgres_air");

			builder.Property(e => e.Code).HasColumnName("code");
			builder.Property(e => e.Class).HasColumnName("class");
			builder.Property(e => e.Model).HasColumnName("model");
			builder.Property(e => e.Range).HasColumnName("range");
			builder.Property(e => e.Velocity).HasColumnName("velocity");
		}
	}
}