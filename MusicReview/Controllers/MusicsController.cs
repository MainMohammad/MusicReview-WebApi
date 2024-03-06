using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MusicReview.Dto;
using MusicReview.Interfaces;
using MusicReview.Models;
using MusicReview.Repository;

namespace MusicReview.Controllers
{
    [Route("Api/[Controller]")]
    [ApiController]
    public class MusicsController : Controller
    {
        private readonly IMusicRepository _musicRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;
        public MusicsController(IMusicRepository musicRepository, IMapper mapper, IReviewRepository reviewRepository)
        {
            _musicRepository = musicRepository;
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<Music>))]
        public IActionResult GetMusics()
        {
            var result = _mapper.Map<List<MusicDto>>(_musicRepository.GetMusics());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(result);
        }

        [HttpGet("{musicId}")]
        [ProducesResponseType(200, Type = typeof(Music))]
        [ProducesResponseType(400)]
        public IActionResult GetMusic(int musicId)
        {
            if (!_musicRepository.MusicExists(musicId))
                return BadRequest(ModelState);
            var result = _mapper.Map<MusicDto>(_musicRepository.GetMusic(musicId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(result);
        }

        [HttpGet("{musicId}/Artists")]
        [ProducesResponseType(200, Type = typeof(ICollection<Artist>))]
        [ProducesResponseType(400)]
        public IActionResult GetArtistsOfAMusic(int musicId)
        {
            if (!_musicRepository.MusicExists(musicId))
                return BadRequest(ModelState);
            var result = _mapper.Map<List<ArtistDto>>(_musicRepository.ArtistsOfAMusic(musicId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(result);
        }

        [HttpGet("{musicId}/Rating")]
        [ProducesResponseType(200, Type = typeof(decimal))]
        [ProducesResponseType(400)]
        public IActionResult GetMusicRating(int musicId)
        {
            if (!_musicRepository.MusicExists(musicId))
                return NotFound();

            var rating = _musicRepository.GetMusicRating(musicId);

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(rating);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateMusic([FromQuery] int artistId, [FromQuery] int genreId, [FromBody] MusicDto musicCreate)
        {
            if (musicCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var musicMap = _mapper.Map<Music>(musicCreate);

            if (_musicRepository.MusicExists(musicMap))
            {
                ModelState.AddModelError("", "Music Already Exists!");
                return StatusCode(500, ModelState);
            }

            if (!_musicRepository.CreateMusic(artistId, genreId, musicMap))
            {
                ModelState.AddModelError("", "Sth went wrong while saving...");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPost]
        [Route("{musicId}/Adding to genre")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult AddMusicToGenre([FromQuery] int genreId, [FromQuery] int musicId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (_musicRepository.IsMusicInGenre(genreId, musicId))
            {
                ModelState.AddModelError("", "Music is already in this genre!");
                return StatusCode(422, ModelState);
            }

            if (!_musicRepository.AddMusicToGenre(genreId, musicId))
            {
                ModelState.AddModelError("", "Sth went wrong while saving...");
                return StatusCode(500, ModelState);
            }

            return Ok("Music Successfully added to genre!");
        }

        [HttpPost]
        [Route("{musicId}/Adding Artist to song")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult AddArtistToMusic(int musicId, [FromQuery] int artistId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (_musicRepository.IsArtistInMusic(artistId, musicId))
            {
                ModelState.AddModelError("", "Artist is already in music!");
                return StatusCode(422, ModelState);
            }

            if (!_musicRepository.AddArtistToMusic(artistId, musicId))
            {
                ModelState.AddModelError("", "Sth went wrong while saving...");
                return StatusCode(500, ModelState);
            }

            return Ok("Artist Successfully added to music!");
        }

        [HttpPut("{musicId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateMusic(int musicId, [FromBody] MusicDto musicUpdate)
        {
            if (musicUpdate == null)
                return BadRequest(ModelState);

            if (musicId != musicUpdate.Id)
                return BadRequest(ModelState);

            if (!_musicRepository.MusicExists(musicId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var musicMap = _mapper.Map<Music>(musicUpdate);

            if (!_musicRepository.UpdateMusic(musicMap))
            {
                ModelState.AddModelError("", "Something went wrong updating artist");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{musicId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteMusic(int musicId)
        {
            if (!_musicRepository.MusicExists(musicId))
            {
                return NotFound();
            }

            var reviewsToDelete = _reviewRepository.GetReviewsOfASong(musicId);
            var musicToDelete = _musicRepository.GetMusic(musicId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_reviewRepository.DeleteReviews(reviewsToDelete.ToList()))
            {
                ModelState.AddModelError("", "Something went wrong when deleting reviews");
            }

            if (!_musicRepository.DeleteMusic(musicToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting artist");
            }

            return NoContent();
        }
    }
}