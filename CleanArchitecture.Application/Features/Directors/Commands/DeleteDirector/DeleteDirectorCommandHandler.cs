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

namespace CleanArchitecture.Application.Features.Directors.Commands.DeleteDirector
{
    public class DeleteDirectorCommandHandler : IRequestHandler<DeleteDirectorCommand>
    {

        private readonly ILogger<DeleteDirectorCommandHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteDirectorCommandHandler(ILogger<DeleteDirectorCommandHandler> logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteDirectorCommand request, CancellationToken cancellationToken)
        {
            var directorToDelete = await _unitOfWork.Repository<Director>().GetByIdAsync(request.Id);


            if (directorToDelete == null)
            {

                _logger.LogError($"No se encontró el director con id {request.Id}");
                throw new ArgumentException($"No se encontró el director con id {request.Id}");
            }

            _unitOfWork.Repository<Director>().DeleteEntity(directorToDelete);
            await _unitOfWork.Complete();

            _logger.LogInformation($"El actor con id {directorToDelete.Id} fue eliminado exitosamente");


            return Unit.Value;
        }
    }
}
