using MusicReview.Models;

namespace MusicReview.Interfaces
{
    public interface ILabelRepository
    {
        ICollection<Label> GetLabels();
        Label GetLabel(int labelId);
        Label GetLabelByArtist(int artistId);
        ICollection<Artist> GetArtistsOfALabel(int labelId);
        bool LabelExists(int labelId);
        bool CreateLabel(Label label);
        bool UpdateLabel(Label label);
        bool DeleteLabel(Label label);
        bool Save();
    }
}