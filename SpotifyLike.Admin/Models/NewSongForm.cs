namespace SpotifyLike.Admin.Models
{
    public class NewSongForm
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public int Duracao { get; set; }
        public Guid AlbumId { get; set; }
        //public string ArtistsIds { get; set; }
    }
}
