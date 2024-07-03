using Microsoft.AspNetCore.Mvc;
using SpotifyLike.Application.Admin;
using SpotifyLike.Application.Admin.Dto;

namespace SpotifyLike.Admin.Controllers
{
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

        public IActionResult Criar()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Salvar(UsuarioAdminDto dto)
        {
            return RedirectToAction("Index");
        }
    }
}
