using AutoFixture;
using CleanArchitecture.Application.Contracts.Percistence;
using CleanArchitecture.Domain;
using Moq;

namespace CleanArchitecture.Application.UnitTests.Mocks
{
    public static class MockVideoRepository
    {
        public static Mock<IVideoRepository> GetVideoRepository()
        {
            var fixture = new Fixture();
            var videos = fixture.CreateMany<Video>().ToList();

            var videoMockRepository = new Mock<IVideoRepository>();
            videoMockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(videos);
            return videoMockRepository;
        }
    }
}
