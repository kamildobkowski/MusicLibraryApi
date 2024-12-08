using Microsoft.EntityFrameworkCore;
using MusicLibraryApi.Entities;
using MusicLibraryApi.Database;
using MusicLibraryApi.Models;

namespace MusicLibraryApi.Services
{
    public class SongService
    {
        private readonly MusicDbContext _context;

        public SongService(MusicDbContext context)
        {
            _context = context;
        }

        public async Task<List<GetSongResponse>> GetAllSongsAsync()
        {
            return await _context.Songs
                .Include(x => x.SongGenres)
                .ThenInclude(x => x.Genre)
                .Select(s => new GetSongResponse
                {
                    Id = s.Id,
                    Title = s.Title,
                    ArtistId = s.ArtistId,
                    Genres = s.SongGenres.Select(g => g.Genre.Name).ToList()
                })
                .ToListAsync();
        }

        public async Task<GetSongResponse?> GetSongByIdAsync(int id)
        {
            return await _context.Songs
                .Where(s => s.Id == id)
                .Include(x => x.SongGenres)
                .ThenInclude(x => x.Genre)
                .Select(s => new GetSongResponse
                {
                    Id = s.Id,
                    Title = s.Title,
                    ArtistId = s.ArtistId,
                    Genres = s.SongGenres.Select(g => g.Genre.Name).ToList()
                })
                .FirstOrDefaultAsync();
        }

       public async Task<CreateSongResponse> CreateSongAsync(CreateSongRequest request)
        {
            var song = new Song
            {
                Title = request.Title,
                ArtistId = request.ArtistId,
                SongGenres = request.Genres.Select(g => new SongGenre { Genre = new Genre { Name = g } }).ToList()
            };

            _context.Songs.Add(song);
            await _context.SaveChangesAsync();

            return new CreateSongResponse
            {
                Id = song.Id,
                Title = song.Title,
                ArtistId = song.ArtistId,
                Genres = song.SongGenres.Select(sg => sg.Genre.Name).ToList()
            };
        }

        public async Task<UpdateSongResponse> UpdateSongAsync(UpdateSongRequest request)
        {
            var song = await _context.Songs
                .Include(s => s.SongGenres)
                .ThenInclude(songGenre => songGenre.Genre)
                .FirstOrDefaultAsync(s => s.Id == request.Id);

            if (song == null)
            {
                throw new KeyNotFoundException("Song not found");
            }

            song.Title = request.Title;
            song.ArtistId = request.ArtistId;

            // Update genres
            song.SongGenres.Clear();
            song.SongGenres = request.Genres.Select(g => new SongGenre { Genre = new Genre { Name = g } }).ToList();

            _context.Entry(song).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return new UpdateSongResponse
            {
                Id = song.Id,
                Title = song.Title,
                ArtistId = song.ArtistId,
                Genres = song.SongGenres.Select(sg => sg.Genre.Name).ToList()
            };
        }

        public async Task DeleteSongAsync(int id)
        {
            var song = await _context.Songs.FindAsync(id);
            if (song != null)
            {
                _context.Songs.Remove(song);
                await _context.SaveChangesAsync();
            }
        }
        
        public async Task<List<GetSongResponse>> GetSongsByArtistIdAsync(int artistId)
        {
            return await _context.Songs
                .Where(s => s.ArtistId == artistId)
                .Include(s => s.SongGenres)
                .ThenInclude(sg => sg.Genre)
                .Select(s => new GetSongResponse
                {
                    Id = s.Id,
                    Title = s.Title,
                    ArtistId = s.ArtistId,
                    Genres = s.SongGenres.Select(sg => sg.Genre.Name).ToList()
                })
                .ToListAsync();
        }
    }
}