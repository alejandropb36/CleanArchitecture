using CleanArchitecture.Data;
using CleanArchitecture.Domain;
using Microsoft.EntityFrameworkCore;

StreamerDbContext streamerDbContext = new();

//await QueryFilter();
//await QueryMethods();
//await QueryLinq();
await TrakingAndNoTraking();


Console.WriteLine("Presiona cualquier tecla para termina");
Console.ReadKey();

async Task TrakingAndNoTraking()
{
    var streamerWithTraking = await streamerDbContext!.Streamers!.FirstOrDefaultAsync(x => x.Id == 1);

    // Libera de memoria el objeto, es recomendable en querys en las cuales no se va a modificar los registros
    var streamerWithNoTraking = await streamerDbContext!.Streamers!.AsNoTracking().FirstOrDefaultAsync(x => x.Id == 2);

    streamerWithTraking.Nombre = "Netflix Super";
    streamerWithNoTraking.Nombre = "Amazon Plus";

    await streamerDbContext!.SaveChangesAsync();

}

async Task QueryLinq()
{
    Console.WriteLine($"Ingresa el servicio de streaming: ");

    var streamerNombre = Console.ReadLine();
    var streamers = await (from i in streamerDbContext.Streamers
                           where EF.Functions.Like(i.Nombre, $"%{streamerNombre}%")
                           select i).ToListAsync();

    foreach(var streamer in streamers)
    {
        Console.WriteLine($"{streamer.Id} - {streamer.Nombre}");
    }
}

async Task QueryMethods()
{
    var streamer = streamerDbContext!.Streamers!;
    var firstAsync = await streamer.Where(y => y.Nombre.Contains("a")).FirstAsync();
    var whereFirstOrDefaultAsync = await streamer.Where(y => y.Nombre.Contains("a")).FirstOrDefaultAsync();
    var firstOrDefaultAsync = await streamer.FirstOrDefaultAsync(y => y.Nombre.Contains("a"));

    var singleAsync = await streamer.Where(x => x.Id == 1).SingleAsync();
    var singleOrDefaultAsync = await streamer.Where(x => x.Id == 1).SingleOrDefaultAsync();
    
    var findAsync = await streamer.FindAsync(1);
}

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

async  Task AddStreamear()
{
    Streamer streamer = new()
    {
        Nombre = "Disney",
        Url = "https://www.disney.com"
    };

    streamerDbContext!.Streamers!.Add(streamer);

    await streamerDbContext.SaveChangesAsync();
}

async Task AddRangeVideos()
{
    var streamer = await streamerDbContext.Streamers.FindAsync(1);

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
}
