namespace MusicLibraryApi.Models;

public class GetSongResponse
{
	public int Id { get; set; }
	public string Title { get; set; } = string.Empty;
	public int ArtistId { get; set; }
	public List<string> Genres { get; set; } = [];
}

public class CreateSongRequest
{
	public string Title { get; set; } = string.Empty;
	public int ArtistId { get; set; }
	public List<string> Genres { get; set; } = [];
}

public class CreateSongResponse
{
	public int Id { get; set; }
	public string Title { get; set; } = string.Empty;
	public int ArtistId { get; set; }
	public List<string> Genres { get; set; } = [];
}

public class UpdateSongRequest
{
	public int Id { get; set; }
	public string Title { get; set; } = string.Empty;
	public int ArtistId { get; set; }
	public List<string> Genres { get; set; } = [];
}

public class UpdateSongResponse
{
	public int Id { get; set; }
	public string Title { get; set; } = string.Empty;
	public int ArtistId { get; set; }
	public List<string> Genres { get; set; } = [];
}