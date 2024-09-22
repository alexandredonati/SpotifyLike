using Microsoft.AspNetCore.Mvc;
using SpotifyLike.Application.Conta;
using SpotifyLike.Application.Streaming;

namespace SpotifyLike.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanosController : ControllerBase
    {
        private PlanosService _planoService { get; set; }
        public PlanosController(PlanosService planoService)
        {
            _planoService = planoService;
        }


        /// <summary>
        /// Obtém uma coleção com todos os planos disponíveis.
        /// </summary>
        [HttpGet]
        public IActionResult GetPlanos()
        {
            var result = this._planoService.GetPlanos();

            return Ok(result);
        }

        /// <summary>
        /// Obtém um plano pelo seu ID.
        /// </summary>
        [HttpGet("{id}")]
        public IActionResult GetPlanoById(Guid id)
        {
            var result = this._planoService.GetPlanoById(id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
