using AutoMapper;
using CleanArchitecture.Application.Contracts.Infraestructure;
using CleanArchitecture.Application.Features.Streamers.Commands.UpdateStreamer;
using CleanArchitecture.Application.Mappings;
using CleanArchitecture.Application.UnitTests.Mocks;
using CleanArchitecture.Infraestructure.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using Xunit;

namespace CleanArchitecture.Application.UnitTests.Features.Streamers.Commands.UpdateStreamer
{
    public class UpdateStreamerCommandHandlerXUnitTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<UnitOfWork> _unitOfWork;
        private readonly Mock<IEmailService> _emailService;
        private readonly Mock<ILogger<UpdateStreamerCommandHandler>> _logger;

        public UpdateStreamerCommandHandlerXUnitTests()
        {
            _unitOfWork = MockUnitOfWork.GetUnitOfWork();

            var mapConfig = new MapperConfiguration(conf => conf.AddProfile<MappingProfile>());
            _mapper = mapConfig.CreateMapper();

            _emailService = new Mock<IEmailService>();

            _logger = new Mock<ILogger<UpdateStreamerCommandHandler>>();

            MockStreamerRepository.AddDataStreamerRepository(_unitOfWork.Object.StreamerDbContext);
        }

        [Fact]
        public async void UpdateStreamerCommand_InputStreamer_ReturnUnit()
        {
            var streamerInput = new UpdateStreamerCommand
            {
                Id = 8001,
                Nombre = "Streamer actualizado",
                Url = "https://streamer-updated.com"
            };
            var handler = new UpdateStreamerCommandHandler(_unitOfWork.Object, _mapper, _logger.Object);

            var result = await handler.Handle(streamerInput, CancellationToken.None);

            result.ShouldBeOfType<Unit>();
        }
    }
}
