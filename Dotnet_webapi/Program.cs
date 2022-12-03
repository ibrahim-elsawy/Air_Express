using System.Text;
using Dotnet_webapi.Config;
using Dotnet_webapi.Models;
using Dotnet_webapi.Models.DAO;
using Dotnet_webapi.Models.Repository;
using Dotnet_webapi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<PostgresContext>();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
        .AddEntityFrameworkStores<PostgresContext>();  

builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("JwtConfig")); 
var key = Encoding.ASCII.GetBytes(builder.Configuration["JwtConfig:Secret"]); 
var tokenValidationParams = new TokenValidationParameters {
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(key),
				ValidateIssuer = false,
				ValidateAudience = false,
				ValidateLifetime = true,
				RequireExpirationTime = false,
				ClockSkew = TimeSpan.Zero
			}; 
builder.Services.AddSingleton(tokenValidationParams);
builder.Services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(jwt =>
			{

				jwt.SaveToken = true;
				jwt.TokenValidationParameters = tokenValidationParams;
			});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IAccountRepo, AccountRepo>();
builder.Services.AddScoped<IRefreshTokenDAO, RefreshTokenDAO>();
builder.Services.AddScoped<IRefreshTokenRepo, RefreshTokenRepo>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddTransient<ITransiantOperation, Operation>();
builder.Services.AddSingleton<ISingletonOperation, Operation>();
builder.Services.AddScoped<IScopedOperation, Operation>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();


app.MapControllers();


app.Run();
