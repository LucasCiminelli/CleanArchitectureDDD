using AutoMapper;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Features.Videos.Queries.GetVideosList;
using CleanArchitecture.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Videos.Commands.CreateVideo
{
    public class CreateVideoCommandHandler : IRequestHandler<CreateVideoCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateVideoCommandHandler> _logger;

        public CreateVideoCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CreateVideoCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<int> Handle(CreateVideoCommand request, CancellationToken cancellationToken)
        {
            var videoEntity = _mapper.Map<Video>(request);

            _unitOfWork.Repository<Video>().AddEntity(videoEntity);

            var result = await _unitOfWork.Complete();

            if (result <= 0)
            {
                _logger.LogError($"Error al intentar crear el registro de video");
                throw new Exception("Error al intentar crear el registro de video");
            }

            _logger.LogInformation("El registro  se creó exitosamente");


            return videoEntity.Id;

        }
    }
}
