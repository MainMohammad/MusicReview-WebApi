using System.ComponentModel.DataAnnotations;

namespace MusicReview.Models
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        //Relations
        public ICollection<MusicGenre> MusicGenres { get; set; }
    }
}
