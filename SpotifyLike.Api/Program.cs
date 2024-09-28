using IdentityServer4.AccessTokenValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SpotifyLike.Application.Conta;
using SpotifyLike.Application.Conta.Profile;
using SpotifyLike.Application.Streaming;
using SpotifyLike.Repository;
using SpotifyLike.Repository.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme()
    {
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Description = "Bearer {seu token de acesso}",
        Scheme = "Bearer"
    });
    
    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement()
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme()
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference()
                {
                    Id = "Bearer",
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme
                }
            },
            Array.Empty<string>()
        }
    });
}
    
    );

builder.Services.AddDbContext<SpotifyLikeContext>(c =>
{
    c
    .UseLazyLoadingProxies()
    .UseSqlServer(builder.Configuration.GetConnectionString("SpotifyConnection"));
});

builder.Services.AddAutoMapper(typeof(UsuarioProfile).Assembly);

builder.Services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = "https://localhost:7084";
                    options.ApiName = "SpotifyLike-api";
                    options.ApiSecret = "SpotifyLikeSecret";
                    options.RequireHttpsMetadata = true;
                });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("spotifylike-role-user", policy =>
    {
        policy.RequireClaim("role", "spotifylike-user");
    });
});

//Repositories
builder.Services.AddScoped<UsuarioRepository>();
builder.Services.AddScoped<PlanoRepository>();
builder.Services.AddScoped<AssinaturaRepository>();
builder.Services.AddScoped<PlaylistRepository>();
builder.Services.AddScoped<ArtistRepository>();
builder.Services.AddScoped<AlbumRepository>();
builder.Services.AddScoped<TransacaoRepository>();
builder.Services.AddScoped<SongRepository>();
//Services
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<ArtistService>();
builder.Services.AddScoped<AlbumService>();
builder.Services.AddScoped<PlanosService>();
builder.Services.AddScoped<SongService>();
builder.Services.AddScoped<AzureStorageAccount>();
builder.Services.AddScoped<AzureServiceBusService>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "SpotifyLike API",
            Version = "v1"
        }
     );

    var filePath = Path.Combine(System.AppContext.BaseDirectory, "SpotifyLike.Api.xml");
    c.IncludeXmlComments(filePath);
});
builder.Services.AddApplicationInsightsTelemetry();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
