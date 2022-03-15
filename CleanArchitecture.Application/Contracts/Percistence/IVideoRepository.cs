using CleanArchitecture.Domain;

namespace CleanArchitecture.Application.Contracts.Percistence
{
    public interface IVideoRepository: IAsyncRepository<Video>
    {
        Task<IReadOnlyCollection<Video>> GetVideoByName(string nombreVideo);
    }
}
