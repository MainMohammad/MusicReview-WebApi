using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicReview.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }

        //Relations
        public int MusicId { get; set; }
        [ForeignKey("MusicId")]
        public Music Music { get; set; }
        public int ReviewerId { get; set; }
        [ForeignKey("ReviewerId")]
        public Reviewer Reviewer { get; set; }
    }
}
