using Microsoft.AspNetCore.Mvc;
using MusicLibraryApi.Models;
using MusicLibraryApi.Services;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicLibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly GenreService _genreService;

        public GenresController(GenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get all genres")]
        [SwaggerResponse(200, "Returns the list of all genres", typeof(IEnumerable<GetGenreResponse>))]
        public async Task<ActionResult<IEnumerable<GetGenreResponse>>> GetGenres()
        {
            var genres = await _genreService.GetAllGenresAsync();
            return Ok(genres);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get a genre by ID")]
        [SwaggerResponse(200, "Returns the genre", typeof(GetGenreResponse))]
        [SwaggerResponse(404, "Genre not found")]
        public async Task<ActionResult<GetGenreResponse>> GetGenre(int id)
        {
            var genre = await _genreService.GetGenreByIdAsync(id);
            if (genre == null)
            {
                return NotFound();
            }
            return Ok(genre);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create a new genre")]
        [SwaggerResponse(201, "Genre created", typeof(CreateGenreResponse))]
        public async Task<ActionResult<CreateGenreResponse>> CreateGenre(CreateGenreRequest request)
        {
            var createdGenre = await _genreService.CreateGenreAsync(request);
            return CreatedAtAction(nameof(GetGenre), new { id = createdGenre.Id }, createdGenre);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update an existing genre")]
        [SwaggerResponse(200, "Genre updated", typeof(UpdateGenreResponse))]
        [SwaggerResponse(400, "Invalid request")]
        public async Task<IActionResult> UpdateGenre(int id, UpdateGenreRequest request)
        {
            if (id != request.Id)
            {
                return BadRequest();
            }

            var updatedGenre = await _genreService.UpdateGenreAsync(request);
            return Ok(updatedGenre);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete a genre")]
        [SwaggerResponse(204, "Genre deleted")]
        public async Task<IActionResult> DeleteGenre(int id)
        {
            await _genreService.DeleteGenreAsync(id);
            return NoContent();
        }
    }
}