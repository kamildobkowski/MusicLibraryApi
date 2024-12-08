using Microsoft.EntityFrameworkCore;
using MusicLibraryApi.Entities;
using MusicLibraryApi.Database;
using MusicLibraryApi.Models;

namespace MusicLibraryApi.Services
{
    public class ArtistService
    {
        private readonly MusicDbContext _context;

        public ArtistService(MusicDbContext context)
        {
            _context = context;
        }

        public async Task<List<GetArtistResponse>> GetAllArtistsAsync()
        {
            return await _context.Artists
                .Select(a => new GetArtistResponse
                {
                    Id = a.Id,
                    Name = a.Name
                })
                .ToListAsync();
        }

        public async Task<GetArtistResponse?> GetArtistByIdAsync(int id)
        {
            return await _context.Artists
                .Where(a => a.Id == id)
                .Select(a => new GetArtistResponse
                {
                    Id = a.Id,
                    Name = a.Name,
                    CountryOfOrigin = a.CountryOfOrigin
                })
                .FirstOrDefaultAsync();
        }

        public async Task<CreateArtistResponse> CreateArtistAsync(CreateArtistRequest request)
        {
            var artist = new Artist
            {
                Name = request.Name,
                CountryOfOrigin = request.CountryOfOrigin
            };

            _context.Artists.Add(artist);
            await _context.SaveChangesAsync();

            return new CreateArtistResponse
            {
                Id = artist.Id,
                Name = artist.Name,
                CountryOfOrigin = artist.CountryOfOrigin
            };
        }

        public async Task<UpdateArtistResponse> UpdateArtistAsync(UpdateArtistRequest request)
        {
            var artist = await _context.Artists.FindAsync(request.Id);
            if (artist == null)
            {
                throw new KeyNotFoundException("Artist not found");
            }

            artist.Name = request.Name;
            artist.CountryOfOrigin = request.CountryOfOrigin;

            _context.Entry(artist).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return new UpdateArtistResponse
            {
                Id = artist.Id,
                Name = artist.Name,
                CountryOfOrigin = artist.CountryOfOrigin
            };
        }

        public async Task DeleteArtistAsync(int id)
        {
            var artist = await _context.Artists.FindAsync(id);
            if (artist != null)
            {
                _context.Artists.Remove(artist);
                await _context.SaveChangesAsync();
            }
        }
    }
}