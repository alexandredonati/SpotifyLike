using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
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
                return Created($"Users/{result.Id}", result);
            }
            catch (BusinessRuleException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Atualiza a assinatura de um usuário existente
        /// </summary>
        [HttpPost("Subscriptions")]
        public IActionResult UpdateSubscription([FromBody] SubscriptionDto dto)
        {
            if (ModelState is { IsValid: false })
                return BadRequest();
            try
            {
                var result = this._usuarioService.UpdateSubscription(dto);
                return Created($"Users/{result.Id}", result);
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
            //var result = this._usuarioService.GetUsers();
            this._usuarioService.TestMethod();
            //if (result == null)
            //    return NotFound();

            //return Ok(result);
            return Ok();
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

        /// <summary>
        /// Obtém as musicas favoritas de um usuário pelo seu ID.
        /// </summary>
        [HttpGet("{idUser}/Favoritas")]
        public IActionResult GetUserFavoriteSongs(Guid idUser)
        {
            try
            {
                var result = this._usuarioService.GetFavoritas(idUser);

                if (result == null)
                    return NotFound();

                return Ok(result);
            }
            catch (BusinessRuleException ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// Obtém as playlists de um usuário pelo seu ID.
        /// </summary>
        [HttpGet("{idUser}/Playlists")]
        public IActionResult GetUserPlaylists(Guid idUser)
        {
            try
            {
                var result = this._usuarioService.GetPlaylists(idUser);

                if (result == null)
                    return NotFound();

                return Ok(result);
            }
            catch (BusinessRuleException ex)
            {
                return BadRequest(ex.Message);
            }
        }



        /// <summary>
        /// Autentica um usuário a partir de e-mail e senha.
        /// </summary>
        [HttpPost("Login")]
        public IActionResult Login([FromBody] Request.LoginRequest dto)
        {
            if (ModelState is { IsValid: false })
                return BadRequest();
            try
            {
                var result = this._usuarioService.Authenticate(dto.Email, dto.Senha);
                return Ok(result);
            }
            catch (BusinessRuleException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Adiciona uma nova música aos favoritos do usuário.
        /// </summary>
        [HttpPost("Favoritar")]
        public IActionResult FavoritarMusica([FromBody] FavoritarDto dto)
        {
            if (ModelState is { IsValid: false })
                return BadRequest();
            try
            {
                var result = this._usuarioService.FavoritarMusica(dto.idUser, dto.idSong);
                return Ok(result);
            }
            catch (BusinessRuleException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Remove uma música dos favoritos do usuário.
        /// </summary>
        [HttpPost("Desfavoritar")]
        public IActionResult DesfavoritarMusica([FromBody] FavoritarDto dto)
        {
            if (ModelState is { IsValid: false })
                return BadRequest();
            try
            {
                var result = this._usuarioService.DesfavoritarMusica(dto.idUser, dto.idSong);
                return Ok(result);
            }
            catch (BusinessRuleException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
