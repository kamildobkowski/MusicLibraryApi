using Microsoft.EntityFrameworkCore;
using MusicLibraryApi.Entities;

namespace MusicLibraryApi.Database;

public sealed class MusicDbContext(DbContextOptions<MusicDbContext> options) : DbContext(options)
{
	public DbSet<Song> Songs { get; set; }
	public DbSet<Artist> Artists { get; set; }
	public DbSet<Genre> Genres { get; set; }
	public DbSet<SongGenre> SongGenres { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<SongGenre>()
			.HasKey(sg => new { sg.SongId, sg.GenreId });

		modelBuilder.Entity<SongGenre>()
			.HasOne(sg => sg.Song)
			.WithMany(s => s.SongGenres)
			.HasForeignKey(sg => sg.SongId);

		modelBuilder.Entity<SongGenre>()
			.HasOne(sg => sg.Genre)
			.WithMany(g => g.SongGenres)
			.HasForeignKey(sg => sg.GenreId);

		modelBuilder.Entity<Song>()
			.HasOne(s => s.Artist)
			.WithMany(a => a.Songs)
			.HasForeignKey(s => s.ArtistId);

		base.OnModelCreating(modelBuilder);
	}
}