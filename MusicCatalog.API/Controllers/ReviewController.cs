using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MusicCatalog.API.DTOs;
using MusicCatalog.API.Entities;
using MusicCatalog.API.Repositories;

namespace MusicCatalog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReviewController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: api/review
        /// <summary>
        /// Retrieves all reviews.
        /// </summary>
        /// <returns>A list of albums.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetAllReviews()
        {
            var reviews = await _unitOfWork.Reviews.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<ReviewDto>>(reviews));
        }

        // GET: api/review/{id}
        /// <summary>
        /// Retrieves a review by ID.
        /// </summary>
        /// <param name="id">The ID of the review to retrieve.</param>
        /// <returns>The review with the specified ID.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ReviewDto>> GetReviewById(long id)
        {
            var review = await _unitOfWork.Reviews.GetByIdAsync(id);

            if (review == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ReviewDto>(review));
        }

        // POST: api/review
        /// <summary>
        /// Creates a new review.
        /// </summary>
        /// <param name="createReviewDto">The DTO contraining the review information</param>
        /// <returns>The created review.</returns>
        [HttpPost]
        public async Task<ActionResult<ReviewDto>> CreateReview([FromBody] CreateReviewDto createReviewDto)
        {
            if (createReviewDto == null)
            {
                return BadRequest("Invalid review data.");
            }

            var review = _mapper.Map<Review>(createReviewDto);
            await _unitOfWork.Reviews.AddAsync(review);
            await _unitOfWork.SaveChangesAsync();

            var reviewDto = _mapper.Map<ReviewDto>(review);
            return CreatedAtAction(nameof(GetReviewById), new { id = review.Id }, reviewDto);
        }

        // TODO: Implement an UpdateReviewDto class for updating reviews. Using create DTO for now.
        // PUT: api/review/{id}
        /// <summary>
        /// Updates an existing review.
        /// </summary>
        /// <param name="id">The ID of the review to update.</param>
        /// <param name="createReviewDto">The DTO containing the updated review information.</param>
        /// <returns>No content if the update was successful.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReview(long id, [FromBody] CreateReviewDto createReviewDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingReview = await _unitOfWork.Reviews.GetByIdAsync(id);

            if (existingReview == null)
            {
                return NotFound();
            }

            _mapper.Map(createReviewDto, existingReview);
            _unitOfWork.Reviews.Update(existingReview);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/review/{id}
        /// <summary>
        /// Deletes a review by ID.
        /// </summary>
        /// <param name="id">The ID of the review to delete.</param>
        /// <returns>No content if the deletion was successful.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(long id)
        {
            var review = await _unitOfWork.Reviews.GetByIdAsync(id);

            if (review == null)
            {
                return NotFound();
            }

            _unitOfWork.Reviews.Remove(review);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }
    }
}
