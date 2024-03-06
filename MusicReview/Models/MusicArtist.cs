namespace MusicReview.Models
{
    public class MusicArtist
    {
        public int MusicId { get; set; }
        public Music Music { get; set; }

        public int ArtistId { get; set; }
        public Artist Artist { get; set; }
    }
}
