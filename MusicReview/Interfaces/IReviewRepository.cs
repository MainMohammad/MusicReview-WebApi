using MusicReview.Models;

namespace MusicReview.Interfaces
{
    public interface IReviewRepository
    {
        public ICollection<Review> GetReviews();
        public Review GetReview(int reviewId);
        public ICollection<Review> GetReviewsOfASong(int musicId);
        bool ReviewExists(int reviewId);
        bool CreateReview(Review review);
        bool UpdateReview(Review review);
        bool DeleteReview(Review review);
        bool DeleteReviews(List<Review> reviews);
        bool Save();
    }
}
