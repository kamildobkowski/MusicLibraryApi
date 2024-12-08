namespace MusicLibraryApi.Models;

public class GetGenreResponse
{
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty;
}

public class CreateGenreRequest
{
	public string Name { get; set; } = string.Empty;
}

public class CreateGenreResponse
{
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty;
}

public class UpdateGenreRequest
{
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty;
}

public class UpdateGenreResponse
{
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty;
}