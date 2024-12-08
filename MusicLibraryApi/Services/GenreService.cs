using Microsoft.EntityFrameworkCore;
using MusicLibraryApi.Database;
using MusicLibraryApi.Entities;
using MusicLibraryApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicLibraryApi.Services
{
    public class GenreService
    {
        private readonly MusicDbContext _context;

        public GenreService(MusicDbContext context)
        {
            _context = context;
        }

        public async Task<List<GetGenreResponse>> GetAllGenresAsync()
        {
            return await _context.Genres
                .Select(g => new GetGenreResponse
                {
                    Id = g.Id,
                    Name = g.Name
                })
                .ToListAsync();
        }

        public async Task<GetGenreResponse?> GetGenreByIdAsync(int id)
        {
            return await _context.Genres
                .Where(g => g.Id == id)
                .Select(g => new GetGenreResponse
                {
                    Id = g.Id,
                    Name = g.Name
                })
                .FirstOrDefaultAsync();
        }

        public async Task<CreateGenreResponse> CreateGenreAsync(CreateGenreRequest request)
        {
            var genre = new Genre
            {
                Name = request.Name
            };

            _context.Genres.Add(genre);
            await _context.SaveChangesAsync();

            return new CreateGenreResponse
            {
                Id = genre.Id,
                Name = genre.Name
            };
        }

        public async Task<UpdateGenreResponse> UpdateGenreAsync(UpdateGenreRequest request)
        {
            var genre = await _context.Genres.FindAsync(request.Id);
            if (genre == null)
            {
                throw new KeyNotFoundException("Genre not found");
            }

            genre.Name = request.Name;

            _context.Entry(genre).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return new UpdateGenreResponse
            {
                Id = genre.Id,
                Name = genre.Name
            };
        }

        public async Task DeleteGenreAsync(int id)
        {
            var genre = await _context.Genres.FindAsync(id);
            if (genre != null)
            {
                _context.Genres.Remove(genre);
                await _context.SaveChangesAsync();
            }
        }
    }
}