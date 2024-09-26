using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using SpotifyLike.Application.Admin;
using SpotifyLike.Application.Admin.Profile;
using SpotifyLike.Application.Conta;
using SpotifyLike.Application.Streaming;
using SpotifyLike.Repository;
using SpotifyLike.Repository.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<SpotifyLikeAdminContext>(c =>
{
    c
    .UseLazyLoadingProxies()
    .UseSqlServer(builder.Configuration.GetConnectionString("SpotifyAdminConnection"));
});
builder.Services.AddDbContext<SpotifyLikeContext>(c =>
{
    c
    .UseLazyLoadingProxies()
    .UseSqlServer(builder.Configuration.GetConnectionString("SpotifyConnection"));
});

builder.Services.AddAutoMapper(typeof(UsuarioAdminProfile).Assembly);

builder.Services.AddScoped<UsuarioAdminRepository>();
builder.Services.AddScoped<UsuarioAdminService>();
builder.Services.AddScoped<ArtistRepository>();
builder.Services.AddScoped<ArtistService>();
builder.Services.AddScoped<AlbumRepository>();
builder.Services.AddScoped<AlbumService>();
builder.Services.AddScoped<SongRepository>();
builder.Services.AddScoped<TransacaoRepository>();
builder.Services.AddScoped<PlanoRepository>();
builder.Services.AddScoped<UsuarioRepository>();
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<PlaylistRepository>();
builder.Services.AddScoped<PlaylistService>();
builder.Services.AddScoped<AssinaturaRepository>();


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
