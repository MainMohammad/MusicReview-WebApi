using System.ComponentModel.DataAnnotations;

namespace MusicReview.Models
{
    public class Reviewer
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        //Relations
        public ICollection<Review> Reviews { get; set; }
    }
}
