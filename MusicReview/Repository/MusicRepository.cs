using MusicReview.Data;
using MusicReview.Dto;
using MusicReview.Interfaces;
using MusicReview.Models;

namespace MusicReview.Repository
{
    public class MusicRepository : IMusicRepository
    {
        private readonly DbCtx _context;
        public MusicRepository(DbCtx context)
        {
            _context = context;
        }

        public ICollection<Artist> ArtistsOfAMusic(int musicId)
        {
            return _context.MusicArtists.Where(m => m.MusicId == musicId).Select(a => a.Artist).ToList();
        }

        public bool CreateMusic(int artistId, int genreId, Music music)
        {
            var artist = _context.Artists.Where(a => a.Id == artistId).FirstOrDefault();
            var genre = _context.Genres.Where(g => g.Id == genreId).FirstOrDefault();

            var musicArtist = new MusicArtist()
            {
                Artist = artist,
                Music = music
            };

            _context.MusicArtists.Add(musicArtist);

            var musicGenre = new MusicGenre() 
            {
                Music = music,
                Genre = genre
            };

            _context.MusicGenres.Add(musicGenre);

            _context.Musics.Add(music);

            return Save();
        }

        public bool DeleteMusic(Music music)
        {
            _context.Musics.Remove(music);
            return Save();
        }

        public Music GetMusic(int musicId)
        {
            return _context.Musics.Where(m => m.Id == musicId).FirstOrDefault();
        }

        public Music GetMusic(string name)
        {
            return _context.Musics.Where(m => m.Name == name).FirstOrDefault();
        }

        public ICollection<Music> GetMusics()
        {
            return _context.Musics.ToList();
        }

        public decimal GetMusicRating(int musicId)
        {
            var reviews = _context.Reviews.Where(m => m.MusicId == musicId);
            if (reviews.Count() <= 0)
                return 0;

            return ((decimal)reviews.Sum(r => r.Rating) / reviews.Count());
        }

        public bool MusicExists(int musicId)
        {
            return _context.Musics.Any(m => m.Id == musicId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false; 
        }

        public bool UpdateMusic(Music music)
        {
            _context.Musics.Update(music);
            return Save();
        }

        public bool IsArtistInMusic(int artistId, int musicId)
        {
            var result = _context.MusicArtists.Where(a => a.ArtistId == artistId).Select(a => a.Music)
                .Where(m => m.Id == musicId)
                .FirstOrDefault();
            if (result == null)
                return false;
            else
                return true;
        }

        public bool MusicExists(Music music)
        {
            return _context.Musics.Any(m => m.Name.Trim().ToUpper() == music.Name.TrimEnd().ToUpper());
        }

        public bool AddArtistToMusic(int artistId, int musicId)
        {
            var artist = _context.Artists.Where(a => a.Id == artistId).FirstOrDefault();
            var music = _context.Musics.Where(m => m.Id == musicId).FirstOrDefault();
            var musicArtist = new MusicArtist()
            {
                Artist = artist,
                Music = music
            };

            _context.MusicArtists.Add(musicArtist);

            return Save();
        }

        public bool AddMusicToGenre(int genreId, int musicId)
        {
            var genre = _context.Genres.Where(g => g.Id == genreId).FirstOrDefault();
            var music = _context.Musics.Where(m => m.Id == musicId).FirstOrDefault();
            var musicGenre = new MusicGenre()
            {
                Genre = genre,
                Music = music
            };

            _context.MusicGenres.Add(musicGenre);

            return Save();
        }

        public bool IsMusicInGenre(int genreId, int musicId)
        {
            var result = _context.MusicGenres.Where(a => a.GenreId == genreId).Select(a => a.Music)
                .Where(m => m.Id == musicId)
                .FirstOrDefault();
            if (result == null)
                return false;
            else
                return true;
        }
    }
}
