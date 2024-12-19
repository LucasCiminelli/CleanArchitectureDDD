using AutoMapper;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Features.Videos.Queries.Vms;
using CleanArchitecture.Application.Specifications.Videos;
using CleanArchitecture.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Videos.Queries.GetVideosListByStreamerId
{
    public class GetVideosListByStreamerIdQueryHandler : IRequestHandler<GetVideosListByStreamerIdQuery, List<VideosVm>>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetVideosListByStreamerIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<VideosVm>> Handle(GetVideosListByStreamerIdQuery request, CancellationToken cancellationToken)
        {

            var specification = new VideosWithActorsSpecifications(request._StreamerId);

            var videosList = await _unitOfWork.Repository<Video>().GetAllWithSpec(specification);

            return _mapper.Map<List<VideosVm>>(videosList);

        }
    }
}
