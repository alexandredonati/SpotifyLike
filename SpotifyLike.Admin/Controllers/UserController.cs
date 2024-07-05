using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpotifyLike.Application.Admin;
using SpotifyLike.Application.Admin.Dto;

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
            _usuarioAdminService.Salvar(dto);
            return RedirectToAction("Index");
        }
    }
}
