using AutoMapper;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Features.Streamers.Queries.Vms;
using CleanArchitecture.Application.Specifications.Streamers;
using CleanArchitecture.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Streamers.Queries.GetStreamersListByUrl
{
    public class GetStreamersListByUrlQueryHandler : IRequestHandler<GetStreamersListByUrlQuery, List<StreamersVm>>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetStreamersListByUrlQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<StreamersVm>> Handle(GetStreamersListByUrlQuery request, CancellationToken cancellationToken)
        {
            var specification = new StreamersWithVideosSpecification(request._Url!);
            var streamerList = await _unitOfWork.Repository<Streamer>().GetAllWithSpec(specification);

            return _mapper.Map<List<StreamersVm>>(streamerList);
        }

    }
}
