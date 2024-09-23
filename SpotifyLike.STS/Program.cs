using Microsoft.Extensions.DependencyInjection;
using SpotifyLike.STS;
using SpotifyLike.STS.Data;
using SpotifyLike.STS.GrantType;
using SpotifyLike.STS.ProfileServer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<DatabaseOptions>(builder.Configuration.GetSection("ConnectionStrings"));
builder.Services.AddScoped<IIdentityRepository, IdentityRepository>();

builder.Services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryIdentityResources(IdentityServerConfiguration.GetIdentityResources())
                .AddInMemoryApiResources(IdentityServerConfiguration.GetApiResources())
                .AddInMemoryApiScopes(IdentityServerConfiguration.GetApiScopes())
                .AddInMemoryClients(IdentityServerConfiguration.GetClients())
                .AddProfileService<ProfileService>()
                .AddResourceOwnerValidator<ResourceOwnerValidation>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseIdentityServer();

app.UseAuthentication();   
app.UseAuthorization();

app.MapControllers();

app.Run();
