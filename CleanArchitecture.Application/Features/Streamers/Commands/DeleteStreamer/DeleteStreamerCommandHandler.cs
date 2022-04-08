using AutoMapper;
using CleanArchitecture.Application.Contracts.Percistence;
using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Features.Streamers.Commands.DeleteStreamer
{
    public class DeleteStreamerCommandHandler : IRequestHandler<DeleteStreamerCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteStreamerCommandHandler> _logger;

        public DeleteStreamerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<DeleteStreamerCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteStreamerCommand request, CancellationToken cancellationToken)
        {
            var streamerToDelete = await _unitOfWork.StreamerRepository.GetByIdAsync(request.Id);
            if (streamerToDelete == null)
            {
                _logger.LogError($"No se encontro el streamer id: {request.Id}");
                throw new NotFoundException(nameof(Streamer), request.Id);
            }

            _unitOfWork.StreamerRepository.DeleteEntity(streamerToDelete);
            var result = await _unitOfWork.Complete();

            if (result <= 0)
            {
                throw new Exception($"El record streamer no se pudo eliminar");
            }

            _logger.LogInformation($"El {request.Id} streamer fue eliminado con exito!");
            return Unit.Value;
        }
    }
}
