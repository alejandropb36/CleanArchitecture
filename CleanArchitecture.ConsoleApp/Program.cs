using CleanArchitecture.Data;
using CleanArchitecture.Domain;

StreamerDbContext streamerDbContext = new();

Streamer streamer = new()
{
    Nombre = "Amazon Prime",
    Url = "https://www.amazonprime.com"
};

streamerDbContext!.Streamers!.Add(streamer);

await streamerDbContext.SaveChangesAsync();

var movies = new List<Video>()
{
    new Video()
    {
        Nombre = "Mad Max",
        StreamerId = streamer.Id
    },
    new Video()
    {
        Nombre = "Batman",
        StreamerId = streamer.Id
    },
    new Video()
    {
        Nombre = "Animales Fantasticos",
        StreamerId = streamer.Id
    },
    new Video()
    {
        Nombre = "Forest Gump",
        StreamerId = streamer.Id
    }
};

await streamerDbContext.AddRangeAsync(movies);

await streamerDbContext.SaveChangesAsync();
