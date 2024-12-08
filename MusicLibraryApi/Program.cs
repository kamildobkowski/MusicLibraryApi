using Microsoft.EntityFrameworkCore;
using MusicLibraryApi.Database;
using MusicLibraryApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<MusicDbContext>(opt => opt.UseNpgsql("Host=localhost;Database=MusicLibrary;Username=dev;Password=DevPassword123"));
builder.Services.AddScoped<ArtistService>();
builder.Services.AddScoped<SongService>();
builder.Services.AddScoped<GenreService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();