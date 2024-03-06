using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MusicReview.Dto;
using MusicReview.Interfaces;
using MusicReview.Models;
using System.ComponentModel.DataAnnotations;

namespace MusicReview.Controllers
{
    [Route("Api/[Controller]")]
    [ApiController]
    public class ArtistsController : Controller
    {
        private readonly IArtistRepository _artistRepository;
        private readonly IMapper _mapper;
        public ArtistsController(IArtistRepository artistRepository, IMapper mapper)
        {
            _artistRepository = artistRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<Artist>))]
        public IActionResult GetArtists() 
        {
            var result = _mapper.Map<List<ArtistDto>>(_artistRepository.GetArtists());
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(result);
        }

        [HttpGet("{artistId}")]
        [ProducesResponseType(200, Type = typeof(Artist))]
        [ProducesResponseType(400)]
        public IActionResult GetArtist(int artistId)
        {
            if (!_artistRepository.ArtistExists(artistId))
                return BadRequest(ModelState);
            var result = _mapper.Map<ArtistDto>(_artistRepository.GetArtist(artistId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(result);
        }

        [HttpGet("{artistId}/Musics")]
        [ProducesResponseType(200, Type = typeof(ICollection<Music>))]
        [ProducesResponseType(400)]
        public IActionResult GetSongsOfAnArtist(int artistId) 
        {
            if (!_artistRepository.ArtistExists(artistId))
                return BadRequest(ModelState);
            var result = _mapper.Map<List<MusicDto>>(_artistRepository.MusicsOfAnArtist(artistId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateArtist([Required]int labelId, [FromBody]ArtistDto newArtist)
        {
            if(newArtist == null)
                return BadRequest(ModelState);

            if (_artistRepository.ArtistExists(newArtist.Id))
            {
                ModelState.AddModelError("", "Artist already exists!");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var artistMap = _mapper.Map<Artist>(newArtist);
            artistMap.LabelId = labelId;
            if (!_artistRepository.CreateArtist(artistMap))
            {
                ModelState.AddModelError("", "Oops! sth went wrong creating the artist...");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created.");
        }

        [HttpPut("{artistId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult EditArtist([Required]int labelId, int artistId, [FromBody] ArtistDto editArtist)
        {
            if (editArtist == null)
                return BadRequest(ModelState);

            if (artistId != editArtist.Id)
                return BadRequest(ModelState);

            if (!_artistRepository.ArtistExists(artistId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var artistMap = _mapper.Map<Artist>(editArtist);
            artistMap.LabelId = labelId;
            if (!_artistRepository.UpdateArtist(artistMap))
            {
                ModelState.AddModelError("", "Oops! sth went wrong editing the artist...");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{artistId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult DeleteArtist(int artistId)
        {
            if(!_artistRepository.ArtistExists(artistId))
                return NotFound();

            var result = _artistRepository.GetArtist(artistId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if(!_artistRepository.DeleteArtist(result))
            {
                ModelState.AddModelError("", "Oops! sth went wrong deleting the artist...");
            }

            return NoContent();
        }
    }
}
