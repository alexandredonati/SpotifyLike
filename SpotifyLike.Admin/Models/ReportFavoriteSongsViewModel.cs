namespace SpotifyLike.Admin.Models
{
    public class ReportFavoriteSongsViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Artists { get; set; }
        public int LikesCount { get; set; }
    }
}
