using AutoMapper;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Application.Features.Actors.Queries.Vms;
using CleanArchitecture.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Actors.Queries.GetActorByNombre
{
    public class GetActorByNombreQueryHandler : IRequestHandler<GetActorByNombreQuery, ActorsVm>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetActorByNombreQueryHandler> _logger;

        public GetActorByNombreQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GetActorByNombreQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ActorsVm> Handle(GetActorByNombreQuery request, CancellationToken cancellationToken)
        {
            var actor = await _unitOfWork.ActorRepository.GetActorByNombre(request._Nombre);

            if(actor == null)
            {
                _logger.LogError($"El actor con nombre {request._Nombre} no existe");
                throw new NotFoundException(nameof(Actor), request._Nombre);
            }


            return _mapper.Map<ActorsVm>(actor);

        }
    }
}
