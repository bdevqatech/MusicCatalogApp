using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MusicCatalog.API.DTOs;
using MusicCatalog.API.Entities;
using MusicCatalog.API.Repositories;

namespace MusicCatalog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenreController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GenreController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: api/genre
        /// <summary>
        /// Retrieves all genres.
        /// </summary>
        /// <returns>A list of genres.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GenreDto>>> GetAllGenres()
        {
            var genres = await _unitOfWork.Genres.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<GenreDto>>(genres));
        }

        // GET: api/genre/{id}
        /// <summary>
        /// Retrieves a genre by its unique identifier.
        /// </summary>
        /// <param name="id">The ID of the genre to retrieve.</param>
        /// <returns> The genre with the specified ID.</returns>
        [HttpGet("id")]
        public async Task<ActionResult<GenreDto>> GetGenreById(long id)
        {
            var genre = await _unitOfWork.Genres.GetByIdAsync(id);

            if (genre == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<GenreDto>(genre));
        }

        // POST: api/genre
        /// <summary>
        /// Creates a new genre.
        /// </summary>
        /// <param name="createGenreDto">The DTO containing the genre information.</param>
        /// <returns>The created genre.</returns>
        [HttpPost]
        public async Task<ActionResult<GenreDto>> CreateGenre([FromBody] CreateGenreDto createGenreDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var genre = _mapper.Map<Genre>(createGenreDto);
            await _unitOfWork.Genres.AddAsync(genre);
            await _unitOfWork.SaveChangesAsync();

            var genreDto = _mapper.Map<GenreDto>(genre);
            return CreatedAtAction(nameof(GetGenreById), new { id = genre.Id }, genreDto);
        }

        // TODO: Implement an UpdateAlbumDto class for updating albums. Using create DTO for now.
        // PUT: api/genre/{id}
        /// <summary>
        /// Updates and existing genre.
        /// </summary>
        /// <param name="id">The ID of the genre to update.</param>
        /// <param name="createGenreDto">The DTO containing the updated genre info.</param>
        /// <returns>No content if the update was successful</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGenre(long id, [FromBody] CreateGenreDto createGenreDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingGenre = await _unitOfWork.Genres.GetByIdAsync(id);

            if (existingGenre == null)
            {
                return NotFound();
            }

            _mapper.Map(createGenreDto, existingGenre);
            _unitOfWork.Genres.Update(existingGenre);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/genre/{id}
        /// <summary>
        /// Deletes the genre with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the genre to delete.</param>
        /// <returns>No content if the deletetion was successful.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenre(long id)
        {
            var existingGenre = await _unitOfWork.Genres.GetByIdAsync(id);

            if (existingGenre == null)
            {
                return NotFound();
            }

            _unitOfWork.Genres.Remove(existingGenre);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }
    }
}
