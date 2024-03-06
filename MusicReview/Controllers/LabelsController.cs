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
    public class LabelsController : Controller
    {
        private readonly ILabelRepository _labelRepository;
        private readonly IMapper _mapper;
        public LabelsController(ILabelRepository labelRepository, IMapper mapper)
        {
            _labelRepository = labelRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<Label>))]
        public IActionResult GetLabels()
        {
            var result = _mapper.Map<List<LabelDto>>(_labelRepository.GetLabels());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(result);
        }

        [HttpGet("{labelId}")]
        [ProducesResponseType(200, Type = typeof(Label))]
        [ProducesResponseType(400)]
        public IActionResult GetLabel(int labelId)
        {
            if (!_labelRepository.LabelExists(labelId))
                return BadRequest(ModelState);
            var result = _mapper.Map<LabelDto>(_labelRepository.GetLabel(labelId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(result);
        }

        [HttpGet("{labelId}/Artists")]
        [ProducesResponseType(200, Type = typeof(ICollection<Artist>))]
        [ProducesResponseType(400)]
        public IActionResult GetArtistsOfALabel(int labelId)
        {
            if (!_labelRepository.LabelExists(labelId))
                return BadRequest(ModelState);
            var result = _mapper.Map<List<ArtistDto>>(_labelRepository.GetArtistsOfALabel(labelId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateLabel([FromBody] LabelDto newLabel)
        {
            if (newLabel == null)
                return BadRequest(ModelState);

            if (_labelRepository.LabelExists(newLabel.Id))
            {
                ModelState.AddModelError("", "Label already exists!");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var labelMap = _mapper.Map<Label>(newLabel);
            if (!_labelRepository.CreateLabel(labelMap))
            {
                ModelState.AddModelError("", "Oops! sth went wrong creating the label...");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created.");
        }

        [HttpPut("{labelId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult EditLabel(int labelId, [FromBody] LabelDto editLabel)
        {
            if (editLabel == null)
                return BadRequest(ModelState);

            if (labelId != editLabel.Id)
                return BadRequest(ModelState);

            if (!_labelRepository.LabelExists(labelId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var labelMap = _mapper.Map<Label>(editLabel);
            if (!_labelRepository.UpdateLabel(labelMap))
            {
                ModelState.AddModelError("", "Oops! sth went wrong editing the label...");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{labelId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult DeleteLabel(int labelId)
        {
            if (!_labelRepository.LabelExists(labelId))
                return NotFound();

            var result = _labelRepository.GetLabel(labelId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_labelRepository.DeleteLabel(result))
            {
                ModelState.AddModelError("", "Oops! sth went wrong deleting the label...");
            }

            return NoContent();
        }
    }
}
