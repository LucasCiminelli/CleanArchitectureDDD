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

namespace CleanArchitecture.Application.Features.Videos.Commands.DeleteVideo
{
    public class DeleteVideoCommandHandler : IRequestHandler<DeleteVideoCommand>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteVideoCommandHandler> _logger;

        public DeleteVideoCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<DeleteVideoCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteVideoCommand request, CancellationToken cancellationToken)
        {
            var videoToDelete = await _unitOfWork.VideoRepository.GetByIdAsync(request.Id);

            if (videoToDelete == null) 
            {
                _logger.LogError($"No se encontró ningun video con el Id {request.Id}");
                throw new NotFoundException(nameof(Video), request);

            }

            _unitOfWork.VideoRepository.DeleteEntity(videoToDelete);

            var result = await _unitOfWork.Complete();

            _logger.LogInformation($"El Video con Id {videoToDelete.Id} fue eliminado exitosamente");

            return Unit.Value;

        }
    }
}
