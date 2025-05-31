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
            var album = await _unitOfWork.Albums.GetByIdAsync(id);

            if (album == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<AlbumDto>(album));
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
