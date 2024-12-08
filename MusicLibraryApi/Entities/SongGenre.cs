namespace MusicLibraryApi.Entities;

public class SongGenre
{
	public int SongId { get; set; }
	public virtual Song Song { get; set; } = default!;
	public int GenreId { get; set; }
	public virtual Genre Genre { get; set; } = default!;
}