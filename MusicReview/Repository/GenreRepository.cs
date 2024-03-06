using MusicReview.Data;
using MusicReview.Interfaces;
using MusicReview.Models;

namespace MusicReview.Repository
{
    public class GenreRepository : IGenreRepository
    {
        private readonly DbCtx _context;
        public GenreRepository(DbCtx context)
        {
            _context = context;
        }

        public bool CreateGenre(Genre genre)
        {
            _context.Genres.Add(genre);
            return Save();
        }

        public bool DeleteGenre(Genre genre)
        {
            _context.Genres.Remove(genre);
            return Save();
        }

        public bool GenreExists(int genreId)
        {
            return _context.Genres.Any(g => g.Id == genreId);
        }

        public Genre GetGenre(int genreId)
        {
            return _context.Genres.Where(g =>  g.Id == genreId).FirstOrDefault();
        }

        public ICollection<Genre> GetGenres()
        {
            return _context.Genres.ToList();
        }

        public ICollection<Music> GetMusicsByGenre(int genreId)
        {
            return _context.MusicGenres.Where(mg => mg.GenreId == genreId).Select(m => m.Music).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateGenre(Genre genre)
        {
            _context.Genres.Update(genre);
            return Save();
        }
    }
}
