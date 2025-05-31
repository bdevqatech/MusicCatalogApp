using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MusicCatalog.API.DTOs;
using MusicCatalog.API.Entities;
using MusicCatalog.API.Repositories;

namespace MusicCatalog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArtistController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ArtistController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: api/artist
        /// <summary>
        /// Retrieves all artists.
        /// </summary>
        /// <returns>A list of artists.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArtistDto>>> GetAllArtists()
        {
            var artists = await _unitOfWork.Artists.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<ArtistDto>>(artists));
        }

        // GET: api/artist/{id}
        /// <summary>
        /// Retrieves an artist by ID.
        /// </summary>
        /// <param name="id">The ID of the artist to retrieve.</param>
        /// <returns>The artist with the specified ID.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ArtistDto>> GetArtistById(long id)
        {
            var artist = await _unitOfWork.Artists.GetByIdAsync(id);

            if (artist == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ArtistDto>(artist));
        }

        // POST: api/artist
        /// <summary>
        /// Creates a new artist.
        /// </summary>
        /// <param name="createArtistDto">The DTO containing the artist information.</param>
        /// <returns>The created artist.</returns>
        [HttpPost]
        public async Task<ActionResult<ArtistDto>> CreateArtist([FromBody] CreateArtistDto createArtistDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var artist = _mapper.Map<Artist>(createArtistDto);
            await _unitOfWork.Artists.AddAsync(artist);
            await _unitOfWork.SaveChangesAsync();

            var artistDto = _mapper.Map<ArtistDto>(artist);
            return CreatedAtAction(nameof(GetArtistById), new { id = artistDto.Id }, artistDto);
        }

        // TODO: Implement an UpdateArtistDto class for updating albums. Using create DTO for now.
        /// PUT: api/artist/{id}
        /// <summary>
        /// Updates an existing artist by ID.
        /// </summary>
        /// <param name="id">The ID of the artist to update.</param>
        /// <param name="createArtistDto">The DTO containing the updated artist information.</param>
        /// <returns>No content if the update was successful.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateArtist(long id, [FromBody] CreateArtistDto createArtistDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingArtist = await _unitOfWork.Artists.GetByIdAsync(id);

            if (existingArtist == null)
            {
                return NotFound();
            }

            _mapper.Map(createArtistDto, existingArtist);
            _unitOfWork.Artists.Update(existingArtist);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/artist/{id}
        /// <summary>
        /// Deletes an artist by ID.
        /// </summary>
        /// <param name="id">The ID of the artist to delete.</param>
        /// <returns>No content if the deletion was successful.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArtist(long id)
        {
            var existingArtist = await _unitOfWork.Artists.GetByIdAsync(id);

            if (existingArtist == null)
            {
                return NotFound();
            }

            _unitOfWork.Artists.Remove(existingArtist);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }
    }
}
