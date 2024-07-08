using Microsoft.AspNetCore.Mvc;
using SpotifyLike.Admin.Models;
using SpotifyLike.Application.Conta;
using SpotifyLike.Application.Streaming;
using SpotifyLike.Application.Streaming.Dto;
using SpotifyLike.Domain.Streaming.Aggregates;

namespace SpotifyLike.Admin.Controllers
{
    public class FavoritesController : Controller
    {
        private UsuarioService _usuarioService { get; set; }
        private PlaylistService _playlistService { get; set; }
        private AlbumService _albumService { get; set; }
        private ArtistService _artistService { get; set; }

        public FavoritesController(UsuarioService usuarioService, PlaylistService playlistService, AlbumService albumService, ArtistService artistService)
        {
            _usuarioService = usuarioService;
            _playlistService = playlistService;
            _albumService = albumService;
            _artistService = artistService;
        }

        public IActionResult SongsReport()
        {
            var favoritePlaylistsIds = _usuarioService.GetAllFavoritePlaylistsIds();

            var favoriteSongs = new List<ReportFavoriteSongsViewModel>();

            foreach(var id in favoritePlaylistsIds)
            {
                var playlist = _playlistService.GetById(id);
                foreach(var song in playlist.Musicas)
                {
                    var favoriteSong = favoriteSongs.FirstOrDefault(s => s.Id == song.Id);

                    if (favoriteSong != null)
                    {
                        favoriteSong.LikesCount++;
                    }
                    else
                    {
                        var album = _albumService.GetAlbumById(song.AlbumId);
                        var artists = album.ArtistIds.Select(artistId => _artistService.GetArtistById(artistId));
                        favoriteSongs.Add(new ReportFavoriteSongsViewModel
                        {
                            Id = song.Id,
                            Title = song.Titulo,
                            Artists = String.Join(",", artists.Select(artist => artist.Nome)),
                            LikesCount = 1
                        });
                    }
                }
            }

            favoriteSongs = favoriteSongs.OrderByDescending(s => s.LikesCount).ToList();

            return View(favoriteSongs);
        }

        public IActionResult ArtistsReport()
        {
            var favoritePlaylistsIds = _usuarioService.GetAllFavoritePlaylistsIds();

            var favoriteArtists = new List<ReportFavoriteArtistsViewModel>();

            foreach (var id in favoritePlaylistsIds)
            {
                var playlist = _playlistService.GetById(id);
                foreach (var song in playlist.Musicas)
                {
                    var album = _albumService.GetAlbumById(song.AlbumId);

                    foreach (var artistId in album.ArtistIds)
                    {
                        var favoriteArtist = favoriteArtists.FirstOrDefault(a => a.Id == artistId);

                        if (favoriteArtist != null)
                        {
                            favoriteArtist.LikesCount++;
                        }
                        else
                        {
                            var artist = _artistService.GetArtistById(artistId);
                            favoriteArtists.Add(new ReportFavoriteArtistsViewModel
                            {
                                Id = artist.Id,
                                Name = artist.Nome,
                                LikesCount = 1
                            });
                        }
                    }
                }
            }

            return View(favoriteArtists);
        }
    }
}
