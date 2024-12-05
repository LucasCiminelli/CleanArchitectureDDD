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

namespace CleanArchitecture.Application.Features.Directors.Commands.UpdateDirector
{
    public class UpdateDirectorCommandHandler : IRequestHandler<UpdateDirectorCommand>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateDirectorCommandHandler> _logger;

        public UpdateDirectorCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UpdateDirectorCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }


        public async Task<Unit> Handle(UpdateDirectorCommand request, CancellationToken cancellationToken)
        {

            var directorToUpdate = await _unitOfWork.Repository<Director>().GetByIdAsync(request.Id);

            if (directorToUpdate == null)
            {
                _logger.LogError($"El director con id {request.Id} no existe");
                throw new NotFoundException(nameof(Director), request.Id);
                
            }

            _mapper.Map(request, directorToUpdate, typeof(UpdateDirectorCommand), typeof(Director));


            _unitOfWork.Repository<Director>().UpdateEntity(directorToUpdate);
            await _unitOfWork.Complete();

            _logger.LogInformation($"La operacion fue exitosa actualizando el director {request.Id}");

            return Unit.Value;

        }
    }
}
