using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MusicCatalog.API.DTOs;
using MusicCatalog.API.Entities;
using MusicCatalog.API.Repositories;

namespace MusicCatalog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecordLabelController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RecordLabelController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: api/recordlabel
        /// <summary>
        /// Retrieves all record labels.
        /// </summary>
        /// <returns>A list of record labels.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecordLabelDto>>> GetAllRecordLabels()
        {
            var recordLabels = await _unitOfWork.RecordLabels.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<RecordLabelDto>>(recordLabels));
        }

        // GET: api/recordlabel/{id}
        /// <summary>
        /// Retrieves a record label by ID.
        /// </summary>
        /// <param name="id">The ID of the record label to retrieve.</param>
        /// <returns>The record label with the specified ID.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<RecordLabelDto>> GetRecordLabelById(long id)
        {
            var recordLabel = await _unitOfWork.RecordLabels.GetByIdAsync(id);

            if (recordLabel == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<RecordLabelDto>(recordLabel));
        }

        // POST: api/recordlabel
        /// <summary>
        /// Creates a new record label.
        /// </summary>
        /// <param name="createRecordLabelDto">The DTO containing the album information.</param>
        /// <returns>The created record label.</returns>
        [HttpPost]
        public async Task<ActionResult<RecordLabelDto>> CreateRecordLabel([FromBody] CreateRecordLabelDto createRecordLabelDto)
        {
            if (createRecordLabelDto == null)
            {
                return BadRequest("Invalid record label data.");
            }

            var recordLabel = _mapper.Map<RecordLabel>(createRecordLabelDto);
            await _unitOfWork.RecordLabels.AddAsync(recordLabel);
            await _unitOfWork.SaveChangesAsync();

            var recordLabelDto = _mapper.Map<RecordLabelDto>(recordLabel);
            return CreatedAtAction(nameof(GetRecordLabelById), new { id = recordLabel.Id }, recordLabelDto);
        }

        // TODO: Implement an UpdateAlbumDto class for updating record labels. Using create DTO for now.
        // PUT: api/recordlabel/{id}
        /// <summary>
        /// Updates an existing record label.
        /// </summary>
        /// <param name="id">The ID of the record label to update.</param>
        /// <param name="createRecordLabelDto">The DTO containing the updated record label information.</param>
        /// <returns>No content if the update was successful.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRecordLabel(long id, [FromBody] CreateRecordLabelDto createRecordLabelDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingRecordLabel = await _unitOfWork.RecordLabels.GetByIdAsync(id);

            if (existingRecordLabel == null)
            {
                return NotFound();
            }

            _mapper.Map(createRecordLabelDto, existingRecordLabel);
            _unitOfWork.RecordLabels.Update(existingRecordLabel);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/recordlabel/{id}
        /// <summary>
        /// Deletes a record label with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the record label to delete.</param>
        /// <returns>No content if the delete was successful.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecordLabel(long id)
        {
            var existingRecordLabel = await _unitOfWork.RecordLabels.GetByIdAsync(id);

            if (existingRecordLabel == null)
            {
                return NotFound();
            }

            _unitOfWork.RecordLabels.Remove(existingRecordLabel);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }
    }
}
