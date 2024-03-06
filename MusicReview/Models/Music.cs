using System.ComponentModel.DataAnnotations;

namespace MusicReview.Models
{
    public class Music
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }

        //Relations
        public ICollection<Review> Reviews { get; set; }
        public ICollection<MusicArtist> MusicArtists { get; set; }
        public ICollection<MusicGenre> MusicGenres { get; set; }
    }
}
