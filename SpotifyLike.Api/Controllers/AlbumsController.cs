using Microsoft.AspNetCore.Mvc;
using SpotifyLike.Application.Streaming;
using SpotifyLike.Application.Streaming.Dto;
using SpotifyLike.Domain;

namespace SpotifyLike.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumsController : ControllerBase
    {
        private AlbumService _albumService {  get; set; }
        public AlbumsController(AlbumService albumService)
        {
            _albumService = albumService;
        }

        /// <summary>
        /// Obtém um álbum pelo seu ID.
        /// </summary>
        [HttpGet("{idAlbum}")]
        public IActionResult GetAlbumById(Guid idAlbum)
        {
            try
            {
                var result = this._albumService.GetAlbumById(idAlbum);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (BusinessRuleException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Adiciona um novo álbum.
        /// </summary>
        [HttpPost]
        public IActionResult NewAlbum([FromBody] AlbumDto dto)
        {
            if (ModelState is { IsValid: false })
                return BadRequest();
            try
            {
                var result = this._albumService.CreateAlbum(dto);
                return Created($"/albums/{result.Id}", result);
            }
            catch (BusinessRuleException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Adiciona novas músicas a um álbum existente.
        /// </summary>
        [HttpPost("Songs")]
        public IActionResult AddSongs([FromBody] AlbumDto dto)
        {
            if (ModelState is { IsValid: false })
                return BadRequest();
            try
            {
                var result = this._albumService.AddSongsToAlbum(dto.Id, dto.Musicas);
                return Created($"/albums/{result.Id}", result);
            }
            catch (BusinessRuleException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
