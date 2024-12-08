using Microsoft.AspNetCore.Mvc;
using MusicLibraryApi.Models;
using MusicLibraryApi.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace MusicLibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private readonly SongService _songService;

        public SongsController(SongService songService)
        {
            _songService = songService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get all songs")]
        [SwaggerResponse(200, "Returns the list of all songs", typeof(IEnumerable<GetSongResponse>))]
        public async Task<ActionResult<IEnumerable<GetSongResponse>>> GetSongs()
        {
            var songs = await _songService.GetAllSongsAsync();
            return Ok(songs);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get a song by ID")]
        [SwaggerResponse(200, "Returns the song", typeof(GetSongResponse))]
        [SwaggerResponse(404, "Song not found")]
        public async Task<ActionResult<GetSongResponse>> GetSong(int id)
        {
            var song = await _songService.GetSongByIdAsync(id);
            if (song == null)
            {
                return NotFound();
            }
            return Ok(song);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create a new song")]
        [SwaggerResponse(201, "Song created", typeof(CreateSongResponse))]
        public async Task<ActionResult<CreateSongResponse>> CreateSong(CreateSongRequest request)
        {
            var createdSong = await _songService.CreateSongAsync(request);
            return CreatedAtAction(nameof(GetSong), new { id = createdSong.Id }, createdSong);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update an existing song")]
        [SwaggerResponse(200, "Song updated", typeof(UpdateSongResponse))]
        [SwaggerResponse(400, "Invalid request")]
        public async Task<IActionResult> UpdateSong(int id, UpdateSongRequest request)
        {
            if (id != request.Id)
            {
                return BadRequest();
            }

            var updatedSong = await _songService.UpdateSongAsync(request);
            return Ok(updatedSong);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete a song")]
        [SwaggerResponse(204, "Song deleted")]
        public async Task<IActionResult> DeleteSong(int id)
        {
            await _songService.DeleteSongAsync(id);
            return NoContent();
        }
        
        [HttpGet("artist/{artistId}")]
        [SwaggerOperation(Summary = "Get songs by artist ID")]
        [SwaggerResponse(200, "Returns the list of songs for the specified artist", typeof(IEnumerable<GetSongResponse>))]
        public async Task<ActionResult<IEnumerable<GetSongResponse>>> GetSongsByArtist(int artistId)
        {
            var songs = await _songService.GetSongsByArtistIdAsync(artistId);
            return Ok(songs);
        }
    }
}