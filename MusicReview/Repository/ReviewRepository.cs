using MusicReview.Data;
using MusicReview.Interfaces;
using MusicReview.Models;

namespace MusicReview.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly DbCtx _ctx;
        public ReviewRepository(DbCtx ctx)
        {
            _ctx = ctx;
        }

        public bool CreateReview(Review review)
        {
            _ctx.Reviews.Add(review);
            return Save();
        }

        public bool DeleteReview(Review review)
        {
            _ctx.Reviews.Remove(review);
            return Save();
        }

        public bool DeleteReviews(List<Review> reviews)
        {
            _ctx.Reviews.RemoveRange(reviews);
            return Save();
        }

        public Review GetReview(int reviewId)
        {
            return _ctx.Reviews.Where(r => r.Id == reviewId).FirstOrDefault();
        }

        public ICollection<Review> GetReviews()
        {
            return _ctx.Reviews.ToList();
        }

        public ICollection<Review> GetReviewsOfASong(int musicId)
        {
            return _ctx.Reviews.Where(m => m.MusicId == musicId).ToList();
        }

        public bool ReviewExists(int reviewId)
        {
            return _ctx.Reviews.Any(r => r.Id == reviewId);
        }

        public bool Save()
        {
            var saved = _ctx.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateReview(Review review)
        {
            _ctx.Reviews.Update(review);
            return Save();
        }
    }
}
