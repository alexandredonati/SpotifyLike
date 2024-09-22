using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpotifyLike.Admin.Models;
using SpotifyLike.Application.Streaming;
using SpotifyLike.Application.Streaming.Dto;

namespace SpotifyLike.Admin.Controllers
{
    [Authorize]
    public class AlbumController : Controller
    {
        private AlbumService _albumService { get; set; }
        private ArtistService _artistService { get; set; }

        public AlbumController(AlbumService albumService, ArtistService artistService)
        {
            _albumService = albumService;
            _artistService = artistService;
        }

        public IActionResult Index()
        {
            var albums = _albumService.GetAllAlbums();
            var result = albums.Select(albumDto => new AlbumViewModel
            {
                Id = albumDto.Id,
                Artists = albumDto.ArtistIds.Select(id =>
                {
                    return _artistService.GetArtistById(id);
                }),
                Name = albumDto.Nome,
                Songs = albumDto.Musicas
            });
            return View(result);
        }

        public IActionResult AdicionarAlbum()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SalvarAlbum(NewAlbumForm dto)
        {
            if (ModelState.IsValid == false)
                return View("AdicionarAlbum");

            var albumDto = new AlbumDto
            {
                Nome = dto.Name,
                ArtistIds = dto.ArtistsIds.Split(",").Select(x => Guid.Parse(x))
            };

            this._albumService.CreateAlbum(albumDto);

            return RedirectToAction("Index");
        }

        public IActionResult Details()
        {
            var obj = RouteData.Values["id"];
            var albumId = Guid.Parse(obj.ToString());
            if (albumId == null || albumId == Guid.Empty)
                return RedirectToAction("Index");

            var album = this._albumService.GetAlbumById(albumId);
            var result = new AlbumViewModel
            {
                Id = album.Id,
                Artists = album.ArtistIds.Select(id =>
                {
                    return _artistService.GetArtistById(id);
                }),
                Name = album.Nome,
                Songs = album.Musicas
            };
            return View(result);
        }

        public IActionResult AdicionarMusica()
        {
            var obj = RouteData.Values["id"];
            var albumId = Guid.Parse(obj.ToString());

            var album = _albumService.GetAlbumById(albumId);

            var result = new NewSongForm
            {
                AlbumId = albumId,
                //ArtistsIds = String.Join(",",album.ArtistIds)
            };
            return View(result);
        }

        [HttpPost]
        public IActionResult SalvarMusicasEmAlbum(NewSongForm form)
        {
            if (ModelState.IsValid == false)
                return View("AdicionarMusica");

            var dto = new SongDto
            {
                Titulo = form.Titulo,
                Duracao = form.Duracao,
                AlbumId = form.AlbumId
            };

            this._albumService.AddSongToAlbum(dto);

            return RedirectToAction("Details", new { id = form.AlbumId });
        }
        public IActionResult ConfirmarExcluirAlbum()
        {
            var obj = RouteData.Values["id"];
            var albumId = Guid.Parse(obj.ToString());

            var result = _albumService.GetAlbumById(albumId);
            return View(result);
        }

        [HttpPost]
        public IActionResult Excluir(AlbumDto dto)
        {
            var id = dto.Id;
            this._albumService.DeleteAlbum(id);
            return RedirectToAction("Index");
        }
    }
}

