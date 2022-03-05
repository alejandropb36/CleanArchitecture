using CleanArchitecture.Data;
using CleanArchitecture.Domain;
using Microsoft.EntityFrameworkCore;

StreamerDbContext streamerDbContext = new();

await QueryFilter();

Console.WriteLine("Presiona cualquier tecla para termina");
Console.ReadKey();

async Task QueryFilter()
{
    Console.WriteLine($"Ingresa una compañia de streaming: ");
    var streamingNombre = Console.ReadLine();

    //var streamers = await streamerDbContext.Streamers.Where(x => x.Nombre == streamingNombre).ToListAsync();
    var streamers = await streamerDbContext!.Streamers!.Where(x => x.Nombre!.Equals(streamingNombre)).ToListAsync();
    foreach (var streamer in streamers)
    {
        Console.WriteLine($"{streamer.Id} - {streamer.Nombre}");
    }
    
    
    //var streamerPartialResults = await streamerDbContext!.Streamers!.Where(x => x.Nombre!.Contains(streamingNombre!)).ToListAsync();
    var streamerPartialResults = await streamerDbContext!.Streamers!.Where(x => EF.Functions.Like(x.Nombre, $"%{streamingNombre}%")).ToListAsync();
    foreach (var streamer in streamerPartialResults)
    {
        Console.WriteLine($"{streamer.Id} - {streamer.Nombre}");
    }
}

//Streamer streamer = new()
//{
//    Nombre = "Amazon Prime",
//    Url = "https://www.amazonprime.com"
//};

//streamerDbContext!.Streamers!.Add(streamer);

//await streamerDbContext.SaveChangesAsync();

//var movies = new List<Video>()
//{
//    new Video()
//    {
//        Nombre = "Mad Max",
//        StreamerId = streamer.Id
//    },
//    new Video()
//    {
//        Nombre = "Batman",
//        StreamerId = streamer.Id
//    },
//    new Video()
//    {
//        Nombre = "Animales Fantasticos",
//        StreamerId = streamer.Id
//    },
//    new Video()
//    {
//        Nombre = "Forest Gump",
//        StreamerId = streamer.Id
//    }
//};

//await streamerDbContext.AddRangeAsync(movies);

//await streamerDbContext.SaveChangesAsync();
