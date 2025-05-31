using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MusicCatalog.API.DTOs;
using MusicCatalog.API.Entities;
using MusicCatalog.API.Repositories;

namespace MusicCatalog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrackController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TrackController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: api/track
        /// <summary>
        /// Retrieves all tracks.
        /// </summary>
        /// <returns>A list of tracks.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrackDto>>> GetAllTracks()
        {
            var tracks = await _unitOfWork.Tracks.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<TrackDto>>(tracks));
        }


        // GET: api/track/{id}
        /// <summary>
        /// Retrieves a track by ID.
        /// </summary>
        /// <param name="id">The ID of the track to retrieve.</param>
        /// <returns>The track with the specified ID.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<TrackDto>> GetTrackById(long id)
        {
            var track = await _unitOfWork.Tracks.GetByIdAsync(id);

            if (track == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<TrackDto>(track));
        }

        // POST: api/track
        /// <summary>
        /// Creates a new track.
        /// </summary>
        /// <param name="createTrackDto">The DTO containing the track information.</param>"
        /// <returns>The created track</returns>
        [HttpPost]
        public async Task<ActionResult<TrackDto>> CreateTrack([FromBody] CreateTrackDto createTrackDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var track = _mapper.Map<Track>(createTrackDto);
            await _unitOfWork.Tracks.AddAsync(track);
            await _unitOfWork.SaveChangesAsync();

            var trackDto = _mapper.Map<TrackDto>(track);
            return CreatedAtAction(nameof(GetTrackById), new { id = track.Id }, trackDto);
        }

        // TODO: Implement an UpdateTrackDto class for updating albums. Using create DTO for now.
        // PUT: api/track/{id}
        /// <summary>
        /// Updates an existing track.
        /// </summary>
        /// <param name="id">The ID of the track to update.</param>"
        /// <returns>No content if the update was successful</returns>
        [HttpPut("id")]
        public async Task<IActionResult> UpdateTrack(long id, [FromBody] CreateTrackDto createTrackDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingTrack = await _unitOfWork.Tracks.GetByIdAsync(id);

            if (existingTrack == null)
            {
                return NotFound();
            }

            _mapper.Map(createTrackDto, existingTrack);
            _unitOfWork.Tracks.Update(existingTrack);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/track/{id}
        /// <summary>
        /// Deletes the track with the specified identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the track to delete.</param>
        /// <returns>No content if the deleteion was successful.</returns>
        [HttpDelete("id")]
        public async Task<IActionResult> DeleteTrack(long id)
        {
            var existingTrack = await _unitOfWork.Tracks.GetByIdAsync(id);

            if (existingTrack == null)
            {
                return NotFound();
            }

            _unitOfWork.Tracks.Remove(existingTrack);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }
    }
}
