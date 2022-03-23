using CleanArchitecture.Domain;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Infraestructure.Persistence
{
    public class StreamerDbContextSeed
    {
        public static async Task SeedAsync(StreamerDbContext context, ILogger<StreamerDbContextSeed> logger)
        {
            if (!context.Streamers!.Any())
            {
                var streamers = GetPreconfiguredStreamers();
                context.Streamers!.AddRange(streamers);
                await context.SaveChangesAsync();
                logger.LogInformation("Estamos agregando nuevos records al db {context}", typeof(StreamerDbContext).Name);
            }
        }

        private static IEnumerable<Streamer> GetPreconfiguredStreamers()
        {
            return new List<Streamer>
            {
                new Streamer { CreatedBy = "Alejandro Ponce", Nombre = "Netflix", Url = "http://www.netflix.com" },
                new Streamer { CreatedBy = "Alejandro Ponce", Nombre = "Amazon Prime", Url = "http://www.amazon-prime.com" },
                new Streamer { CreatedBy = "Alejandro Ponce", Nombre = "HBO", Url = "http://www.hbo.com" },
                new Streamer { CreatedBy = "Alejandro Ponce", Nombre = "Disney +", Url = "http://www.disneyplus.com" },
                new Streamer { CreatedBy = "Alejandro Ponce", Nombre = "Star +", Url = "http://www.starplus.com" },
                new Streamer { CreatedBy = "Alejandro Ponce", Nombre = "Paramount +", Url = "http://www.paramountplus.com" },
            };
        }
    }
}
