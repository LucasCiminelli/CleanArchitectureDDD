using AutoMapper;
using CleanArchitecture.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Actors.Queries.GetActorsList
{
    public class GetActorListQueryHandler : IRequestHandler<GetActorListQuery, List<ActorsVm>>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetActorListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<ActorsVm>> Handle(GetActorListQuery request, CancellationToken cancellationToken)
        {
            var actorsList = await _unitOfWork.ActorRepository.GetActorsByUsername(request._Username);

            return _mapper.Map<List<ActorsVm>>(actorsList);
        }
    }
}
