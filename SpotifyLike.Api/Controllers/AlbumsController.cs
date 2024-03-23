using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpotifyLike.Application.Streaming;
using SpotifyLike.Application.Streaming.Dto;

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

        [HttpGet("{id}")]
        public IActionResult GetAlbumById(Guid id)
        {
            var result = this._albumService.GetAlbum(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public IActionResult NewAlbum([FromBody] AlbumDto dto)
        {
            if (ModelState is { IsValid: false })
                return BadRequest();

            var result = this._albumService.CreateAlbum(dto);

            return Created($"/albums/{result.Id}", result);
        }

    }
}
