using Loxodonta.API.Extensions;
using Loxodonta.API.Extensions.Authentication;
using Loxodonta.Application.Common;
using Loxodonta.Application.Extensions;
using Loxodonta.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
var jwtSection = config.GetSection(nameof(JwtConfiguration));

// Add services to the container.
builder.Services.Configure<JwtConfiguration>(jwtSection);
builder.Services.AddAuthentication(AuthenticationOptionsExtensions.UseDefaults)
    .AddJwtBearerConfigured(jwtSection);

builder.Services.AddInfrastructureDbContext(config);
builder.Services.AddInfrastructureRepositories();
builder.Services.AddApplicationServices();
builder.Services.AddControllers();
builder.Services.AddAuthorization();
builder.Services.AddValidationFilters();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
