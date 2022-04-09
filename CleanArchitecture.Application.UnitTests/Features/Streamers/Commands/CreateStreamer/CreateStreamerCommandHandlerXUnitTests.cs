using AutoMapper;
using CleanArchitecture.Application.Contracts.Infraestructure;
using CleanArchitecture.Application.Features.Streamers.Commands;
using CleanArchitecture.Application.Mappings;
using CleanArchitecture.Application.UnitTests.Mocks;
using CleanArchitecture.Infraestructure.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using Xunit;

namespace CleanArchitecture.Application.UnitTests.Features.Streamers.Commands.CreateStreamer
{
    public class CreateStreamerCommandHandlerXUnitTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<UnitOfWork> _unitOfWork;
        private readonly Mock<IEmailService> _emailService;
        private readonly Mock<ILogger<CreateStreamerCommandHandler>> _logger;

        public CreateStreamerCommandHandlerXUnitTests()
        {
            _unitOfWork = MockUnitOfWork.GetUnitOfWork();

            var mapConfig = new MapperConfiguration(conf => conf.AddProfile<MappingProfile>());
            _mapper = mapConfig.CreateMapper();

            _emailService = new Mock<IEmailService>();

            _logger = new Mock<ILogger<CreateStreamerCommandHandler>>();

            MockStreamerRepository.AddDataStreamerRepository(_unitOfWork.Object.StreamerDbContext);
        }

        [Fact]
        public async Task CreateStreamerCommand_InputStreamer_ReturnsNumber()
        {
            var streamerInput = new CreateStreamerCommand
            {
                Nombre = "Alejandro streaming",
                Url = "https://alejandro-streaming.com"
            };

            var handler = new CreateStreamerCommandHandler(_unitOfWork.Object, _mapper, _emailService.Object, _logger.Object);

            var result = await handler.Handle(streamerInput, CancellationToken.None);

            result.ShouldBeOfType<int>();
        }
    }
}
