using CleanArchitecture.Application.Contracts.Percistence;
using CleanArchitecture.Domain;
using CleanArchitecture.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infraestructure.Repositories
{
    public class VideoRepository : RepositoryBase<Video>, IVideoRepository
    {
        public VideoRepository(StreamerDbContext context) : base(context)
        {
        }

        public async Task<Video> GetVideoByName(string nombreVideo)
        {
            return await _context.Videos!.Where(v => v.Nombre == nombreVideo).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Video>> GetVideoByUserName(string username)
        {
            return await _context.Videos!.Where(v => v.CreatedBy == username).ToListAsync();
        }
    }
}
