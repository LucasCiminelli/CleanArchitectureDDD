using AutoMapper;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Application.Features.Streamers.Queries.Vms;
using CleanArchitecture.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Streamers.Queries.GetStreamerByNombre
{
    public class GetStreamerByNombreQueryHandler : IRequestHandler<GetStreamerByNombreQuery, StreamersVm>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetStreamerByNombreQueryHandler> _logger;

        public GetStreamerByNombreQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GetStreamerByNombreQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<StreamersVm> Handle(GetStreamerByNombreQuery request, CancellationToken cancellationToken)
        {
            var streamer = await _unitOfWork.StreamerRepository.GetStreamerByNombre(request._Nombre);


            if (streamer == null) 
            {

                _logger.LogError($"No se encontro el streamer con el nombre {request._Nombre}");
                throw new NotFoundException(nameof(Streamer), request._Nombre);

            }

            return _mapper.Map<StreamersVm>(streamer);

        }
    }
}
