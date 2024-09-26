using Microsoft.AspNetCore.Mvc;
using SpotifyLike.Application.Streaming;
using SpotifyLike.Application.Streaming.Dto;
using SpotifyLike.Domain;

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

        /// <summary>
        /// Obtém uma coleção com todos os artistas.
        /// </summary>
        [HttpGet]
        public IActionResult GetArtists()
        {
            var result = this._artistService.GetArtists();

            return Ok(result);
        }

        /// <summary>
        /// Obtém um artista pelo seu ID.
        /// </summary>
        [HttpGet("{idArtist}")]
        public IActionResult GetArtistById(Guid idArtist)
        {
            var result = this._artistService.GetArtistById(idArtist);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        /// <summary>
        /// Adiciona um novo artista.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> NewArtist([FromBody] ArtistDto dto)
        {
            if (ModelState is { IsValid: false })
                return BadRequest();
            try
            {
                var result = await this._artistService.CreateArtist(dto);
                return Created($"/artists/{result.Id}", result);
            }
            catch (BusinessRuleException ex) 
            {
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// Obtém uma coleção com todos os albums de um artista.
        /// </summary>
        [HttpGet("{idArtist}/Albums")]
        public IActionResult GetArtistAlbums(Guid idArtist) 
        {
            try
            {
                var result = this._artistService.GetArtistAlbums(idArtist);

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
        /// Obtém uma coleção com todas as músicas de um artista.
        /// </summary>
        [HttpGet("{idArtist}/Songs")]
        public IActionResult GetArtistSongs(Guid idArtist)
        {
            var result = this._artistService.GetArtistSongs(idArtist);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        /// <summary>
        /// Obtém uma coleção de artistas cujos nomes contém a string informada.
        /// </summary>
        [HttpGet("Search/{searchText}")]
        public IActionResult GetArtistsStartsWith(string searchText)
        {
            var result = this._artistService.SearchArtists(searchText);

            return Ok(result);
        }
    }
}
