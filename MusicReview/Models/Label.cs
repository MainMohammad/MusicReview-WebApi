using System.ComponentModel.DataAnnotations;

namespace MusicReview.Models
{
    public class Label
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        //Relations
        public ICollection<Artist> Artists { get; set; }
    }
}
