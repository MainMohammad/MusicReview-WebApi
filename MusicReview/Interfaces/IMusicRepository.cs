using MusicReview.Models;

namespace MusicReview.Interfaces
{
    public interface IMusicRepository
    {
        ICollection<Music> GetMusics();
        Music GetMusic(int musicId);
        Music GetMusic(string name);
        ICollection<Artist> ArtistsOfAMusic(int musicId);
        decimal GetMusicRating(int musicId);
        bool MusicExists(int musicId);
        bool MusicExists(Music music);
        bool CreateMusic(int artistId, int genreId, Music music);
        bool UpdateMusic(Music music);
        bool DeleteMusic(Music music);
        bool AddArtistToMusic(int artistId, int musicId);
        bool AddMusicToGenre(int genreId, int musicId);
        bool IsArtistInMusic(int artistId, int musicId);
        bool IsMusicInGenre(int genreId, int musicId);
        bool Save();
    }
}
