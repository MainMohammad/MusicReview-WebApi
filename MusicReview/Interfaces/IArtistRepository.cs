using MusicReview.Models;

namespace MusicReview.Interfaces
{
    public interface IArtistRepository
    {
        ICollection<Artist> GetArtists();
        Artist GetArtist(int artistId);
        ICollection<Music> MusicsOfAnArtist(int artistId);
        bool ArtistExists(int artistId);
        bool CreateArtist(Artist artist);
        bool UpdateArtist(Artist artist);
        bool DeleteArtist(Artist artist);
        bool Save();
    }
}
