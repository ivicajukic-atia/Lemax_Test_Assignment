using Lemax_Test_Assignment.Data.Interfaces;
using Lemax_Test_Assignment.Data.Repositories;
using Lemax_Test_Assignment.Middleware;
using Lemax_Test_Assignment.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();
builder.Host.UseSerilog();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure Authentication
builder.Services.AddAuthentication(options =>
{
  options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
  options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
  options.TokenValidationParameters = new TokenValidationParameters
  {
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidateLifetime = true,
    ValidateIssuerSigningKey = true,
    // Other JWT configuration here
  };
});

// Configure Authorization
builder.Services.AddAuthorization(options =>
{
  options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
});

// Determine which repository to use based on configuration
var repositoryType = builder.Configuration["RepositoryType"];

if (repositoryType == "InMemory")
{
  builder.Services.AddSingleton<IHotelRepository, InMemoryHotelRepository>();
}
else
{
  builder.Services.AddScoped<IHotelRepository, HotelRepository>();
}

builder.Services.AddScoped<IHotelService, HotelService>();

builder.Services.AddHealthChecks()
    .AddCheck("Self", () => HealthCheckResult.Healthy());
    //.AddSqlServer(builder.Configuration.GetConnectionString("LemaxSqlConnectionString"));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseMiddleware<DummyAuthMiddleware>();
app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapHealthChecks("/health");
app.MapControllers();

app.Run();
