using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpotifyLike.Application.Streaming;
using SpotifyLike.Application.Streaming.Dto;
using SpotifyLike.Domain.Streaming.Aggregates;
using SpotifyLike.Repository;

namespace SpotifyLike.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistaController : ControllerBase
    {
        private ArtistService _artistService { get; set; }

        public ArtistaController(ArtistService artistService)
        {
            _artistService = artistService;
        }

        [HttpGet]
        public IActionResult GetArtistas()
        {
            var result = this._artistService.Obter();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetArtistas(Guid id)
        {
            var result = this._artistService.Obter(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Criar([FromBody] ArtistDto dto)
        {
            if (ModelState is { IsValid: false })
                return BadRequest();

            var result = this._artistService.Criar(dto);

            return Created($"/banda/{result.Id}", result);

        }

        [HttpPost]
        public IActionResult AssociarAlbum(AlbumDto dto)
        {
            if (ModelState is { IsValid: false })
                return BadRequest();

            var result = this._artistService.AssociarAlbum(dto);

            return Created($"/banda/{result.ArtistId}/albums/{result.Id}", result);
        }

        [HttpGet("{idArtista}/albums/{id}")]
        public IActionResult GetAlbums(Guid idArtista, Guid id) 
        {
            var result = this._artistService.ObterAlbum(idArtista, id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}
