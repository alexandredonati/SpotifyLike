using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpotifyLike.Application.Admin;
using SpotifyLike.Application.Admin.Dto;
using SpotifyLike.Application.Streaming.Dto;
using SpotifyLike.Application.Streaming;

namespace SpotifyLike.Admin.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private UsuarioAdminService _usuarioAdminService { get; set; }

        public UserController(UsuarioAdminService usuarioAdminService)
        {
            _usuarioAdminService = usuarioAdminService;
        }

        public IActionResult Index()
        {
            var result = _usuarioAdminService.GetAll();
            return View(result);
        }

        [AllowAnonymous]
        public IActionResult Criar()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Salvar(UsuarioAdminDto dto)
        {
            if (!ModelState.IsValid)
            {               
                return View("Criar");
            }
            var result = _usuarioAdminService.Salvar(dto);
            return RedirectToAction("Index");
        }

        public IActionResult ConfirmarExcluir()
        {
            var obj = RouteData.Values["id"];
            var artistId = Guid.Parse(obj.ToString());

            var result = _usuarioAdminService.GetById(artistId);
            return View(result);
        }

        [HttpPost]
        public IActionResult Excluir(UsuarioAdminDto dto)
        {
            var id = dto.Id;
            this._usuarioAdminService.DeleteUser(id);
            return RedirectToAction("Index");
        }
    }
}
