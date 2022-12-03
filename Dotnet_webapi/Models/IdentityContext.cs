// using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore;

// namespace Dotnet_webapi.Models
// {
// 	public class IdentityContext : IdentityDbContext
// 	{
//         private readonly IConfiguration Configuration;
// 		public IdentityContext(DbContextOptions<IdentityContext> options,IConfiguration configuration)
// 	    : base(options)
// 		{
// 			Configuration = configuration;
// 		}
//     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//         => optionsBuilder.UseNpgsql(Configuration.GetConnectionString("PostgresDB"));
// 		protected override void OnModelCreating(ModelBuilder builder)
// 		{
// 			base.OnModelCreating(builder);
// 		}
// 	}
// }