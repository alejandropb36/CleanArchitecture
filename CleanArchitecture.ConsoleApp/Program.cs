using CleanArchitecture.Data;
using CleanArchitecture.Domain;
using Microsoft.EntityFrameworkCore;

StreamerDbContext dbContext = new();

await MultipleEntitiesQuery();
//await AddNewDirectoWithVideo();
//await AddNewActorWithVideo();
//await AddNewStreamerWithVideoId();
//await AddNewStreamerWithVideo();
//await TrakingAndNoTraking();
//await QueryLinq();
//await QueryMethods();
//await QueryFilter();


Console.WriteLine("Presiona cualquier tecla para termina");
Console.ReadKey();

async Task MultipleEntitiesQuery()
{
    //var videoWithActores = await dbContext!.Videos!
    //    .Include(v => v.Actores)
    //    .FirstOrDefaultAsync(v => v.Id == 1);

    //var actors = await dbContext!.Actores!.Select(a => a.Nombre).ToListAsync();
    var videosWithDirector = await dbContext!.Videos!
        .Where(v => v.Director != null)
        .Include(v => v.Director)
        .Select(v =>
            new
            {
                DirectorMio = $"{v.Director!.Nombre} {v.Director.Apellido}",
                Movie = v.Nombre

            }
        ).ToListAsync();

    foreach(var video in videosWithDirector)
    {
        Console.WriteLine($"{video.Movie} - {video.DirectorMio}");
    }
}

async Task AddNewDirectoWithVideo()
{
    var director = new Director
    {
        Nombre = "Lorenzo",
        Apellido = "Basteri",
        VideoId = 1
    };

    await dbContext.AddAsync(director);
    await dbContext.SaveChangesAsync();

}

async Task AddNewActorWithVideo()
{
    var actor = new Actor
    {
        Nombre = "Brad",
        Apellido = "Pitt"
    };
    await dbContext.AddAsync(actor);
    await dbContext.SaveChangesAsync();

    var videoActor = new VideoActor
    {
        ActorId = actor.Id,
        VideoId = 1
    };

    await dbContext.AddAsync(videoActor);
    await dbContext.SaveChangesAsync();
}

async Task AddNewStreamerWithVideoId()
{

    var batmanForever= new Video
    {
        Nombre = "Batman Forever",
        StreamerId =  1007
    };

    await dbContext.AddAsync(batmanForever);
    await dbContext.SaveChangesAsync();
}

async Task AddNewStreamerWithVideo()
{
    var pantaya = new Streamer
    {
        Nombre = "Pantaya"
    };

    var hungerGames = new Video
    {
        Nombre = "Hunger Games",
        Streamer = pantaya
    };

    await dbContext.AddAsync(hungerGames);
    await dbContext.SaveChangesAsync();
}

async Task TrakingAndNoTraking()
{
    var streamerWithTraking = await dbContext!.Streamers!.FirstOrDefaultAsync(x => x.Id == 1);

    // Libera de memoria el objeto, es recomendable en querys en las cuales no se va a modificar los registros
    var streamerWithNoTraking = await dbContext!.Streamers!.AsNoTracking().FirstOrDefaultAsync(x => x.Id == 2);

    streamerWithTraking.Nombre = "Netflix Super";
    streamerWithNoTraking.Nombre = "Amazon Plus";

    await dbContext!.SaveChangesAsync();

}

async Task QueryLinq()
{
    Console.WriteLine($"Ingresa el servicio de streaming: ");

    var streamerNombre = Console.ReadLine();
    var streamers = await (from i in dbContext.Streamers
                           where EF.Functions.Like(i.Nombre, $"%{streamerNombre}%")
                           select i).ToListAsync();

    foreach(var streamer in streamers)
    {
        Console.WriteLine($"{streamer.Id} - {streamer.Nombre}");
    }
}

async Task QueryMethods()
{
    var streamer = dbContext!.Streamers!;
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
    var streamers = await dbContext!.Streamers!.Where(x => x.Nombre!.Equals(streamingNombre)).ToListAsync();
    foreach (var streamer in streamers)
    {
        Console.WriteLine($"{streamer.Id} - {streamer.Nombre}");
    }
    
    
    //var streamerPartialResults = await streamerDbContext!.Streamers!.Where(x => x.Nombre!.Contains(streamingNombre!)).ToListAsync();
    var streamerPartialResults = await dbContext!.Streamers!.Where(x => EF.Functions.Like(x.Nombre, $"%{streamingNombre}%")).ToListAsync();
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

    dbContext!.Streamers!.Add(streamer);

    await dbContext.SaveChangesAsync();
}

async Task AddRangeVideos()
{
    var streamer = await dbContext.Streamers.FindAsync(1);

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

    await dbContext.AddRangeAsync(movies);

    await dbContext.SaveChangesAsync();
}
