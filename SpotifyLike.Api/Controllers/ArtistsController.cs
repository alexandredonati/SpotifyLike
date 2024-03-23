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
    public class ArtistsController : ControllerBase
    {
        private ArtistService _artistService { get; set; }

        public ArtistsController(ArtistService artistService)
        {
            _artistService = artistService;
        }

        [HttpGet]
        public IActionResult GetArtists()
        {
            var result = this._artistService.GetArtists();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetArtistById(Guid id)
        {
            var result = this._artistService.GetArtistById(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public IActionResult NewArtist([FromBody] ArtistDto dto)
        {
            if (ModelState is { IsValid: false })
                return BadRequest();

            var result = this._artistService.CreateArtist(dto);

            return Created($"/artists/{result.Id}", result);

        }

        [HttpGet("{idArtista}/albums")]
        public IActionResult GetArtistAlbums(Guid idArtista) 
        {
            var result = this._artistService.GetArtistAlbums(idArtista);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}
