using System.ComponentModel.DataAnnotations;

namespace MusicReview.Models
{
    public class MusicGenre
    {
        [Key]
        public int MusicId { get; set; }
        public Music Music { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
    }
}
