using AutoMapper;
using CleanArchitecture.Application.Contracts.Percistence;
using CleanArchitecture.Application.Features.Videos.Queries.GetVideosList;
using CleanArchitecture.Application.Mappings;
using CleanArchitecture.Application.UnitTests.Mocks;
using Moq;
using Shouldly;
using Xunit;

namespace CleanArchitecture.Application.UnitTests.Features.Videos.Queries
{
    public class GetVideosListQueryHandlerXUnitTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _unitOfWork;

        public GetVideosListQueryHandlerXUnitTests()
        {
            _unitOfWork = MockUnitOfWork.GetUnitOfWork();
            var mapConfig = new MapperConfiguration(conf => conf.AddProfile<MappingProfile>());
            _mapper = mapConfig.CreateMapper();
        }

        [Fact]
        public async Task GetVideoListTest()
        {
            var handler = new GetVideosListQueryHandler(_unitOfWork.Object, _mapper);
            var username = "alejandro";
            var request = new GetVideosListQuery(username);

            var result = await handler.Handle(request, CancellationToken.None);

            result.ShouldBeOfType<List<VideoVm>>();
            Assert.True(result.Count > 0);
        }
    }
}
