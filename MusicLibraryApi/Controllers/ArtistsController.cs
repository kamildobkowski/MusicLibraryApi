using Microsoft.AspNetCore.Mvc;
using MusicLibraryApi.Models;
using MusicLibraryApi.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace MusicLibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        private readonly ArtistService _artistService;

        public ArtistsController(ArtistService artistService)
        {
            _artistService = artistService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get all artists")]
        [SwaggerResponse(200, "Returns the list of all artists", typeof(IEnumerable<GetArtistResponse>))]
        public async Task<ActionResult<IEnumerable<GetArtistResponse>>> GetArtists()
        {
            var artists = await _artistService.GetAllArtistsAsync();
            return Ok(artists);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get an artist by ID")]
        [SwaggerResponse(200, "Returns the artist", typeof(GetArtistResponse))]
        [SwaggerResponse(404, "Artist not found")]
        public async Task<ActionResult<GetArtistResponse>> GetArtist(int id)
        {
            var artist = await _artistService.GetArtistByIdAsync(id);
            if (artist == null)
            {
                return NotFound();
            }
            return Ok(artist);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create a new artist")]
        [SwaggerResponse(201, "Artist created", typeof(CreateArtistResponse))]
        public async Task<ActionResult<CreateArtistResponse>> CreateArtist(CreateArtistRequest request)
        {
            var createdArtist = await _artistService.CreateArtistAsync(request);
            return CreatedAtAction(nameof(GetArtist), new { id = createdArtist.Id }, createdArtist);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update an existing artist")]
        [SwaggerResponse(200, "Artist updated", typeof(UpdateArtistResponse))]
        [SwaggerResponse(400, "Invalid request")]
        public async Task<IActionResult> UpdateArtist(int id, UpdateArtistRequest request)
        {
            if (id != request.Id)
            {
                return BadRequest();
            }

            var updatedArtist = await _artistService.UpdateArtistAsync(request);
            return Ok(updatedArtist);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete an artist")]
        [SwaggerResponse(204, "Artist deleted")]
        public async Task<IActionResult> DeleteArtist(int id)
        {
            await _artistService.DeleteArtistAsync(id);
            return NoContent();
        }
    }
}