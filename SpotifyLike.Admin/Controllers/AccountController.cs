using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using SpotifyLike.Admin.Models;
using SpotifyLike.Application.Admin;
using System.Security.Claims;

namespace SpotifyLike.Admin.Controllers
{
    public class AccountController : Controller
    {
        private UsuarioAdminService _usuarioAdminService { get; set; }

        public AccountController(UsuarioAdminService usuarioAdminService)
        {
            _usuarioAdminService = usuarioAdminService;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var usuario = _usuarioAdminService.Authenticate(request.Email, request.Senha);

            if (usuario == null)
            {
                ModelState.AddModelError("InvalidCredentials", "Email ou senha inválidos");
                return View();
            }

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaims(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.Nome),
                new Claim(ClaimTypes.Email, usuario.Email)
            });

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

            return RedirectToAction("Index", "Home");
        }
    }
}
