using AutoMapper;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Features.Actors.Queries.Vms;
using CleanArchitecture.Application.Features.Shared.Queries;
using CleanArchitecture.Application.Specifications.Actors;
using CleanArchitecture.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Actors.Queries.PaginationActor
{
    public class PaginationActorQueryHandler : IRequestHandler<PaginationActorQuery, PaginationVm<ActorsVm>>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PaginationActorQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PaginationVm<ActorsVm>> Handle(PaginationActorQuery request, CancellationToken cancellationToken)
        {

            var actorSpecificationParams = new ActorSpecificationParams
            {
                Sort = request.Sort,
                Search = request.Search,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
            };

            var spec = new ActorSpecification(actorSpecificationParams);

            var actors = await _unitOfWork.Repository<Actor>().GetAllWithSpec(spec);

            var actorsCount = new ActorForCountingSpecification(actorSpecificationParams);
            var totalActorsFounded = await _unitOfWork.Repository<Actor>().CountAsync(actorsCount);

            var rounded = Math.Ceiling(Convert.ToDecimal(totalActorsFounded) / Convert.ToDecimal(request.PageSize));
            var totalPages = Convert.ToInt32(rounded);

            var data = _mapper.Map<IReadOnlyList<Actor>, IReadOnlyList<ActorsVm>>(actors);

            var pagination = new PaginationVm<ActorsVm>
            {
                Count = totalActorsFounded,
                PageCount = totalPages,
                Data = data,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,

            };

            return pagination;
        }
    }
}
