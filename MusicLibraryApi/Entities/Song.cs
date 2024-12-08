namespace MusicLibraryApi.Entities;

public class Song
{
	public int Id { get; set; }
	public string Title { get; set; } = string.Empty;
	public int ArtistId { get; set; }
	public virtual Artist Artist { get; set; } = default!;
	public List<SongGenre> SongGenres { get; set; } = [];
}