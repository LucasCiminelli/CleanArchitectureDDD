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

namespace CleanArchitecture.Application.Features.Videos.Commands.UpdateVideo
{
    public class UpdateVideoCommandHandler : IRequestHandler<UpdateVideoCommand>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UpdateVideoCommandHandler> _logger;
        private readonly IMapper _mapper;

        public UpdateVideoCommandHandler(IUnitOfWork unitOfWork, ILogger<UpdateVideoCommandHandler> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }


        public async Task<Unit> Handle(UpdateVideoCommand request, CancellationToken cancellationToken)
        {
            var videoToUpdate = await _unitOfWork.VideoRepository.GetByIdAsync(request.Id);


            if (videoToUpdate == null)
            {

                _logger.LogError($"no se encontró ningun registro para el id {request.Id}");
                throw new NotFoundException(nameof(Video), request);
            
            }

            _mapper.Map(request, videoToUpdate, typeof(UpdateVideoCommand), typeof(Video));

            _unitOfWork.VideoRepository.UpdateEntity(videoToUpdate);

            var result = await _unitOfWork.Complete();

            if (result <= 1)
            {
                _logger.LogError("Error al actualizar la entidad");
            }

            _logger.LogInformation("El registro se actualizó correctamente");


            return Unit.Value;

        }
    }
}
