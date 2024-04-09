using Microsoft.AspNetCore.Mvc;
using SpotifyLike.Application.Streaming;

namespace SpotifyLike.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private SongService _songService;
        public SongsController(SongService songService)
        {
            _songService = songService;
        }

        /// <summary>
        /// Obtém uma coleção com todas as músicas registradas na aplicação.
        /// </summary>
        [HttpGet]
        public IActionResult GetAllSongs()
        {
            var songs = this._songService.GetAllSongs();
            return Ok(songs);
        }

        /// <summary>
        /// Obtém uma coleção de músicas cujos títulos contém a string informada.
        /// </summary>
        [HttpGet("Search/{searchText}")]
        public IActionResult GetSongsStartsWith(string searchText)
        {
            var songs = this._songService.SearchSongs(searchText);
            return Ok(songs);
        }
    }
}
