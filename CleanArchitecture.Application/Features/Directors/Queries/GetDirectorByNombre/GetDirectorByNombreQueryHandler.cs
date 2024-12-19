using AutoMapper;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Application.Features.Directors.Queries.Vms;
using CleanArchitecture.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Directors.Queries.GetDirectorByNombre
{
    public class GetDirectorByNombreQueryHandler : IRequestHandler<GetDirectorByNombreQuery, DirectorsVm>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetDirectorByNombreQueryHandler> _logger;

        public GetDirectorByNombreQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GetDirectorByNombreQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<DirectorsVm> Handle(GetDirectorByNombreQuery request, CancellationToken cancellationToken)
        {
            var director = await _unitOfWork.DirectorRepository.GetDirectorByName(request._Nombre);


            if(director == null)
            {
                _logger.LogError($"El director con el nombre{request._Nombre} no existe");
                throw new NotFoundException(nameof(Director), request._Nombre);
            }

            return _mapper.Map<DirectorsVm>(director);

        }
    }
}
