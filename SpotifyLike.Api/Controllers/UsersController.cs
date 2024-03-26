using Microsoft.AspNetCore.Mvc;
using SpotifyLike.Application.Conta;
using SpotifyLike.Application.Conta.Dto;
using SpotifyLike.Domain;

namespace SpotifyLike.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private UsuarioService _usuarioService;
        public UsersController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        /// <summary>
        /// Adiciona um novo usuário
        /// </summary>
        [HttpPost]
        public IActionResult Create([FromBody] UsuarioDto dto)
        {
            if (ModelState is { IsValid: false})
                return BadRequest();
            try
            {
                var result = this._usuarioService.Create(dto);
                return Created($"users/{result.Id}", result);
            }
            catch (BusinessRuleException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Obtém uma coleção com todos os usuários cadastrados.
        /// </summary>
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var result = this._usuarioService.GetUsers();

            if (result == null)
                return NotFound();

            return Ok(result);

        }

        /// <summary>
        /// Obtém um usuário pelo seu ID.
        /// </summary>
        [HttpGet("{idUser}")]
        public IActionResult Obter(Guid idUser)
        {
            var result = this._usuarioService.GetUserById(idUser);

            if (result == null)
                return NotFound();

            return Ok(result);

        }
    }
}
