using AutoMapper;
using CleanArchitecture.Application.Contracts.Percistence;
using MediatR;

namespace CleanArchitecture.Application.Features.Videos.Queries.GetVideosList
{
    public class GetVideosListQueryHandler : IRequestHandler<GetVideosListQuery, List<VideoVm>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetVideosListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<VideoVm>> Handle(GetVideosListQuery request, CancellationToken cancellationToken)
        {
            var videoList = await _unitOfWork.VideoRepository.GetVideoByUserName(request.Username);
            

            return _mapper.Map<List<VideoVm>>(videoList);
        }
    }
}
