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
    public class GenresController : Controller
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;
        public GenresController(IGenreRepository genreRepository, IMapper mapper)
        {
            _genreRepository = genreRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<Genre>))]
        public IActionResult GetGenres()
        {
            var result = _mapper.Map<List<GenreDto>>(_genreRepository.GetGenres());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(result);
        }

        [HttpGet("{genreId}")]
        [ProducesResponseType(200, Type = typeof(Genre))]
        [ProducesResponseType(400)]
        public IActionResult GetGenre(int genreId)
        {
            if (!_genreRepository.GenreExists(genreId))
                return BadRequest(ModelState);
            var result = _mapper.Map<GenreDto>(_genreRepository.GetGenre(genreId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(result);
        }

        [HttpGet("{genreId}/Musics")]
        [ProducesResponseType(200, Type = typeof(ICollection<Music>))]
        [ProducesResponseType(400)]
        public IActionResult GetSongsOfAGenre(int genreId)
        {
            if (!_genreRepository.GenreExists(genreId))
                return BadRequest(ModelState);
            var result = _mapper.Map<List<MusicDto>>(_genreRepository.GetMusicsByGenre(genreId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateGenre([FromBody] GenreDto newGenre)
        {
            if (newGenre == null)
                return BadRequest(ModelState);

            if (_genreRepository.GenreExists(newGenre.Id))
            {
                ModelState.AddModelError("", "Genre already exists!");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var genreMap = _mapper.Map<Genre>(newGenre);
            if (!_genreRepository.CreateGenre(genreMap))
            {
                ModelState.AddModelError("", "Oops! sth went wrong creating the genre...");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created.");
        }

        [HttpPut("{genreId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult EditGenre(int genreId, [FromBody] GenreDto editGenre)
        {
            if (editGenre == null)
                return BadRequest(ModelState);

            if (genreId != editGenre.Id)
                return BadRequest(ModelState);

            if (!_genreRepository.GenreExists(genreId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var genreMap = _mapper.Map<Genre>(editGenre);
            if (!_genreRepository.UpdateGenre(genreMap))
            {
                ModelState.AddModelError("", "Oops! sth went wrong editing the genre...");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{genreId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult DeleteGenre(int genreId)
        {
            if (!_genreRepository.GenreExists(genreId))
                return NotFound();

            var result = _genreRepository.GetGenre(genreId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_genreRepository.DeleteGenre(result))
            {
                ModelState.AddModelError("", "Oops! sth went wrong deleting the genre...");
            }

            return NoContent();
        }
    }
}
