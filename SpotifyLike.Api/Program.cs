using Microsoft.EntityFrameworkCore;
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
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<SpotifyLikeContext>(c =>
{
    c.UseSqlServer(builder.Configuration.GetConnectionString("SpotifyConnection"));
});

builder.Services.AddAutoMapper(typeof(UsuarioProfile).Assembly);

//Repositories
builder.Services.AddScoped<UsuarioRepository>();
builder.Services.AddScoped<PlanoRepository>();
builder.Services.AddScoped<ArtistRepository>();
builder.Services.AddScoped<AlbumRepository>();

//Services
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<ArtistService>();
builder.Services.AddScoped<AlbumService>();

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
