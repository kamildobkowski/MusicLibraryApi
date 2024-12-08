namespace MusicLibraryApi.Models;

public class GetArtistResponse
{
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public string? CountryOfOrigin { get; set; }
}

public class CreateArtistRequest
{
	public string Name { get; set; } = string.Empty;
	public string? CountryOfOrigin { get; set; }
}

public class CreateArtistResponse
{
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public string? CountryOfOrigin { get; set; }
}

public class UpdateArtistRequest
{
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public string? CountryOfOrigin { get; set; }
}

public class UpdateArtistResponse
{
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public string? CountryOfOrigin { get; set; }
}