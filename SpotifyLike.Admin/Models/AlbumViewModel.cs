using SpotifyLike.Application.Streaming.Dto;

namespace SpotifyLike.Admin.Models
{
    public class AlbumViewModel
    {
        public Guid Id { get; set; }
        public IEnumerable<ArtistDto> Artists { get; set; }
        public string Name { get; set; }
        public IList<SongDto> Songs { get; set; }

    }
}
