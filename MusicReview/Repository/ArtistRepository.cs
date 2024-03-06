using Microsoft.EntityFrameworkCore;
using MusicReview.Data;
using MusicReview.Interfaces;
using MusicReview.Models;

namespace MusicReview.Repository
{
    public class ArtistRepository : IArtistRepository
    {
        private readonly DbCtx _context;
        public ArtistRepository(DbCtx context)
        {
            _context = context;
        }

        public bool ArtistExists(int artistId)
        {
            return _context.Artists.Any(a => a.Id == artistId);
        }

        public bool CreateArtist(Artist artist)
        {
            _context.Artists.Add(artist);
            return Save();
        }

        public bool DeleteArtist(Artist artist)
        {
            _context.Artists.Remove(artist);
            return Save();
        }

        public Artist GetArtist(int artistId)
        {
            return _context.Artists.Where(a => a.Id == artistId).FirstOrDefault();
        }

        public ICollection<Artist> GetArtists()
        {
            return _context.Artists.ToList();
        }

        public ICollection<Music> MusicsOfAnArtist(int artistId)
        {
            return _context.MusicArtists.Where(a => a.ArtistId == artistId).Select(m => m.Music).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateArtist(Artist artist)
        {
            _context.Artists.Update(artist);
            return Save();
        }
    }
}
