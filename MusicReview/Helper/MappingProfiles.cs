using AutoMapper;
using MusicReview.Dto;
using MusicReview.Models;

namespace MusicReview.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Artist, ArtistDto>();
            CreateMap<Genre, GenreDto>();
            CreateMap<Label, LabelDto>();
            CreateMap<Music, MusicDto>();
            CreateMap<Review, ReviewDto>();
            CreateMap<Reviewer, ReviewerDto>();
            CreateMap<ArtistDto, Artist>();
            CreateMap<GenreDto, Genre>();
            CreateMap<LabelDto, Label>();
            CreateMap<MusicDto, Music>();
            CreateMap<ReviewDto, Review>();
            CreateMap<ReviewerDto, Reviewer>();
        }
    }
}
