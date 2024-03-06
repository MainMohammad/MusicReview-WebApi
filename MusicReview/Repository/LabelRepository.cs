using Microsoft.EntityFrameworkCore;
using MusicReview.Data;
using MusicReview.Interfaces;
using MusicReview.Models;

namespace MusicReview.Repository
{
    public class LabelRepository : ILabelRepository
    {
        private readonly DbCtx _context;
        public LabelRepository(DbCtx context)
        {
            _context = context;
        }

        public bool CreateLabel(Label label)
        {
            _context.Labels.Add(label);
            return Save();
        }

        public bool DeleteLabel(Label label)
        {
            _context.Labels.Remove(label);
            return Save();
        }

        public ICollection<Artist> GetArtistsOfALabel(int labelId)
        {
            return _context.Artists.Where(l => l.LabelId == labelId).ToList();
        }

        public Label GetLabel(int labelId)
        {
            return _context.Labels.Where(l => l.Id == labelId).FirstOrDefault();
        }

        public Label GetLabelByArtist(int artistId)
        {
            return _context.Artists.Where(a => a.Id == artistId).Select(a => a.Label).FirstOrDefault();
        }

        public ICollection<Label> GetLabels()
        {
            return _context.Labels.ToList();
        }

        public bool LabelExists(int labelId)
        {
            return _context.Labels.Any(l => l.Id == labelId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateLabel(Label label)
        {
            _context.Labels.Update(label);
            return Save();
        }
    }
}
