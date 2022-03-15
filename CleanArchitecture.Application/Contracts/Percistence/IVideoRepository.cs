using CleanArchitecture.Domain;

namespace CleanArchitecture.Application.Contracts.Percistence
{
    public interface IVideoRepository: IAsyncRepository<Video>
    {
        Task<Video> GetVideoByName(string nombreVideo);
        Task<IEnumerable<Video>> GetVideoByUserName(string username);
    }
}
