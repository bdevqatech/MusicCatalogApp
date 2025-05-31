using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MusicCatalog.API.DTOs;
using MusicCatalog.API.Entities;
using MusicCatalog.API.Repositories;

namespace MusicCatalog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlbumController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AlbumController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: api/album
        /// <summary>
        /// Retrieves all albums.
        /// </summary>
        /// <returns>A list of albums.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlbumDto>>> GetAllAlbums()
        {
            var albums = await _unitOfWork.Albums.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<AlbumDto>>(albums));
        }

        // GET: api/album/{id}
        /// <summary>
        /// Retrieves an album by ID.
        /// </summary>
        /// <param name="id">The ID of the album to retrieve.</param>
        /// <returns>The album with the specified ID.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<AlbumDto>> GetAlbumById(long id)
        {
            var album = await _unitOfWork.Albums.GetAlbumWithDetailsAsync(id);

            if (album == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<AlbumDto>(album));
        }

        // GET: api/album/artist/{artistId}
        /// <summary>
        /// Retrieves all albums by a specific artist ID.
        /// </summary>
        /// <param name="artistId">The ID of the artist to retrieve albums for.</param>
        /// <returns>The list of albums by the artist with the specified ID.</returns>
        [HttpGet("artist/{artistId}")]
        public async Task<ActionResult<IEnumerable<AlbumDto>>> GetAlbumsByArtistId(long artistId)
        {
            var albums = await _unitOfWork.Albums.GetAlbumsByArtistIdAsync(artistId);

            if (albums == null || !albums.Any())
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<AlbumDto>>(albums));
        }

        // GET: api/album/genre/{genreId}
        /// <summary>
        /// Retrieves all albums by a specific genre ID.
        /// </summary>
        /// <param name="genreId">The ID of the genre to retrieve albums for.</param>
        /// <returns>The list of albums belonging to the genre with the specified ID.</returns>
        [HttpGet("genre/{genreId}")]
        public async Task<ActionResult<IEnumerable<AlbumDto>>> GetAlbumsByGenreId(int genreId)
        {
            var albums = await _unitOfWork.Albums.GetAlbumsByGenreIdAsync(genreId);

            if (albums == null || !albums.Any())
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<AlbumDto>>(albums));
        }

        // GET: api/album/recordlabel/{recordLabelId}
        /// <summary>
        /// Retrieves a list of albums associated with the specified record label.
        /// </summary>
        /// <param name="recordLabelId">The ID of the record label to retrieve albums for.</param>
        /// <returns>A list of albums associated with the specified record label.</returns>
        [HttpGet("recordLabel/{recordLabelId}")]
        public async Task<ActionResult<IEnumerable<AlbumDto>>> GetAlbumsByRecordLabelId(long recordLabelId)
        {
            var albums = await _unitOfWork.Albums.GetAlbumsByRecordLabelIdAsync(recordLabelId);

            if (albums == null || !albums.Any())
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<AlbumDto>>(albums));
        }

        // GET: api/album/search
        /// <summary>
        /// Searches for albums that match the specified search term.
        /// </summary>
        /// <param name="term">The search term used to filter albums.</param>
        /// <param name="pageNumber">The page number of the results to retrieve. Must be greater than or equal to 1. Defaults to 1 if not
        /// specified.</param>
        /// <param name="pageSize">The number of albums to include in each page of results. Must be between 1 and 100. Defaults to 10 if not
        /// specified or if an invalid value is provided.</param>
        /// <returns>An <see cref="ActionResult{T}"/> containing a collection of <see cref="AlbumDto"/> objects representing the
        /// albums that match the search criteria.</returns>
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<AlbumDto>>> SearchAlbums([FromQuery] string term, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1 || pageSize > 100) pageSize = 10;

            var albums = await _unitOfWork.Albums.SearchAlbumsAsync(term ?? string.Empty, pageNumber, pageSize);
            return Ok(_mapper.Map<IEnumerable<AlbumDto>>(albums));
        }

        // POST: api/album
        /// <summary>
        /// Creates a new album.
        /// </summary>
        /// <param name="createAlbumDto">The DTO containing the album information.</param>
        /// <returns>The created album.</returns>
        [HttpPost]
        public async Task<ActionResult<AlbumDto>> CreateAlbum([FromBody] CreateAlbumDto createAlbumDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var album = _mapper.Map<Album>(createAlbumDto);
            await _unitOfWork.Albums.AddAsync(album);
            await _unitOfWork.SaveChangesAsync();

            var albumDto = _mapper.Map<AlbumDto>(album);
            return CreatedAtAction(nameof(GetAlbumById), new { id = album.Id }, albumDto);
        }


        // TODO: Implement an UpdateAlbumDto class for updating albums. Using create DTO for now.
        // PUT: api/album/{id}
        /// <summary>
        /// Updates an existing album.
        /// </summary>
        /// <param name="id">The ID of the album to update.</param>
        /// <param name="createAlbumDto">The DTO containing the updated artist information</param>
        /// <returns>No content if the update was successful.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAlbum(long id, [FromBody] CreateAlbumDto createAlbumDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingAlbum = await _unitOfWork.Albums.GetByIdAsync(id);

            if (existingAlbum == null)
            {
                return NotFound();
            }

            _mapper.Map(createAlbumDto, existingAlbum);
            _unitOfWork.Albums.Update(existingAlbum);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/album/{id}
        /// <summary>
        /// Deletes an album with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the album to delete.</param>
        /// <returns>No content if the deletion was successful.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlbum(long id)
        {
            var existingAlbum = await _unitOfWork.Albums.GetByIdAsync(id);

            if (existingAlbum == null)
            {
                return NotFound();
            }

            _unitOfWork.Albums.Remove(existingAlbum);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }
    }
}
