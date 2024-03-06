using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicReview.Models
{
    public class Artist
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        //Relations
        public int LabelId { get; set; }
        [ForeignKey("LabelId")]
        public Label Label { get; set; }
        public ICollection<MusicArtist> MusicArtists { get; set; }
    }
}
