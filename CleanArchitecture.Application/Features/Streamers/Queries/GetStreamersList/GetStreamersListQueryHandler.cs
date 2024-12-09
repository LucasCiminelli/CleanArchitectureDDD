using AutoMapper;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Streamers.Queries.GetStreamersList
{
    public class GetStreamersListQueryHandler : IRequestHandler<GetStreamersListQuery, List<StreamersVm>>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetStreamersListQueryHandler> _logger;

        public GetStreamersListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GetStreamersListQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<StreamersVm>> Handle(GetStreamersListQuery request, CancellationToken cancellationToken)
        {
            var streamersList = await _unitOfWork.StreamerRepository.GetStreamersByUsername(request._Username);

            if (streamersList == null) 
            {
                _logger.LogError($"No se encontró data relacionada con el usuario {request._Username}");
                throw new NotFoundException(nameof(Streamer), request._Username);

            }

           return _mapper.Map<List<StreamersVm>>(streamersList);

        }
    }
}
