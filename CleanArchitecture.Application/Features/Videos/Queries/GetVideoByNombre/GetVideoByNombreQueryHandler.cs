using AutoMapper;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Application.Features.Videos.Queries.GetVideosList;
using CleanArchitecture.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Videos.Queries.GetVideoByNombre
{
    public class GetVideoByNombreQueryHandler : IRequestHandler<GetVideoByNombreQuery, VideosVm>
    {

        private readonly ILogger<GetVideoByNombreQueryHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetVideoByNombreQueryHandler(ILogger<GetVideoByNombreQueryHandler> logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<VideosVm> Handle(GetVideoByNombreQuery request, CancellationToken cancellationToken)
        {
            var video = await _unitOfWork.VideoRepository.GetVideoByNombre(request._Nombre);

            if (video == null)
            {
                _logger.LogError($"No se encontró un video con el nombre {request._Nombre}");
                throw new NotFoundException(nameof(Video), request._Nombre);
            }

            return _mapper.Map<VideosVm>(video);
        }
    }
}
