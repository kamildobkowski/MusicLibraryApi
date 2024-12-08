namespace MusicLibraryApi.Entities;

public class Artist
{
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public string? CountryOfOrigin { get; set; }

	public List<Song> Songs { get; set; } = [];
}