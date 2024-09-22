using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpotifyLike.Application.Streaming;
using SpotifyLike.Application.Streaming.Dto;

namespace SpotifyLike.Admin.Controllers
{
    [Authorize]
    public class ArtistController : Controller
    {
        private ArtistService _artistService { get; set; }

        public ArtistController(ArtistService artistService)
        {
            _artistService = artistService;
        }

        public IActionResult Index()
        {
            var result = _artistService.GetArtists();
            return View(result);
        }

        public IActionResult Adicionar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Salvar(ArtistDto dto)
        {
            if (ModelState.IsValid == false)
                return View("Adicionar");

            this._artistService.CreateArtist(dto);

            return RedirectToAction("Index");
        }

        public IActionResult ConfirmarExcluir()
        {
            var obj = RouteData.Values["id"];
            var artistId = Guid.Parse(obj.ToString());

            var result = _artistService.GetArtistById(artistId);
            return View(result);
        }

        [HttpPost]
        public IActionResult Excluir(ArtistDto dto)
        {
            var id = dto.Id;
            this._artistService.DeleteArtist(id);
            return RedirectToAction("Index");
        }
    }
}
